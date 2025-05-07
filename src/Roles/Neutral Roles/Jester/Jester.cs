using MiraAPI.Roles;
using UnityEngine;
using MiraAPI.GameEnd;
using MiraAPI.GameOptions;
using ReachForStars.MiscSettings;
using MiraAPI.Patches.Stubs;
using MiraAPI.Modifiers;

namespace ReachForStars.Roles.Neutrals.Jester;

public class JesterRole : ImpostorRole, ICustomRole
{
    public string RoleName => "Jester";
    public string RoleDescription => "Fool the crew!";
    public string RoleLongDescription => RoleDescription;
    public Color RoleColor => new Color(1f, 0.18f, 0.81f, 1f);
    public ModdedRoleTeams Team => ModdedRoleTeams.Custom;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        UseVanillaKillButton = false,
        CanGetKilled = true,
        CanUseVent = !OptionGroupSingleton<MiscOptions>.Instance.NoVents,
        CanUseSabotage = false,
    };
    public string GetCustomEjectionMessage(NetworkedPlayerInfo player)
    {
        return $"You've all been fooled!\n {player.PlayerName} was the Jester!\n\n";
    }
    public override void Initialize(PlayerControl p)
    {
        RoleBehaviourStubs.Initialize(this, p);
    }
    public override void Deinitialize(PlayerControl p)
    {   
    }
    

    public override void SpawnTaskHeader(PlayerControl playerControl)
    {
        // remove existing task header.
    }

    public override bool DidWin(GameOverReason gameOverReason)
    {
        return gameOverReason == CustomGameOver.GameOverReason<JesterWin>();
    }
    public override void OnDeath(DeathReason deathreason)
    {
        if (deathreason == DeathReason.Exile)
        {
            CustomGameOver.Trigger<JesterWin>([Player.Data]);
        }
    }
}
