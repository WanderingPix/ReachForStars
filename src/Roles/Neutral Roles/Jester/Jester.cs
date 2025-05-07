using MiraAPI.Roles;
using UnityEngine;
using MiraAPI.GameEnd;
using MiraAPI.GameOptions;
using ReachForStars.MiscSettings;
using MiraAPI.Patches.Stubs;
using MiraAPI.Modifiers;
using ReachForStars.Translation;

namespace ReachForStars.Roles.Neutrals.Jester;

public class JesterRole : ImpostorRole, ICustomRole
{
    public string RoleName => roleName.GetTranslatedText();
    public TranslationPool roleName = new TranslationPool(
        english: "Jester",
        french: "Plaisantin",
        spanish: "Bufón",
        portuguese: "Bobo",
        russian: "",
        italian: ""
    );
    public string RoleDescription => RoleDescShort.GetTranslatedText();
    public TranslationPool RoleDescShort = new TranslationPool
    (
        english: "Fool The Crew!",
        french: "Trollez l'équipage!",
        spanish: "¡Engaña a la tripulación!",
        portuguese: "Engane a tripulação!",
        russian: "",
        italian: ""
    );
    public string RoleLongDescription => RoleDescLong.GetTranslatedText();
    public TranslationPool RoleDescLong = new TranslationPool
    (
        english: "Get voted out to win.",
        french: "Faites-vous ejecter pour gagner.",
        spanish: "Ser expulsado para ganar",
        portuguese: "Seja expulso para ganhar",
        russian: "",
        italian: ""
    );
    public Color RoleColor => new Color(1f, 0.18f, 0.81f, 1f);
    public ModdedRoleTeams Team => ModdedRoleTeams.Custom;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        UseVanillaKillButton = false,
        CanGetKilled = true,
        CanUseVent = OptionGroupSingleton<JesterOptions>.Instance.CanVent,
        CanUseSabotage = false,
    };
    public TranslationPool EjectMessage = new TranslationPool
    (
        english: "You've all been fooled!\n P was the Jester!\n\n",
        french: "Vous avez tous été dupés!\n P était le Plaisantin!\n\n",
        spanish: "¡Todos han sido engañados!\n P era el Bufón!\n\n",
        portuguese: "Todos foram enganados!\n P era o Bobo!\n\n",
        russian: "",
        italian: ""
    );
    public string GetCustomEjectionMessage(NetworkedPlayerInfo player)
    {
        string message = EjectMessage.GetTranslatedText();
        message = message.Replace("P", player.PlayerName);
        return message;
    }
    public override void Initialize(PlayerControl p)
    {
        RoleBehaviourStubs.Initialize(this, p);
        if (OptionGroupSingleton<JesterOptions>.Instance.CanReportBodies) HudManager.Instance.ReportButton.Hide();
        if (OptionGroupSingleton<JesterOptions>.Instance.CanReportBodies) ShipStatus.Instance.EmergencyButton.enabled = false;
    }
    public override void Deinitialize(PlayerControl p)
    {
        if (OptionGroupSingleton<JesterOptions>.Instance.CanReportBodies) HudManager.Instance.ReportButton.Show();
        if (OptionGroupSingleton<JesterOptions>.Instance.CanReportBodies) ShipStatus.Instance.EmergencyButton.enabled = true;
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
