using MiraAPI.Roles;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Snoop;

public class SnoopRole : CrewmateRole, ICustomRole
{
    public TranslationPool RoleDescLong = new(
        "Use the admin map and place cameras to gather info",
        french: "Utilisez la carte d'administration et les caméras pour recueillir de l'info"
    );

    public TranslationPool RoleDescShort = new(
        "Gather Info on the crew",
        french: "Espionnez vos coéquipiers"
    );

    public TranslationPool Rolename = new TranslationPool(
        english: "Snoop",
        spanish: "fisgón",
        french: "Espion",
        russian: "шпион"
        //italian: "Spia"
    );

    public Color NameColor => Color.white;
    public string RoleName => Rolename.GetTranslatedText();
    public string RoleLongDescription => RoleDescLong.GetTranslatedText();
    public string RoleDescription => RoleDescShort.GetTranslatedText();
    public Color RoleColor => Palette.CrewmateRoleHeaderBlue;
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
    };
}