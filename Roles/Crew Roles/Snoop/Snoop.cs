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
        italian: "Spia" //https://shared.cloudflare.steamstatic.com/store_item_assets/steam/apps/440/manuals/SentryManual_web.pdf?t=1745368581
    );
    public string RoleLongDescription => "Gather Info on the crew"; //TODO trans
    public string RoleDescription => RoleLongDescription;
    public Color RoleColor => Palette.White;
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;

    public Color OptionsMenuColor => Palette.CrewmateBlue;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
    };
}
