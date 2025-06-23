using MiraAPI.Roles;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Sheriff;

public class SheriffRole : CrewmateRole, ICustomRole
{
    public TranslationPool RoleDescLong = new(
        "Shoot the Impostors, but\n not the Crew",
        french: "Tirez sur les imposteurs,\n mais pas sur vos coéquipiers",
        spanish: "Dispara a los impostores pero no\n a los tripulantes",
        russian: "стреляй в предателей, но не в экипаж"
        //italian: "Spara gli Impostori\n non l'equipaggio"
    );

    public TranslationPool RoleDescShort = new(
        "Choose who to shoot",
        french: "Choisissez sur qui tirer",
        spanish: "Elige a quién disparar",
        russian: "выбери того, кого застрелить"
        //italian: "Scegli la persona da sparare"
    );

    public TranslationPool Rolename = new(
        "Sheriff",
        "Sheriff",
        "Shérif",
        "Шериф"
        //italian: "Sceriffo"
    );

    public override bool IsAffectedByComms => false;
    public string RoleName => Rolename.GetTranslatedText();
    public string RoleLongDescription => RoleDescLong.GetTranslatedText();
    public string RoleDescription => RoleDescShort.GetTranslatedText();
    public Color RoleColor => Palette.CrewmateRoleHeaderBlue;
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;

    public Color OptionsMenuColor => Palette.CrewmateRoleHeaderBlue;

    public CustomRoleConfiguration Configuration => new(this)
    {
        Icon = Assets.SheriffIcon
    };
}