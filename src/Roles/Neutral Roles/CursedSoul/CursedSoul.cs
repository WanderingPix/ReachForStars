using MiraAPI.Roles;
using UnityEngine;
using MiraAPI.Patches.Stubs;
using MiraAPI.Hud;
using Reactor.Localization.Utilities;
using MiraAPI.Utilities;
using MiraAPI.GameEnd;

namespace ReachForStars.Roles.Neutrals.CursedSoul;
public class CursedSoulRole : ImpostorRole, ICustomRole
{
    public string RoleName => "Cursed Soul";
    public string RoleDescription => "Win the game as a new body";
    public string RoleLongDescription => RoleDescription;
    public Color RoleColor => Color.gray;
    public StringNames BlurbName = CustomStringName.CreateAndRegister("Cursed Soul");
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
    public void FixedUpdate()
    {
        if (!Player.Data.IsDead && Helpers.GetAlivePlayers().Count < 3)
        {
            CustomGameOver.Trigger<CUrsedSoulWin>([Player.Data]);
        }
    }
    public override void Initialize(PlayerControl player)
    {
        RoleBehaviourStubs.Initialize(this, player);

        CustomButtonSingleton<PossessButton>.Instance.Button.Show();

        player.MurderPlayer(player, MurderResultFlags.Succeeded);
    }
}
