using MiraAPI.PluginLoading;
using MiraAPI.Roles;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Trapper;

[MiraIgnore]
public class TrapperRole : CrewmateRole, ICustomRole
{
    public TranslationPool RoleDescLong = new TranslationPool(
        english: "Place Traps around the map to catch the impostors!",
        french: "Mettez des piéges pour trouver les imposteurs!",
        spanish: "¡Coloca trampas alrededor del mapa para atrapar a los impostores!",
        russian: "Ставь Трапки (ловушки) чтобы поймать предателей!"
        //italian: ""
    );

    public TranslationPool RoleDescShort = new TranslationPool(
        english: "Trap the Impostors!",
        french: "Piégez les imposteurs!",
        spanish: "¡Atrapa a los impostores!",
        russian: "Замани предателей в ловушку"
        //italian: ""
    );

    public TranslationPool Rolename = new(
        "Trapper",
        "Trampero",
        "Piégeur",
        "Траппер"
        //italian: ""
    );

    public override bool IsAffectedByComms => false;
    public string RoleName => Rolename.GetTranslatedText();
    public string RoleLongDescription => RoleDescLong.GetTranslatedText();
    public string RoleDescription => RoleDescShort.GetTranslatedText();
    public Color RoleColor => Palette.CrewmateRoleHeaderBlue;
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;

    public Color OptionsMenuColor => Palette.CrewmateRoleHeaderBlue;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        //Icon = Assets.PlaceHolder,
        IntroSound = Assets.TrapPlaceSfx,
    };
}