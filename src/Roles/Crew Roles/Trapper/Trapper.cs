using MiraAPI.Roles;
using MiraAPI.Utilities.Assets;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Trapper;

public class TrapperRole : CrewmateRole, ICustomRole
{
    public string RoleName => Rolename.GetTranslatedText(); 
    public TranslationPool Rolename = new TranslationPool(
        english: "Trapper",
        spanish: "",
        portuguese: "",
        french: "Piégeur",
        russian: "Траппер", 
        italian: ""
    );
    public string RoleLongDescription => RoleDescLong.GetTranslatedText();
    public TranslationPool RoleDescLong = new TranslationPool(
        english: "Place Traps around the map to catch the impostors!",
        french: "",
        portuguese: "",
        spanish: "",
        russian: "Ставь Трапки (ловушки) чтобы поймать предателей!",
        italian: ""
    );
    public string RoleDescription => RoleDescShort.GetTranslatedText();
    public TranslationPool RoleDescShort = new TranslationPool(
        english: "Trap the Impostors!",
        french: "Piégez les imposteurs!",
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
        Icon = Assets.PlaceHolder,
    };
}
