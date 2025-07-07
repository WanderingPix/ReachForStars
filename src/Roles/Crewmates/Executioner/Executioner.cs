using System;
using MiraAPI.PluginLoading;
using MiraAPI.Roles;
using ReachForStars.Networking;
using ReachForStars.Translation;
using ReachForStars.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace ReachForStars.Roles.Crewmates.Executionner;

[MiraIgnore]
public class ExecutionerRole : CrewmateRole, ICustomRole
{
    public TranslationPool RoleDescLong = new(
        "Finish your tasks to execute the Impostors!",
        french: "Utilisez la carte d'administration et les caméras pour recueillir de l'info"
    );

    public TranslationPool RoleDescShort = new(
        "Gather Info on the crew",
        french: "Espionnez vos coéquipiers"
    );

    public TranslationPool Rolename = new(
        "Executioner",
        "fisgón",
        "Espion",
        "шпион"
        //italian: "Spia"
    );

    public Color NameColor => Palette.White;
    public string RoleName => Rolename.GetTranslatedText();
    public string RoleLongDescription => RoleDescLong.GetTranslatedText();
    public string RoleDescription => RoleDescShort.GetTranslatedText();
    public Color RoleColor => Palette.CrewmateRoleHeaderBlue;
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;

    public CustomRoleConfiguration Configuration => new(this);

    public bool CanLocalPlayerSeeRole(PlayerControl player)
    {
        return false;
    }

    public void SetUpVoteArea(PlayerVoteArea voteArea)
    {
        var ExecuteButton = Instantiate(voteArea.Buttons.transform.GetChild(0), voteArea.Buttons.transform);
        ExecuteButton.name = "ExecuteButton";
        ExecuteButton.transform.localPosition = Vector3.zero;

        var rend = ExecuteButton.GetComponent<SpriteRenderer>();
        rend.sprite = Assets.ExileButton.LoadAsset();

        var button = ExecuteButton.GetComponent<PassiveButton>();
        button.OnClick = new Button.ButtonClickedEvent();
        button.OnClick.AddListener(OnClick(voteArea.TargetPlayerId));
    }

    public Action OnClick(byte id)
    {
        void Listener()
        {
            if (MeetingHud.Instance) Player.RpcExecutePlayer(PlayerControlUtils.GetPlayerById(id));
        }

        return Listener;
    }
}