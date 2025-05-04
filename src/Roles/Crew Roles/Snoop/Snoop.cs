using MiraAPI.Roles;
using MiraAPI.Utilities.Assets;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Snoop;

public class SnoopRole : CrewmateRole, ICustomRole
{
    public string RoleName => Rolename.GetTranslatedText(); 
    public TranslationPool Rolename = new TranslationPool(
        english: "Snoop",
        spanish: "fisgón",
        portuguese: "bisbilhoteira",
        french: "Espion",
        russian: "шпион",
        italian: "Spia"
    );
    public string RoleLongDescription => RoleDescLong.GetTranslatedText();
    public TranslationPool RoleDescLong = new TranslationPool(
        english: "Use the admin map and place cameras to gather info",
        french: "Utilisez la carte d'administration et les caméras pour recueillir de l'info"
    );
    public string RoleDescription => RoleDescShort.GetTranslatedText();
    public TranslationPool RoleDescShort = new TranslationPool(
        english:"Gather Info on the crew",
        french:"Espionnez vos coéquipiers",
    );
    public Color RoleColor => Palette.White;
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;

    public Color OptionsMenuColor => Palette.CrewmateBlue;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
    };
}
