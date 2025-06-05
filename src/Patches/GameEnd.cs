using HarmonyLib;
using MiraAPI.Utilities;
using MiraAPI.Roles;
using MiraAPI.GameOptions;
using ReachForStars.GameEndSettings;
using System.Linq;

namespace ReachForStars
{
    [HarmonyPatch(typeof(LogicGameFlowNormal), nameof(LogicGameFlowNormal.CheckEndCriteria))]
    public class GameEndPatches
    {
        /// Heavy rewrite of CheckEndCriteria
        public static bool Prefix(LogicGameFlowNormal __instance)
        {
            //Crew Tasks
            if (OptionGroupSingleton<GameEndOptions>.Instance.TaskWinToggle && !TutorialManager.InstanceExists)
            {
                __instance.Manager.CheckEndGameViaTasks();
            }

            //Neutrals stop gameEnd
            if (Helpers.GetAlivePlayers().Where(x => x.Data.Role.IsImpostor).Count() >= Helpers.GetAlivePlayers().Where(x => !x.Data.Role.IsImpostor).Count() && Helpers.GetAlivePlayers().Where(x => x.Data.Role is ICustomRole customRole && customRole.Team == ModdedRoleTeams.Custom).Count() == 0 && !OptionGroupSingleton<GameEndOptions>.Instance.NeutralsStopGameEnd)
            {
                __instance.Manager.RpcEndGame(GameOverReason.ImpostorByKill, false);
            }

            if (Helpers.GetAlivePlayers().Where(x => x.Data.Role.IsImpostor == false).Count() == Helpers.GetAlivePlayers().Count() && !TutorialManager.InstanceExists)
            {
                __instance.Manager.RpcEndGame(GameOverReason.HumansByVote, false);
            }
            return false;
        }
    }
}
