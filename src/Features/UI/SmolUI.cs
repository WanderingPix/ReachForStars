using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ReachForStars.Features
{
    public static class SmolUI
    {
        public static void ResizeUI()
        {
            foreach (ActionButton button in HudManager.Instance.GetComponentsInChildren<ActionButton>(true))
            {
                button.gameObject.transform.localScale *= 0.8f; //Once chipseq's client settings pr is merged, add a slider for this 
            }
            foreach (GridArrange arrange in HudManager.Instance.transform.FindChild("Buttons").GetComponentsInChildren<GridArrange>(true))
            {
                arrange.CellSize *= new Vector2(0.8f, 0.8f);
            }
        }
    }
}