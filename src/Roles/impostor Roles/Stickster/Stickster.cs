using System.Collections.Generic;
using MiraAPI.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Roles;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Stickster;


public class SticksterRole : ImpostorRole, ICustomRole
{
    public string RoleName => "Stickster";
    public string RoleDescription => RoleDescShort.GetTranslatedText();
    public TranslationPool RoleDescShort = new TranslationPool
    (
        english: "Spill glue all over the map!",
        spanish: "¡Derrama pegamento por todo el mapa!",
        french: "Verse de la colle partout sur la carte!",
        russian: "Пролей клей по всей карте!",
        italian: ""
    );

    public string RoleDescriptionLong => RoleDescLong.GetTranslatedText();
    public TranslationPool RoleDescLong = new TranslationPool
    (
        english: "Spill glue to slow down crewmates!",
        spanish: "Derrama pegamento para ralentizar a los tripulantes!",
        french: "Verse de la colle pour ralentir l'équipage!",
        russian: "",
        italian: ""
    );
    public string RoleLongDescription => RoleDescription;
    public Color RoleColor => Palette.ImpostorRed;
    public ModdedRoleTeams Team => ModdedRoleTeams.Impostor;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
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
