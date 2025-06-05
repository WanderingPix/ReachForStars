using UnityEngine;
using MiraAPI.Modifiers;
using Reactor.Utilities;
using MiraAPI.Events;
using Reactor.Utilities.Extensions;
using MiraAPI.Utilities.Assets;

namespace ReachForStars.Roles.Crewmates.Trapper
{
    public class Trap : MonoBehaviour
    {
        SpriteRenderer myRend;
        public PlayerControl Trapper;
        bool HasBeenTriggered = false;
        public void Start()
        {
            myRend = gameObject.AddComponent<SpriteRenderer>();
            myRend.sprite = Assets.Trap0.LoadAsset();
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
            if (PlayerControl.LocalPlayer == p || PlayerControl.LocalPlayer == Trapper)
            {
               
            }
        }
        public LoadableResourceAsset[] TrapSprites = new[]
        {
            Assets.Trap0,
            Assets.Trap1,
            Assets.Trap2
        };
        public System.Collections.IEnumerator DoTriggerAnim()
        {
            for (int i = 0; i == 3; i++)
            {
                myRend.sprite = TrapSprites[i].LoadAsset();
                yield return new WaitForSeconds(0.125f);
            }
            //SFX  
            yield break;
        }
    }
}
