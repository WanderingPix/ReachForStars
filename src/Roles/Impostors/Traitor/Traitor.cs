using System.Linq;
using MiraAPI.Events;
using MiraAPI.Events.Vanilla.Gameplay;
using MiraAPI.Patches.Stubs;
using MiraAPI.PluginLoading;
using MiraAPI.Roles;
using MiraAPI.Utilities;
using ReachForStars.Components;
using ReachForStars.Translation;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Traitor;

[MiraIgnore] //TODO: Finish this role
public class TraitorRole : ImpostorRole, ICustomRole
{
    public TranslationPool rolename = new(
        "Traitor",
        "",
        "Traitre",
        ""
    );

    public override bool IsAffectedByComms => false;
    public bool IsImpostor => true;
    public RoleTeamTypes RoleTeam => RoleTeamTypes.Impostor;
    public string RoleName => rolename.GetTranslatedText();
    public string RoleDescription => "Backstab the crew";
    public string RoleLongDescription => RoleDescription;
    public Color RoleColor => Palette.ImpostorRed;
    public ModdedRoleTeams Team => ModdedRoleTeams.Impostor;

    public CustomRoleConfiguration Configuration => new(this)
    {
        UseVanillaKillButton = true,
        CanGetKilled = true,
        CanUseVent = true,
        Icon = Assets.DigButton,
        TasksCountForProgress = false,
        IntroSound = Assets.DigSfx
    };

    public override void SpawnTaskHeader(PlayerControl playerControl)
    {
        // remove existing task header.
    }

    public override bool DidWin(GameOverReason gameOverReason)
    {
        return GameManager.Instance.DidImpostorsWin(gameOverReason);
    }

    public override void Initialize(PlayerControl player)
    {
        RoleBehaviourStubs.Initialize(this, player);
        if (Player.Data.Role.IsImpostor && TraitorsRemainingTextController.Instance == null)
            Helpers.CreateTextLabel("TraitorsRemainingText", HudManager.Instance.transform,
                    AspectPosition.EdgeAlignments.Bottom, new Vector3(0f, 1f, 0f), 3f).gameObject
                .AddComponent<TraitorsRemainingTextController>();
    }

    public override void OnMeetingStart()
    {
    }

    [RegisterEvent]
    public static void OnIntroCutsceneBeginForAll(IntroBeginEvent e)
    {
        e.IntroCutscene.TeamTitle.GetComponent<TextTranslatorTMP>().DestroyImmediate();
        var TraitorsCount = PlayerControl.AllPlayerControls.ToArray().Count(x => x.Data.Role is TraitorRole r);
        if (PlayerControl.LocalPlayer.Data.Role.IsImpostor && TraitorsCount > 0)
        {
            var t = Instantiate(e.IntroCutscene.TeamTitle, e.IntroCutscene.transform);
            t.transform.localPosition = new Vector3(0, 1.5f, 0);
            if (TraitorsCount > 1)
                t.text += $"<size=4>And there are {TraitorsCount} Traitors...</size>";
            else t.text += "<size=4>And there's 1 Traitor...";
        }
    }
}