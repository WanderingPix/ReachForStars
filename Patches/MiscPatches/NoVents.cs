using Reactor.Utilities.Extensions;
using MiraAPI.GameOptions;
using ReachForStars.MiscSettings;

namespace ReachForStars
{   
    public static class NoVents
    {
        /// <summary>
        /// Executed by Patches/OnCutsceneBreak.cs
        /// </summary>
        public static void TryBreakVent()
        {
            if (OptionGroupSingleton<MiscOptions>.Instance.NoVents)
            {
                foreach (var vent in ShipStatus.Instance.AllVents)
                {
                    vent.gameObject.DestroyImmediate();
                }
            }
        }
    }
}