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
        french: "Shérif"
    );
    public string RoleLongDescription => "Choose who to shoot";
    public string RoleDescription => RoleDescLong.GetTranslatedText();
    public TranslationPool RoleDescLong = new TranslationPool(
        english: "Shoot the Impostors, but\n not the Crew.",
        french: "Tirez sur les imposteurs,\n mais pas sur vos coéquipiers.",
        portuguese: "Atire nos Impostores, mas\n não nos seus companheiros de equipe.",
        spanish: "Dispara a los impostores, pero no\n a tus compañeros."
    );
    
    public Color RoleColor => Palette.White;
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;

    public Color OptionsMenuColor => Palette.CrewmateBlue;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        Icon = Assets.SheriffIcon0,
    };
}
