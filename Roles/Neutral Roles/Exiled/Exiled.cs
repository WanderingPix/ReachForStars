using MiraAPI.Roles;
using UnityEngine;
using System.Collections.Generic;
using System;
using Random = System.Random;
using MiraAPI.Patches.Stubs;
using MiraAPI.Hud;

namespace ReachForStars.Roles.Neutrals.Exiled
{
    public class Exiled : ImpostorRole, ICustomRole
    {
        public string RoleName => "Exiled";
        public string EnemyTeam = "";
        public string RoleDescription => $"Make sure the {EnemyTeam}lose at all costs!";
        public string RoleLongDescription => RoleDescription;
        public Color RoleColor => Color.yellow;
        public ModdedRoleTeams Team => ModdedRoleTeams.Custom;

        public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
        {
            UseVanillaKillButton = false,
            CanGetKilled = true,
            CanUseVent = false,
        };

        public override void SpawnTaskHeader(PlayerControl playerControl)
        {
            // remove existing task header.
        }

        public override bool DidWin(GameOverReason gameOverReason)
        {
            if (EnemyTeam == "crewmates")
            {
                return !GameManager.Instance.DidHumansWin(gameOverReason);
            }
            else
            {
                return !GameManager.Instance.DidImpostorsWin(gameOverReason);
            }
        }
        public override void Initialize(PlayerControl player)
        {
            if (EnemyTeam == "")
            {
                Random random = new Random();
                List<string> strings = new List<string> { "crewmates", "impostors" };
                EnemyTeam = strings[random.Next(strings.Count)];

                RoleBehaviourStubs.Initialize(this, player);

                
                GameObject Button = CustomButtonSingleton<ExileKill>.Instance.Button.gameObject;
                Button.transform.SetParent(HudManager.Instance.transform);
                AspectPosition pos = Button.gameObject.AddComponent<AspectPosition>();
                pos.Alignment = AspectPosition.EdgeAlignments.Center;
                pos.DistanceFromEdge = new Vector3(0f, -1.75f, 0f);
            }
        }
    }
}
