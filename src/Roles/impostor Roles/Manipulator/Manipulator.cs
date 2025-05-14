using MiraAPI.GameOptions;
using MiraAPI.Roles;
using UnityEngine;
using ReachForStars.Translation;

namespace ReachForStars.Roles.Impostors.Manipulator;


public class ManipulatorRole : ImpostorRole, ICustomRole
{
    public string RoleName => roleName.GetTranslatedText();
    public TranslationPool roleName = new TranslationPool
    (
        english: "Manipulator",
        french: "Manipulateur",
        spanish: "Manipulador",
        portuguese: "Manipulador",
        russian: "",
        italian: ""
    );
    public string RoleDescription => RoleDescShort.GetTranslatedText();
    public TranslationPool RoleDescShort = new TranslationPool
    (
        english: "Use magic against the crew.",
        french: "Utilisez de la magie contre l'equipage.",
        spanish: "",
        portuguese: "",
        russian: "",
        italian: ""
    );
    public string RoleLongDescription => RoleDescLong.GetTranslatedText();
    public TranslationPool RoleDescLong = new TranslationPool
    (
        english: "Use potions to kill, roleblock, and confuse the crew",
        french: "Utilisez des potions pour tuer, bloquer les roles des coéquipiers, et les embrouilles",
        spanish: "",
        portuguese: "",
        russian: "",
        italian: ""
    );
    public Color RoleColor => Palette.ImpostorRed;
    public ModdedRoleTeams Team => ModdedRoleTeams.Impostor;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        UseVanillaKillButton = OptionGroupSingleton<ManipulatorOptions>.Instance.CanDoNormalKilling.Value,
        CanGetKilled = true,
        CanUseVent = OptionGroupSingleton<ManipulatorOptions>.Instance.Canvent.Value,
    };

    public override void SpawnTaskHeader(PlayerControl playerControl)
    {
        // remove existing task header.
    }

    public override bool DidWin(GameOverReason gameOverReason)
    {
        return GameManager.Instance.DidImpostorsWin(gameOverReason);
    }
}
