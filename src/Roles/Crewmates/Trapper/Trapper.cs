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
        spanish: "Trampero",
         
        french: "Piégeur",
        russian: "Траппер", 
        italian: ""
    );
    public string RoleLongDescription => RoleDescLong.GetTranslatedText();
    public TranslationPool RoleDescLong = new TranslationPool(
        english: "Place Traps around the map to catch the impostors!",
        french: "Mettez des piéges pour trouver les imposteurs!",        
        spanish: "¡Coloca trampas alrededor del mapa para atrapar a los impostores!",
        russian: "Ставь Трапки (ловушки) чтобы поймать предателей!",
        italian: ""
    );
    public string RoleDescription => RoleDescShort.GetTranslatedText();
    public TranslationPool RoleDescShort = new TranslationPool(
        english: "Trap the Impostors!",
        french: "Piégez les imposteurs!",
        spanish: "¡Atrapa a los impostores!",
        russian: "",
        italian: ""
    );
    
    public Color RoleColor => Palette.CrewmateBlue;
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;

    public Color OptionsMenuColor => Palette.CrewmateBlue;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        //Icon = Assets.PlaceHolder,
        IntroSound = Assets.TrapPlaceSfx,
    };
}
