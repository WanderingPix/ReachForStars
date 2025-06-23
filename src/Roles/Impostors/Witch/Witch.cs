using MiraAPI.GameOptions;
using MiraAPI.Roles;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Witch;

public class WitchRole : ImpostorRole, ICustomRole
{
    public TranslationPool RoleDescLong = new(
        "Use potions to and confuse the crew",
        french: "Utilisez des potions pour tuer et les roles des coéquipiers, et les embrouilles",
        spanish: "Usa pociones para matar y confundir a la tripulación",
        russian: "Используйте зелья, чтобы сбить с толку экипаж!"
        //italian: ""
    );

    public TranslationPool RoleDescShort = new(
        "Use magic against the crew.",
        french: "Utilisez de la magie contre l'equipage.",
        spanish: "Usa magia contra la tripulación.",
        russian: "Используй магию против экипажа!"
        //italian: "Utilizza la magia contro l'equipaggio."
    );

    public TranslationPool roleName = new(
        "Witch",
        french: "Sorcière",
        spanish: "Bruja",
        russian: "Ведьма"
        //italian: "Strega"
    );

    public override bool IsAffectedByComms => false;
    public string RoleName => roleName.GetTranslatedText();
    public string RoleDescription => RoleDescShort.GetTranslatedText();
    public string RoleLongDescription => RoleDescLong.GetTranslatedText();

    public Color RoleColor => Palette.ImpostorRed;
    public ModdedRoleTeams Team => ModdedRoleTeams.Impostor;

    public CustomRoleConfiguration Configuration => new(this)
    {
        UseVanillaKillButton = OptionGroupSingleton<WitchOptions>.Instance.CanDoNormalKilling,
        CanGetKilled = true,
        TasksCountForProgress = false
        //Icon = Assets.PlaceHolder
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