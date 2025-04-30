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

namespace ReachForStars.Roles.Neutrals.CursedSoul;

public class CursedSoulRole : ImpostorRole, ICustomRole
{
    public string RoleName => "Cursed Soul";
    public string RoleDescription => "Win the game as a new body";
    public string RoleLongDescription => RoleDescription;
    public Color RoleColor => new Color(0.7f, 0.7f, 0.7f, 1f);
    public ModdedRoleTeams Team => ModdedRoleTeams.Custom;
    public StringNames BlurbName = CustomStringName.CreateAndRegister("Cursed Soul");

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
    bool HasDiedOnce = false;
    public override void Initialize(PlayerControl player)
    {
        
        this.Player = player;
        if (!player.AmOwner)
        {
            RoleBehaviourStubs.Initialize(this, player);
            
            player.RpcCustomMurder(player, true, false, false, false, false, false);
            CustomButtonSingleton<posess>.Instance.Button.ToggleVisible(true);
        }
    }
}
