using MiraAPI.Roles;
using UnityEngine;
using MiraAPI.GameEnd;
using MiraAPI.GameOptions;
using ReachForStars.MiscSettings;
using ReachForStars.Networking;
using MiraAPI.Networking;
using MiraAPI.Patches.Stubs;
using MiraAPI.Hud;
using System;
using Reactor.Localization.Utilities;
using MiraAPI.PluginLoading;

namespace ReachForStars.Roles.Neutrals.CursedSoul;
public class CursedSoulRole : ImpostorRole, ICustomRole
{
    public string RoleName => "Cursed Soul";
    public string RoleDescription => "Win the game as a new body";
    public string RoleLongDescription => RoleDescription;
    public Color RoleColor => Color.gray;
    public ModdedRoleTeams Team => ModdedRoleTeams.Custom;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        UseVanillaKillButton = true,
        CanGetKilled = true,
        CanUseVent = !OptionGroupSingleton<MiscOptions>.Instance.NoVents,
    };
    

    public override void SpawnTaskHeader(PlayerControl playerControl)
    {
        // remove existing task header.
    }

    public override bool DidWin(GameOverReason gameOverReason)
    {
        return true;
    }
    public override void Initialize(PlayerControl player)
    {
        RoleBehaviourStubs.Initialize(this, player);

        player.Die(DeathReason.Kill, false);

        CustomButtonSingleton<PossessButton>.Instance.Button.Show();
    }
}
