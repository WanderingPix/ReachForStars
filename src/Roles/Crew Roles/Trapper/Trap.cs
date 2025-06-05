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
                NoisemakerArrow arrow = Object.Instantiate(RoleManager.Instance.GetRole(AmongUs.GameOptions.RoleTypes.Noisemaker).Cast<NoisemakerRole>().deathArrowPrefab).GetComponent<NoisemakerArrow>();
            }
        }
        public System.Collections.IEnumerator DoTriggerAnim()
        {
            myRend.sprite = Assets.Trap0.LoadAsset();
            yield return new WaitForSeconds(0.125f);
            myRend.sprite = Assets.Trap1.LoadAsset();
            yield return new WaitForSeconds(0.125f);
            myRend.sprite = Assets.Trap2.LoadAsset();
            yield return new WaitForSeconds(0.125f);
            myRend.sprite = Assets.Trap3.LoadAsset();
            yield return new WaitForSeconds(0.125f);
            //Play sound TBD
            yield break;
        }
    }
}
