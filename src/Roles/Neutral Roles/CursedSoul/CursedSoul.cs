using MiraAPI.Roles;
using UnityEngine;
using MiraAPI.Patches.Stubs;
using MiraAPI.Hud;
using Reactor.Localization.Utilities;
using MiraAPI.Utilities;
using MiraAPI.GameEnd;
using MiraAPI.Networking;
using MiraAPI.Events;
using Rewired;
using System.Linq;

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
        CanUseVent = false,
    };


    public override void SpawnTaskHeader(PlayerControl playerControl)
    {
        // remove existing task header.
    }

    public override bool DidWin(GameOverReason gameOverReason)
    {
        return false; //TODO: win cons
    }
    public override void Initialize(PlayerControl player)
    {
        RoleBehaviourStubs.Initialize(this, player);

        CustomButtonSingleton<PossessButton>.Instance.Button.Show();

        player.Visible = true;
    }
    [RegisterEvent]
    public static void OnSetRole(MiraAPI.Events.Vanilla.Gameplay.SetRoleEvent @event)
    {
        foreach (var p in PlayerControl.AllPlayerControls.ToArray().ToList().Where(x => x.Data.Role is CursedSoulRole))
        {
            p.Die(DeathReason.Exile, false);
        }
    }
}
