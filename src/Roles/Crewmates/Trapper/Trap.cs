using MiraAPI.Events;
using MiraAPI.Events.Vanilla.Meeting;
using MiraAPI.Modifiers;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Trapper;

public class Trap : MonoBehaviour
{
    public PlayerControl Trapper;
    private bool HasBeenTriggered;
    private Animator myAnim;
    private SpriteRenderer myRend;

    public void Start()
    {
        myRend = gameObject.GetComponent<SpriteRenderer>();
        myAnim = gameObject.GetComponent<Animator>();

        HudManager.Instance.StartCoroutine(Effects.Bounce(gameObject.transform));
        if (PlayerControl.LocalPlayer.Data.Role.IsImpostor) myRend.color = new Color(1f, 1f, 1f, 0.3f);
        else if (!PlayerControl.LocalPlayer.Data.Role.IsImpostor &&
                 PlayerControl.LocalPlayer.Data.Role is not TrapperRole) myRend.color = new Color(1f, 1f, 1f, 0f);
    }

    public void FixedUpdate()
    {
        foreach (var player in PlayerControl.AllPlayerControls)
            if (ShouldTrigger(player) && Vector2.Distance(player.GetTruePosition(), transform.position) < 1f)
                Trigger(player);
    }

    [RegisterEvent]
    public static void OnMeetingEnd(EndMeetingEvent @event)
    {
        foreach (var Trap in FindObjectsOfType<Trap>()) Trap.gameObject.DestroyImmediate();
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
        }
    }
}