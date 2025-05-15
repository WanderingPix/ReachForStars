using System.Collections.Generic;
using MiraAPI.Hud;
using MiraAPI.Roles;
using UnityEngine;
using ReachForStars.Translation;

namespace ReachForStars.Roles.Impostors.Mole;


public class MoleRole : ImpostorRole, ICustomRole
{
    public string RoleName => rolename.GetTranslatedText();
    public TranslationPool rolename = new TranslationPool(
    english: "Mole",
    spanish: "Topo",
    portuguese: "Verruga",
    french: "Taupe", 
    italian: "Talpa"
    );
    public string RoleDescription => "Dig vents around the map";
    public string RoleLongDescription => RoleDescription;
    public Color RoleColor => Palette.ImpostorRed;
    public ModdedRoleTeams Team => ModdedRoleTeams.Impostor;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        UseVanillaKillButton = true,
        CanGetKilled = true,
        CanUseVent = true,
    };

    public override void SpawnTaskHeader(PlayerControl playerControl)
    {
        // remove existing task header.
    }

    public List<Vent> PlacedVents;

    public override bool DidWin(GameOverReason gameOverReason)
    {
        return GameManager.Instance.DidImpostorsWin(gameOverReason);
    }
    public override void OnMeetingStart()
    {
        CustomButtonSingleton<Dig>.Instance.IncreaseUses(1);
    }
}
