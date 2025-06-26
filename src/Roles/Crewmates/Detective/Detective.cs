using System.Collections.Generic;
using System.Linq;
using MiraAPI.Hud;
using MiraAPI.Patches.Stubs;
using MiraAPI.Roles;
using MiraAPI.Utilities;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Detective;

public class DetectiveRole : CrewmateRole, ICustomRole
{
    public TranslationPool roleDescLong = new
    (
        "Inspect dead bodies to find out who was nearby!",
        "¡Inspecciona los cadáveres para averiguar quién estaba cerca!",
        "Inspectez des cadavres pour decouvrir qui etait proche!",
        "Иследуй трупы чтобы найти, кто был близко!"
        //italian: "Osseva i cadaveri per ricevere indizi!"
    );

    public TranslationPool roleName = new
    (
        "Detective",
        "Detective",
        "Detective",
        "Детектив"
        //italian: "Ispettore"
    );

    public override bool IsAffectedByComms => false;

    public List<PlayerControl> Suspects { get; set; } = new();

    public Color NameColor => Palette.White;
    public string RoleName => roleName.GetTranslatedText();
    public string RoleLongDescription => roleDescLong.GetTranslatedText();
    public string RoleDescription => roleDescLong.GetTranslatedText();
    public Color RoleColor => Palette.CrewmateRoleHeaderBlue;
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;

    public CustomRoleConfiguration Configuration => new(this)
    {
        Icon = Assets.DetectiveIcon
    };

    public override void OnVotingComplete()
    {
        Suspects.Clear();
    }

    public override void Initialize(PlayerControl player)
    {
        if (player == PlayerControl.LocalPlayer)
        {
            RoleBehaviourStubs.Initialize(this, player);
            CustomButtonSingleton<Inspect>.Instance.Button.Show();
        }
    }

    public void RegenerateSuspectList()
    {
        Suspects.Clear();
        foreach (var p in Helpers.GetClosestPlayers(Player.GetTruePosition(), 7f)
                     .Where(x => x != Player && !x.Data.IsDead))
            Suspects.Add(p);
    }

    public void SetUpVoteArea(PlayerVoteArea area)
    {
        var Indicator = Instantiate(area.XMark, area.XMark.transform.parent);
        Indicator.gameObject.name = "DetectiveIndicator";
        Indicator.sprite = Assets.DetectiveIndicator.LoadAsset();
        Indicator.gameObject.SetActive(true);
    }
}