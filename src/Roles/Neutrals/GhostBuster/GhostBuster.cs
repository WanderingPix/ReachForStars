using AmongUs.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Modifiers;
using MiraAPI.Patches.Stubs;
using MiraAPI.Roles;
using ReachForStars.Modifiers;
using ReachForStars.Roles.Neutrals.GhostBuster;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Neutrals.Roles.GhostBuster;

public class GhostBusterRole : ImpostorRole, ICustomRole
{
    public TranslationPool roleDescShort = new
    (
        "Collect 3 souls to win!",
        "¡Mata a tus objetivos para ganar!",
        "Tuez vos primes pour gagner!",
        "Убей свои цели, чтобв победить!"
        //italian: "Asassina i tuoi target per vincere!"
    );

    public TranslationPool roleName = new(
        "Ghost Buster",
        french: "Chasseur De Prime",
        spanish: "cazarrecompensas",
        russian: "Охотник за Головами"
        //italian: "Sicario"
    );

    public override bool IsAffectedByComms => false;
    public string RoleName => roleName.GetTranslatedText();
    public string RoleDescription => roleDescShort.GetTranslatedText();
    public string RoleLongDescription => RoleDescription;
    public Color RoleColor => RFSPalette.GhostBusterColor;
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
        return Player.HasModifier<NeutralWinner>();
    }

    public override void Initialize(PlayerControl player)
    {
        RoleBehaviourStubs.Initialize(this, player);
        Player.AddModifier<CanSeeGhostsModifier>();
        if (player == PlayerControl.LocalPlayer)
        {
            CustomButtonSingleton<Vacuum>.Instance.Button.Show();
            //TODO: UI and win cons
        }
    }

    public override void Deinitialize(PlayerControl targetPlayer)
    {
        Player.RemoveModifier<CanSeeGhostsModifier>();
    }
}