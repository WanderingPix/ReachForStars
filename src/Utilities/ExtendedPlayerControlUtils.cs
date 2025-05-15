using System.Collections.Generic;
using Reactor.Utilities.Attributes;
using UnityEngine;

namespace ReachForStars
{
    public static class ExtendedPlayerControlUtils
    {
        public static ExtendedPlayerControl GetExtendedPlayerControl(this PlayerControl p)
        {
            return p.GetComponent<ExtendedPlayerControl>();
        }
    }
}