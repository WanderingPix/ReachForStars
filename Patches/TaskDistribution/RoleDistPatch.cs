using HarmonyLib;
using ReachForStars.MiscSettings;
using MiraAPI.GameOptions;
using UnityEngine.ProBuilder;

namespace ReachForStars
{
    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.AssignTaskIndexes))]
    public class TaskDistribution
    {
        public static bool Prefix(ShipStatus __instance)
        {
            int num = 0;

            if (OptionGroupSingleton<MiscOptions>.Instance.NoVents)
            {
                //__instance.ShortTasks.RemoveAt<NormalPlayerTask>();
            }
            for (int i = 0; i < __instance.CommonTasks.Length; i++)
            {
                __instance.CommonTasks[i].Index = num++;
            }
            for (int j = 0; j < __instance.LongTasks.Length; j++)
            {
                __instance.LongTasks[j].Index = num++;
            }
            for (int k = 0; k < __instance.ShortTasks.Length; k++)
            {
                __instance.ShortTasks[k].Index = num++;
            }
            return false;
        }
    }
}