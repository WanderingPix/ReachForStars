using System.Collections;
using MiraAPI.Events;
using MiraAPI.Events.Vanilla.Player;
using MiraAPI.Roles;
using MiraAPI.Utilities;
using ReachForStars.Components;
using ReachForStars.Translation;
using ReachForStars.Utilities;
using Reactor.Utilities;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Blessed;

public class BlessedRole : CrewmateGhostRole, ICustomRole
{
    public TranslationPool RoleDescLong = new(
        "Finish your tasks to revive yourself!",
        french: "",
        spanish: "",
        russian: ""
    );

    public TranslationPool RoleDescShort = new(
        "Finish your tasks to revive yourself!",
        french: "",
        spanish: "",
        russian: ""
    );

    public TranslationPool Rolename = new(
        "Blessed",
        "",
        "",
        ""
    );

    public override bool IsAffectedByComms => false;

    public void Start()
    {
        if (Player == null) return;

        Player.gameObject.layer = LayerMask.NameToLayer("Players");

        if (Player != PlayerControl.LocalPlayer) return;

        Player.RegenerateTasks();
        Player.gameObject.layer = LayerMask.NameToLayer("Players");

        //TBD Custom Player Model :3
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
        TasksCountForProgress = false
    };

    public static IEnumerator CoRevive(PlayerControl p)
    {
        if (PlayerControl.LocalPlayer.Data.Role.IsImpostor) ShowReviveArrow(p);
        yield return new WaitForSeconds(8f);
        p.Revive();
        yield break;
    }

    public static void ShowReviveArrow(PlayerControl target)
    {
        var go = Instantiate(Assets.ReviveArrowPrefab.LoadAsset());
        var arrow = go.AddComponent<ReviveArrow>();
        arrow.RevivingPlayer = target;
    }

    public override void Deinitialize(PlayerControl targetPlayer)
    {
        //TBD Custom Player Model :3
    }

    [RegisterEvent]
    public static void OnTaskComplete(CompleteTaskEvent e)
    {
        if (e.Player.Data.Role is BlessedRole b && e.Player.GetTasksLeft() == 0)
            Coroutines.Start(CoRevive(e.Player)); //Check if player is blessed and has finished all their tasks
    }
}