using System.Collections.Generic;
using System.Linq;
using MiraAPI.Roles;
using MiraAPI.Utilities;
using ReachForStars.Roles.Neutrals.BountyHunter;
using ReachForStars.Utilities;
using MiraAPI.GameEnd;
using UnityEngine;
using Random = System.Random;

namespace ReachForStars.Roles.Neutrals.Roles;

public class BountyHunterRole : ImpostorRole, ICustomRole
{
    public string RoleName => "Bounty Hunter";
    public string RoleDescription => "Make sure your targets are dead";
    public string RoleLongDescription => RoleDescription;
    public Color RoleColor => new Color(1f, 0.12f, 0.54f, 1f);
    public ModdedRoleTeams Team => ModdedRoleTeams.Custom;

    public int SuccessfulKills;

    public int UnsuccessfulKills;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        UseVanillaKillButton = true,
        CanGetKilled = true,
        CanUseVent = false,
    };

    public override void Initialize(PlayerControl player)
    {
        GenerateNewBountyTarget();
        HudManager.Instance.KillButton.Show();

        Popup = Object.Instantiate<HideAndSeekDeathPopupNameplate>(GameManagerCreator.Instance.HideAndSeekManagerPrefab.DeathPopupPrefab.GetComponentInChildren<HideAndSeekDeathPopupNameplate>(), HudManager.Instance.transform.parent);
        AspectPosition pos = Popup.gameObject.AddComponent<AspectPosition>();
        pos.Alignment = AspectPosition.EdgeAlignments.Top;
        pos.DistanceFromEdge = new Vector3(0f, 1f, 0f);
        pos.AdjustPosition();
    }

    public override void SpawnTaskHeader(PlayerControl playerControl)
    {
        // remove existing task header.
    }

    public override bool DidWin(GameOverReason gameOverReason)
    {
        return gameOverReason == CustomGameOver.GameOverReason<BountyHunterWin>();
    }
    
    HideAndSeekDeathPopupNameplate Popup;
    public PlayerControl BountyTarget;
    private void GenerateNewBountyTarget()
    {
        Random rnd = new Random();
        List<PlayerControl> Playerpool = Helpers.GetAlivePlayers().Where(x => x.Data.PlayerId != PlayerControl.LocalPlayer.Data.PlayerId).ToList();
        int index = rnd.Next(Playerpool.Count);
        BountyTarget = Playerpool[index];
        Popup.SetPlayer(BountyTarget);
    }

    public override void OnVotingComplete()
    {
        GenerateNewBountyTarget();
    }
    public override PlayerControl FindClosestTarget()
    {
        if (PlayerControl.LocalPlayer.GetClosestPlayer(true, 1f, false) == BountyTarget)
        {
            return BountyTarget;
        }
        else return null;
    }
    public override void UseAbility()
    {
        HudManager.Instance.StartCoroutine(Effects.ScaleIn(Popup.gameObject.transform, 2f, 1f, 1.2f));
        HudManager.Instance.StartCoroutine(Effects.ColorFade(Popup.background, Color.white, new Color(0f, 0f, 0f, 0f), 1.2f));
    }
}
