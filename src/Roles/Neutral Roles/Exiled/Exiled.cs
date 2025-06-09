using MiraAPI.Roles;
using UnityEngine;
using MiraAPI.Patches.Stubs;
using MiraAPI.Utilities;
using MiraAPI.Hud;

namespace ReachForStars.Roles.Neutrals.Exiled
{
    public class Exiled : ImpostorRole, ICustomRole
    {
        public string RoleName => "Exiled";
        public string RoleDescription => $"Make sure {EnemyTeam} lose at all costs!";
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
            if (EnemyTeam == ExiledEnemyTeam.Crewmates)
            {
                return !GameManager.Instance.DidHumansWin(gameOverReason);
            }
            else
            {
                return !GameManager.Instance.DidImpostorsWin(gameOverReason);
            }
        }
        public ExiledEnemyTeam EnemyTeam;
        public override void Initialize(PlayerControl player)
        {
            RoleBehaviourStubs.Initialize(this, player);
            if (player == PlayerControl.LocalPlayer)
            {
                CustomButtonSingleton<ExileKill>.Instance.Button.Show();
                bool CheckChance = Helpers.CheckChance(50);
                if (CheckChance)
                {
                    EnemyTeam = ExiledEnemyTeam.Crewmates;
                }
                else
                {
                    EnemyTeam = ExiledEnemyTeam.Impostors;
                }
            }
        }
    }
    public enum ExiledEnemyTeam
    {
        Crewmates,
        Impostors
    }
}
