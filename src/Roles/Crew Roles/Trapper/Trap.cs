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

            HudManager.Instance.StartCoroutine(Effects.Bounce(gameObject.transform));
            if (PlayerControl.LocalPlayer.Data.Role.IsImpostor) myRend.color = new Color(1f, 1f, 1f, 0.3f);
            else if (!PlayerControl.LocalPlayer.Data.Role.IsImpostor && PlayerControl.LocalPlayer.Data.Role is not TrapperRole) myRend.color = new Color(1f, 1f, 1f, 0f);
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
            HudManager.Instance.StartCoroutine(Effects.Bounce(gameObject.transform, 0.4f, 0.25f));
            p.AddModifier<StunnedModifier>().Trapper = Trapper;
            myAnim.runtimeAnimatorController = Assets.TrapCloseAnimationController.LoadAsset();
            HasBeenTriggered = true;
            if (PlayerControl.LocalPlayer == p || PlayerControl.LocalPlayer == Trapper)
            {
                SoundManager.Instance.PlaySound(Assets.TrapCloseSfx.LoadAsset(), false);
                myRend.color = Color.white;
                Coroutines.Start(SetUpArrow(p));
            }
        }
        public IEnumerator SetUpArrow(PlayerControl p)
        {
            ArrowBehaviour arrow = Helpers.CreateArrow(null, Color.white);
            arrow.transform.localScale *= new Vector2(2f, 2f);
            arrow.target = p.GetTruePosition();
            arrow.gameObject.AddComponent<Animator>().runtimeAnimatorController = Assets.TrapArrowAnimationController.LoadAsset();
            yield return new WaitForSeconds(6f);
            arrow.gameObject.DestroyImmediate();
            yield break;      
        }
    }
}
