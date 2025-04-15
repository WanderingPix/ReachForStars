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

    GameObject TargetNameplate;

    public override void Initialize(PlayerControl player)
    {
        GenerateNewBountyTarget();
    }

    public override void SpawnTaskHeader(PlayerControl playerControl)
    {
        // remove existing task header.
    }

    public override bool DidWin(GameOverReason gameOverReason)
    {
        return gameOverReason == CustomGameOver.GameOverReason<BountyHunterWin>();
    }
    
    GameObject Popup;
    public PlayerControl BountyTarget;
    private void GenerateNewBountyTarget()
    {
        Random rnd = new Random();
        List<PlayerControl> Playerpool = Helpers.GetAlivePlayers().Where(x => x.Data.PlayerId != PlayerControl.LocalPlayer.Data.PlayerId).ToList();
        int index = rnd.Next(Playerpool.Count);
        BountyTarget = Playerpool[index];

        Popup = HudManager.Instance.SpawnHnSPopUp(BountyTarget, $"Wipe  out {BountyTarget.Data.name}");

        RoleDescription.Replace(RoleLongDescription, $"Kill {BountyTarget.Data.PlayerName}");
    }
    public override void OnMeetingStart()
    {
        if (BountyTarget != null && BountyTarget.Data.IsDead)
        {
            SuccessfulKills++;
        }
        else if (BountyTarget != null && !BountyTarget.Data.IsDead)
        {
            UnsuccessfulKills++;
        }
        GenerateNewBountyTarget();
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
}
