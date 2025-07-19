using MiraAPI.Hud;
using MiraAPI.Roles;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Lightener;

public class LightenerRole : CrewmateGhostRole, ICustomRole
{
    public TranslationPool RoleDescLong = new(
        "Help the crew by placing lanterns to light up dim areas!",
        french: "",
        spanish: "",
        russian: ""
    );

    public TranslationPool RoleDescShort = new(
        "Let there be light!",
        french: "",
        spanish: "",
        russian: ""
    );

    public TranslationPool Rolename = new(
        "Lightener",
        "",
        "",
        ""
    );

    public override bool IsAffectedByComms => false;

    public void Start()
    {
        if (PlayerControl.LocalPlayer == Player) CustomButtonSingleton<LightUp>.Instance.SetActive(true, this);
    }

    public string RoleName => Rolename.GetTranslatedText();
    public string RoleLongDescription => RoleDescLong.GetTranslatedText();
    public string RoleDescription => RoleDescShort.GetTranslatedText();
    public Color RoleColor => Palette.CrewmateRoleHeaderBlue;
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;

    public Color OptionsMenuColor => Palette.CrewmateRoleHeaderBlue;

    public CustomRoleConfiguration Configuration => new(this)
    {
        Icon = Assets.SheriffIcon,
        ShowInFreeplay = true,
        HideSettings = false,
    };
}