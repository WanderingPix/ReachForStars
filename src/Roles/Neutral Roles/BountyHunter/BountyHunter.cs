using System.Collections.Generic;
using System.Linq;
using MiraAPI.Roles;
using MiraAPI.Utilities;
using ReachForStars.Roles.Neutrals.BountyHunter;
using ReachForStars.Utilities;
using MiraAPI.GameEnd;
using UnityEngine;
using ReachForStars.Translation;
using Random = System.Random;
using MiraAPI.GameOptions;
using MiraAPI.Patches.Stubs;
using Reactor.Utilities.Extensions;
using MiraAPI.Modifiers;
using TMPro;

namespace ReachForStars.Roles.Neutrals.Roles;

public class BountyHunterRole : ImpostorRole, ICustomRole
{
    public string RoleName => roleName.GetTranslatedText();
    public TranslationPool roleName = new TranslationPool
    (
        english: "Bounty Hunter",
        french: "Chasseur De Prime",
        spanish: "",
        portuguese: "",
        russian: "Охотник за Головами",
        italian: "Mercenario"
    );
    public string RoleDescription => "Make sure your targets are dead";
    public string RoleLongDescription => RoleDescription;
    public Color RoleColor => new Color(1f, 0.12f, 0.54f, 1f);
    public ModdedRoleTeams Team => ModdedRoleTeams.Custom;
    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        UseVanillaKillButton = true,
        CanGetKilled = true,
        CanUseVent = false,
    };
    PlayerVoteArea Popup;
    public PlayerControl BountyTarget;
    public int SuccessfulKills;
    public GameObject BountyUIHolder;
    public TextMeshPro BountyText;
    public override void Initialize(PlayerControl player)
    {
        RoleBehaviourStubs.Initialize(this, player);
        HudManager.Instance.KillButton.Show();
        SetUpUI();
        GenerateNewBountyTarget();
    }
    public void SetUpUI()
    {
        //UI Holder stuff
        BountyUIHolder = new GameObject("BountyUIHolder");
        BountyUIHolder.transform.localScale = new Vector3(4f, 1.5f, 1f);
        BountyUIHolder.transform.SetParent(HudManager.Instance.gameObject.transform);
        AspectPosition pos = BountyUIHolder.gameObject.AddComponent<AspectPosition>();
        pos.Alignment = AspectPosition.EdgeAlignments.Top;
        pos.DistanceFromEdge = new Vector3(0f, 0.75f, 0f);
        pos.AdjustPosition();


        //PlayerVoteArea stuff
        Popup = HudManager.Instance.MeetingPrefab.CreateButton(null); //Using Player temporarily, it'll get overriden anyway by GenerateBountyTarget
        Popup.transform.SetParent(BountyUIHolder.transform);
        foreach (var rend in Popup.gameObject.transform.GetComponentsInChildren<SpriteRenderer>())
        {
            rend.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        }
        Popup.XMark.gameObject.SetActive(false);
        Popup.Overlay.gameObject.SetActive(false);
        Popup.SetHighlighted(true);
        Popup.transform.localPosition = new Vector3(0f, -0.5f, 0f);

        Popup.PlayerIcon.gameObject.SetActive(true);


        //BountyText stuff
        BountyText = Object.Instantiate<TextMeshPro>(HudManager.Instance.KillButton.buttonLabelText, BountyUIHolder.transform);
        BountyText.gameObject.GetComponent<TextTranslatorTMP>().DestroyImmediate();
        BountyText.gameObject.transform.localScale = new Vector3(1.25f, 2.5f, 1f);
    }
    public override void Deinitialize(PlayerControl targetPlayer)
    {
        if (BountyUIHolder != null)
        {
            BountyUIHolder.gameObject.DestroyImmediate();
        }
    }


    public override void SpawnTaskHeader(PlayerControl playerControl)
    {
        // remove existing task header.
    }

    public override bool DidWin(GameOverReason gameOverReason)
    {
        return gameOverReason == CustomGameOver.GameOverReason<BountyHunterWin>();
    }
    private void GenerateNewBountyTarget()
    {
        Random rnd = new Random();
        List<PlayerControl> Playerpool = Helpers.GetAlivePlayers().Where(x => x != PlayerControl.LocalPlayer && x != MeetingHud.Instance.exiledPlayer).ToList();
        int index = rnd.Next(Playerpool.Count);
        BountyTarget = Playerpool[index];

        if (Popup)
        {
            Popup.SetDead(false, false);
            Popup.SetCosmetics(BountyTarget.Data);
        }
        if (BountyText)
        {
            BountyText.text = $"Current Target: {BountyTarget.Data.PlayerName}\n\n\n\n ({SuccessfulKills}/{OptionGroupSingleton<BountyHunterOptions>.Instance.SuccessfulKillsQuota})";
        }
    }

    public override void OnVotingComplete()
    {
        GenerateNewBountyTarget();
        BountyUIHolder.SetActive(true);
    }
    public override void OnMeetingStart()
    {
        BountyUIHolder.SetActive(false);
    }

    public override PlayerControl FindClosestTarget()
    {
        if (PlayerControl.LocalPlayer.GetClosestPlayer(true, 1f, false) == BountyTarget)
        {
            return BountyTarget;
        }
        else return null;
    }
    public void OnTargetKill()
    {
        SuccessfulKills++;
        Popup.SetDead(false, true, BountyTarget.Data.Role is GuardianAngelRole);

        BountyText.text = $"Current Target: {BountyTarget.Data.PlayerName}\n\n\n\n ({SuccessfulKills}/{OptionGroupSingleton<BountyHunterOptions>.Instance.SuccessfulKillsQuota})";
        
        if (SuccessfulKills >= ((int)OptionGroupSingleton<BountyHunterOptions>.Instance.SuccessfulKillsQuota))
        {
            CustomGameOver.Trigger<BountyHunterWin>([Player.Data]);
        }
    }
}
