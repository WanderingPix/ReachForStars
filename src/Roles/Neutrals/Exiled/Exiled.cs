using AmongUs.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Patches.Stubs;
using MiraAPI.Roles;
using MiraAPI.Utilities;
using UnityEngine;

namespace ReachForStars.Roles.Neutrals.Exiled;

public class Exiled : ImpostorRole, ICustomRole
{
    public ExiledEnemyTeam EnemyTeam;
    public override bool IsAffectedByComms => false;
    public string RoleName => "Exiled";
    public string RoleDescription => $"Make sure {EnemyTeam} lose at all costs!";
    public string RoleLongDescription => RoleDescription;
    public Color RoleColor => RFSPalette.ExiledColor;
    public ModdedRoleTeams Team => ModdedRoleTeams.Custom;

    public CustomRoleConfiguration Configuration => new(this)
    {
        UseVanillaKillButton = false,
        CanGetKilled = true,
        CanUseVent = false,
        GhostRole = (RoleTypes)RoleId.Get<NeutralGhost>(),
        TasksCountForProgress = false
    };

    public override void SpawnTaskHeader(PlayerControl playerControl)
    {
        // remove existing task header.
    }

    public override bool DidWin(GameOverReason gameOverReason)
    {
        if (EnemyTeam == ExiledEnemyTeam.Crewmates) return !GameManager.Instance.DidHumansWin(gameOverReason);

        return !GameManager.Instance.DidImpostorsWin(gameOverReason);
    }

    public override void Initialize(PlayerControl player)
    {
        RoleBehaviourStubs.Initialize(this, player);
        if (player == PlayerControl.LocalPlayer)
        {
            var CheckChance = Helpers.CheckChance(50);
            if (CheckChance)
                EnemyTeam = ExiledEnemyTeam.Crewmates;
            else
                EnemyTeam = ExiledEnemyTeam.Impostors;

            CustomButtonSingleton<ExileKill>.Instance.Button.Show();
        }
    }

    public override void OnMeetingStart()
    {
        CustomButtonSingleton<ExileKill>.Instance.SetUses(1);
    }
}

public enum ExiledEnemyTeam
{
    Crewmates,
    Impostors
}