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
        spanish: "Sheriff",//TODO these translations
        portuguese: "Xerife",
        french: "Shérif"
    );
    public string RoleLongDescription => "Choose who to shoot";
    public string RoleDescription => RoleLongDescription;
    public Color RoleColor => Palette.White;
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;

    public Color OptionsMenuColor => Palette.CrewmateBlue;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
    };
}
