using MiraAPI.Roles;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Stickster;

public class SticksterRole : ImpostorRole, ICustomRole
{
    public TranslationPool RoleDescLong = new(
        "Spill glue to slow down crewmates!",
        "Derrama pegamento para ralentizar a los tripulantes!",
        "Verse de la colle pour ralentir l'équipage!",
        "Разлей клей чтобы замедлять экипаж!"
        //italian: ""
    );

    public TranslationPool RoleDescShort = new TranslationPool
    (
        english: "Spill glue all over the map!",
        spanish: "¡Derrama pegamento por todo el mapa!",
        french: "Verse de la colle partout sur la carte!",
        russian: "Пролей клей по всей карте!"
        //italian: ""
    );

    public string RoleDescriptionLong => RoleDescLong.GetTranslatedText();
    public override bool IsAffectedByComms => false;
    public string RoleName => "Stickster";
    public string RoleDescription => RoleDescShort.GetTranslatedText();
    public string RoleLongDescription => RoleDescription;
    public Color RoleColor => Palette.ImpostorRed;
    public ModdedRoleTeams Team => ModdedRoleTeams.Impostor;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        IntroSound = Assets.SticksterIntroSFX
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