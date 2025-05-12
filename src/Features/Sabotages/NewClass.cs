using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReachForStars.Features.Sabotages
{
    public class DisableAnnoyingImpostorsSabotagingWhileDead
    {
        public static void ToggleOffSabs()
        {
            HudManager.Instance.SabotageButton.Hide();
        }
    }
}