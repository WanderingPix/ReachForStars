using MiraAPI.Roles;
using MiraAPI.Utilities.Assets;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.FogBringer;

public class FogBringerRole : CrewmateRole, ICustomRole
{
    public string RoleName => Rolename.GetTranslatedText(); 
    public TranslationPool Rolename = new TranslationPool(
        english: "FogBringer",
        spanish: "",
        portuguese: "",
        french: "",
        russian: "", 
        italian: ""
    );
    public string RoleLongDescription => RoleDescLong.GetTranslatedText();
    public TranslationPool RoleDescLong = new TranslationPool(
        english: "Place smoke clouds aroudn the map to catch the impostors!",
        french: "",
        portuguese: "",
        spanish: "",
        russian: "",
        italian: ""
    );
    public string RoleDescription => RoleDescShort.GetTranslatedText();
    public TranslationPool RoleDescShort = new TranslationPool(
        english: "Catch the impostors red-handed!",
        french: "",
        portuguese: "",
        spanish: "",
        russian: "",
        italian: ""
    );
    
    public Color RoleColor => Palette.CrewmateRoleHeaderBlue;
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;

    public Color OptionsMenuColor => Palette.CrewmateBlue;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        Icon = Assets.SheriffIcon0,
    };
}
