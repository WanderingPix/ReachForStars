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

    public TranslationPool Rolename = new(
        "Snoop",
        "fisgón",
        "Espion",
        "шпион"
        //italian: "Spia"
    );

    public Color NameColor => Palette.White;
    public string RoleName => Rolename.GetTranslatedText();
    public string RoleLongDescription => RoleDescLong.GetTranslatedText();
    public string RoleDescription => RoleDescShort.GetTranslatedText();
    public Color RoleColor => Palette.CrewmateRoleHeaderBlue;
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;

    public CustomRoleConfiguration Configuration => new(this);
}