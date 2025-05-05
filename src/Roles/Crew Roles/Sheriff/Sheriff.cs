using MiraAPI.Roles;
using MiraAPI.Utilities.Assets;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Sheriff;

public class SheriffRole : CrewmateRole, ICustomRole
{
    public string RoleName => Rolename.GetTranslatedText(); 
    public TranslationPool Rolename = new TranslationPool(
        english: "Sheriff",
        spanish: "Sheriff",
        portuguese: "Xerife",
        french: "Shérif",
        russian: "Шериф", 
        italian: "Sceriffo"
    );
    public string RoleLongDescription => RoleDescLong.GetTranslatedText();
    public TranslationPool RoleDescLong = new TranslationPool(
        english: "Shoot the Impostors, but\n not the Crew",
        french: "Tirez sur les imposteurs,\n mais pas sur vos coéquipiers",
        portuguese: "Atire nos Impostores, mas\n não nos seus companheiros de equipe",
        spanish: "Dispara a los impostores pero no\n a los tripulantes",
        russian: "стреляй в предателей, но не в экипаж",
        italian: "Spara gli Impostori\n non l'equipaggio"
    );
    public string RoleDescription => RoleDescShort.GetTranslatedText();
    public TranslationPool RoleDescShort = new TranslationPool(
        english: "Choose who to shoot",
        french: "Choisissez sur qui tirer",
        portuguese: "Escolha em quem atirar",
        spanish: "Elige a quién disparar",
        russian: "выбери того, кого застрелить",
        italian: "Scegli la persona da sparare"
    );
    
    public Color RoleColor => Palette.CrewmateRoleHeaderBlue;
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;

    public Color OptionsMenuColor => Palette.CrewmateBlue;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        Icon = Assets.SheriffIcon0,
    };
}
