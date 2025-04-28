using System.Collections.Generic;
using MiraAPI.Hud;
using MiraAPI.Roles;
using UnityEngine;
using ReachForStars.Translation;
using Il2CppSystem.Web.Util;
using MiraAPI.Utilities;

namespace ReachForStars.Roles.Impostors.Mole;


public class MoleRole : ImpostorRole, ICustomRole
{
    public string RoleName => rolename.GetTranslatedText();
    public TranslationPool rolename = new TranslationPool(
    english: "Mole",
    spanish: "Lunar",
    portuguese: "Verruga",
    french: "Taupe",
    russian: "Крот",
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
        public List<Vent> vents;

    public void PlaceVent(PlayerControl p)
    {
            Vent vent = Object.Instantiate<Vent>(Object.FindObjectOfType<Vent>(true)); 
            vents.Add(vent);
            vent.name = $"MoleVent{vents.Count + 1}";
            vent.Id = ShipStatus.Instance.AllVents.Count + vents.Count;        
            vent.transform.position = p.GetTruePosition();
            vent.Id = ShipStatus.Instance.AllVents.Count + vents.Count;
            vent.Right = null;
            if (vents.Count > 1)
            {
                vent.Right = vents[^1];
            }
            vents[1].Left = vent;
                
            //TODO: smoke cloud
                
            vent.StartCoroutine(Effects.Bounce(vent.transform, 1f));
            vent.StartCoroutine(Effects.ColorFade(vent.myRend, Palette.Black, Palette.White, 1.4f)); 
    }

    public override bool DidWin(GameOverReason gameOverReason)
    {
        return GameManager.Instance.DidImpostorsWin(gameOverReason);
    }
    public override void OnMeetingStart()
    {
        CustomButtonSingleton<Dig>.Instance.IncreaseUses(1);
    }
}
