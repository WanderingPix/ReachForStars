using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

namespace ReachForStars.Patches
{
    [HarmonyPatch]
    public class GameEndPatches
    {
        [HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.Start))]
        [HarmonyPostfix]
        public static void CoBeginPostfix(EndGameManager __instance)
        {
            
        }
    }
}