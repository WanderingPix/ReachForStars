using UnityEngine;
using MiraAPI.Modifiers;
using Reactor.Utilities;
using MiraAPI.Events;
using Reactor.Utilities.Extensions;
using MiraAPI.Utilities.Assets;
using MiraAPI.Utilities;
using System.Collections;

namespace ReachForStars.Roles.Crewmates.Trapper
{
    public class Trap : MonoBehaviour
    {
        SpriteRenderer myRend;
        Animator myAnim;
        public PlayerControl Trapper;
        bool HasBeenTriggered = false;
        public void FixedUpdate()
        {
            foreach (var player in PlayerControl.AllPlayerControls)
            {
                if (ShouldTrigger(player) && Vector2.Distance(player.GetTruePosition(), transform.position) < 1f)
                {
                    Trigger(player);
                }
            }
        }
        public void Start()
        {
            myRend = gameObject.GetComponent<SpriteRenderer>();
            myAnim = gameObject.GetComponent<Animator>();
        }
        [RegisterEvent]
        public static void OnMeetingEnd(MiraAPI.Events.Vanilla.Meeting.EndMeetingEvent @event)
        {
            foreach (var Trap in Object.FindObjectsOfType<Trap>())
            {
                Trap.gameObject.DestroyImmediate();
            }
        }
        public bool ShouldTrigger(PlayerControl p)
        {
            return p.Data.Role.IsImpostor && !p.HasModifier<StunnedModifier>() && !HasBeenTriggered;
        }
        public void Trigger(PlayerControl p)
        {
            p.AddModifier<StunnedModifier>();
            myAnim.runtimeAnimatorController = Assets.TrapCloseAnimationController.LoadAsset();
            HasBeenTriggered = true;
            //if (PlayerControl.LocalPlayer == p || PlayerControl.LocalPlayer == Trapper) SetUpArrow();
        }
    }
}
