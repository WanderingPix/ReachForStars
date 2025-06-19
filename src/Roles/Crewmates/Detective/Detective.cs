using MiraAPI.Hud;
using MiraAPI.Patches.Stubs;
using MiraAPI.Roles;
using MiraAPI.Utilities;
using ReachForStars.Translation;
using Rewired;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Detective;


public class DetectiveRole : CrewmateRole, ICustomRole
{
    public string RoleName => roleName.GetTranslatedText();
    public TranslationPool roleName = new
    (
        english: "Detective",
        spanish: "Detective",
        french: "Detective",
        russian: "Детектив"
        //italian: "Ispettore"
    );
    public string RoleLongDescription => roleDescLong.GetTranslatedText();
    public TranslationPool roleDescLong = new
    (
        english: "Inspect dead bodies to find out who was nearby!",
        spanish: "¡Inspecciona los cadáveres para averiguar quién estaba cerca!",
        french: "Inspectez des cadavres pour decouvrir qui etait proche!",
        russian: "Иследуй трупы чтобы найти, кто был близко!"
        //italian: "Osseva i cadaveri per ricevere indizi!"
    );
    public string RoleDescription => roleDescLong.GetTranslatedText();
    public Color RoleColor => Palette.CrewmateRoleHeaderBlue;
    public Color OptionsMenuColor => Palette.CrewmateRoleHeaderBlue;
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;

    public List<PlayerControl> Suspects { get; set; } = new();
    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
    };
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
        foreach (PlayerControl p in Helpers.GetClosestPlayers(Player.GetTruePosition(), 6f, true).Where(x => x != Player && !x.Data.IsDead))
        {
            Suspects.Add(p);
        }
    }
    public void SetUpVoteArea(PlayerVoteArea area)
    {
        SpriteRenderer Indicator = Object.Instantiate<SpriteRenderer>(area.XMark, area.XMark.transform.parent);
        Indicator.gameObject.name = "DetectiveIndicator";
        Indicator.sprite = Assets.DetectiveIndicator.LoadAsset();
        Indicator.gameObject.SetActive(true);
    }
}
