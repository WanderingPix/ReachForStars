using MiraAPI.GameOptions;
using MiraAPI.Roles;
using UnityEngine;
using ReachForStars.Translation;

namespace ReachForStars.Roles.Impostors.Witch;


public class WitchRole : ImpostorRole, ICustomRole
{
    public string RoleName => roleName.GetTranslatedText();
    public TranslationPool roleName = new TranslationPool
    (
        english: "Witch",
        french: "Sorcière",
        spanish: "Bruja",
        portuguese: "Bruxa",
        russian: "Ведьма",
        italian: "Strega"
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
        UseVanillaKillButton = OptionGroupSingleton<WitchOptions>.Instance.CanDoNormalKilling,
        CanGetKilled = true,
        CanUseVent = OptionGroupSingleton<WitchOptions>.Instance.CanVent,
        Icon = Assets.PoisonButton
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
