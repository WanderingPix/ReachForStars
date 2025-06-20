using System.Collections.Generic;
using MiraAPI.Roles;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Chiller;

public class FreezerRole : ImpostorRole, ICustomRole
{
    public List<Vent> MinerVents;

    public TranslationPool RoleDescLong = new
    (
        "Freeze bodies to stop the crewmates from reporting them!",
        "¡Congela los cuerpos para evitar que los compañeros de tripulación los denuncien!",
        "Figez les cadavres pour empécher les coéquipiers de lés trouver!",
        "Замораживай трупы чтобы экипаж не мог зарепортить!"
        //italian: "Congela i cadaveri per impedire che siano trovati!"
    );

    public TranslationPool RoleDescShort = new
    (
        "Freeze bodies to hide identities!",
        "Congelar cuerpos para ocultar sus identidades",
        "Figez des cadavres pour cacher leurs identités!",
        "Замораживай трупы чтобы избежать личностей!"
        //italian: "Congela cadaveri per nascondere le identità!"
    );

    public TranslationPool rolename = new
    (
        english: "Chiller",
        french: "Refrigirateur",
        spanish: "Congelador",
        russian: "Охладитель"
        //italian: "Congelatore"
    );

    public override bool IsAffectedByComms => false;
    public string RoleName => rolename.GetTranslatedText();
    public string RoleDescription => RoleDescShort.GetTranslatedText();

    public string RoleLongDescription => RoleDescLong.GetTranslatedText();
    public Color RoleColor => Palette.ImpostorRed;
    public ModdedRoleTeams Team => ModdedRoleTeams.Impostor;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        UseVanillaKillButton = true,
        CanGetKilled = true,
        CanUseVent = true,
        //Icon = Assets.ChillerIcon
        IntroSound = Assets.FreezeSFX
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