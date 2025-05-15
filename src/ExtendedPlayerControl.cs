using System.Collections.Generic;
using AmongUs.GameOptions;
using Reactor.Utilities.Attributes;
using UnityEngine;

namespace ReachForStars
{
    public class ExtendedPlayerControl : MonoBehaviour
    {
        public PlayerControl parent;
        public DeathReason deathReason;
        public PlayerControl Killer;
        public List<RoleTypes> RoleHistory;
        
    }
}