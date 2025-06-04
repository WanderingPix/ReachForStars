using UnityEngine;
using MiraAPI.Modifiers;
using Reactor.Utilities;
using MiraAPI.Events;
using Reactor.Utilities.Extensions;

namespace ReachForStars.Roles.Crewmates.Trapper
{
    public class Trap : MonoBehaviour
    {
        SpriteRenderer myRend;
        bool HasBeenTriggered = false;
        public void Start()
        {
            myRend = gameObject.AddComponent<SpriteRenderer>();
            myRend.sprite = Assets.TrapOpen.LoadAsset();
        }
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
            Coroutines.Start(DoTriggerAnim());

            HasBeenTriggered = true;
        }
        public System.Collections.IEnumerator DoTriggerAnim()
        {
            HudManager.Instance.StartCoroutine(Effects.Bounce(gameObject.transform, 1f, 0.2f));
            HudManager.Instance.StartCoroutine(Effects.Rotate2D(gameObject.transform, 0f, -45f, 0.3f));
            myRend.sprite = Assets.TrapClosed.LoadAsset();
            yield return new WaitForSeconds(0.25f);
            HudManager.Instance.StartCoroutine(Effects.Rotate2D(gameObject.transform, -45f, 45f, 0.3f));
            yield return new WaitForSeconds(0.1f);
            HudManager.Instance.StartCoroutine(Effects.Rotate2D(gameObject.transform, -45f, 45f, 0.1f));
            yield break;
        }
    }
}
