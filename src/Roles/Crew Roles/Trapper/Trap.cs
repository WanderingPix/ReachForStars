using UnityEngine;
using MiraAPI.Modifiers;

namespace ReachForStars.Roles.Crewmates.Trapper
{
    public class Trap : MonoBehaviour
    {
        public bool ShouldTrigger(PlayerControl p)
        {
            return p.Data.Role.IsImpostor && !p.HasModifier<StunnedModifier>();
        }
        public void Trigger(PlayerControl p)
        {
            p.AddModifier<StunnedModifier>();
            Coroutines.Start();
        }
        public void FixedUpdate()
        {
            //UNFINISHED CHECK FOR NEARBY PLAYERS OWO
        }
        public System.Collections.IEnumerator DoTriggerAnim()
        {
            //UNFINISHED
        }
    }
}
