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

    public int SuccessfulKills;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        UseVanillaKillButton = true,
        CanGetKilled = true,
        CanUseVent = false,
    };

    public override void Initialize(PlayerControl player)
    {
        RoleBehaviourStubs.Initialize(this, player);
        HudManager.Instance.KillButton.Show();

        Popup = HudManager.Instance.MeetingPrefab.CreateButton(GenerateNewBountyTarget().Data);
        Popup.transform.SetParent(HudManager.Instance.transform);
        foreach (var rend in Popup.gameObject.transform.GetComponentsInChildren<SpriteRenderer>())
        {
            rend.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        }
        Popup.SetCosmetics(BountyTarget.Data);
        Popup.Background.enabled = true;
        Popup.SetHighlighted(true);
        AspectPosition pos = Popup.gameObject.AddComponent<AspectPosition>();
        pos.Alignment = AspectPosition.EdgeAlignments.Top;
        pos.DistanceFromEdge = new Vector3(0f, 1f, 0f);
        pos.AdjustPosition();
        Popup.MaskArea.DestroyImmediate();
    }
    public override void Deinitialize(PlayerControl targetPlayer)
    {
        if (Popup != null)
        {
            Popup.gameObject.DestroyImmediate();
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
    
    PlayerVoteArea Popup;
    public PlayerControl BountyTarget;
    private PlayerControl GenerateNewBountyTarget()
    {
        Random rnd = new Random();
        List<PlayerControl> Playerpool = Helpers.GetAlivePlayers().Where(x => x.Data.PlayerId != PlayerControl.LocalPlayer.Data.PlayerId).ToList();
        int index = rnd.Next(Playerpool.Count);
        BountyTarget = Playerpool[index];

        return BountyTarget;
    }

    public override void OnVotingComplete()
    {
        Popup.SetCosmetics(GenerateNewBountyTarget().Data);
        Popup.SetHighlighted(true);
        Popup.MaskArea.DestroyImmediate();
        HudManager.Instance.StartCoroutine(Effects.ScaleIn(Popup.gameObject.transform, 0f, 1f, 0.7f));
    }
    public override void OnMeetingStart()
    {
        HudManager.Instance.StartCoroutine(Effects.ScaleIn(Popup.gameObject.transform, 1f, 0f, 0.7f));
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
        Popup.SetDead(false, true, false);
        
        HudManager.Instance.StartCoroutine(Effects.ScaleIn(Popup.gameObject.transform, 2f, 1f, 1.2f));
        HudManager.Instance.StartCoroutine(Effects.ColorFade(Popup.Background, Color.white, new Color(0f, 0f, 0f, 0f), 1.2f));
        if (SuccessfulKills == 1/*OptionGroupSingleton<BountyHunterOptions>.Instance.SuccessfulKillsQuota*/)
        {
            CustomGameOver.Trigger<BountyHunterWin>([Player.Data]);
        }
    }
}
