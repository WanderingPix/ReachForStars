using MiraAPI.PluginLoading;
using MiraAPI.Roles;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Jailor;

[MiraIgnore]
public class JailorRole : CrewmateRole, ICustomRole
{
    public TranslationPool RoleDescLong = new(
        "",
        french: "",
        spanish: "",
        russian: ""
    );

    public TranslationPool RoleDescShort = new(
        "",
        french: "",
        spanish: "",
        russian: ""
    );

    public TranslationPool Rolename = new(
        "",
        "",
        "",
        ""
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