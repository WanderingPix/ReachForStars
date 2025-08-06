using System.Linq;
using MiraAPI.Events;
using MiraAPI.Events.Vanilla.Gameplay;
using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using ReachForStars.Modifiers;
using ReachForStars.Utilities;
using Reactor.Utilities;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Mole;

public class TunnelingModifier : TimedModifier
{
    public override string ModifierName => "Tunneling";
    public override float Duration => 10;


    public override void OnActivate()
    {
        CreateVent().myAnim.m_animator.runtimeAnimatorController = Assets.VentDigAnimController.LoadAsset();
        if (Player == PlayerControl.LocalPlayer)
            SoundManager.Instance.PlaySound(Assets.TunnelingSFX.LoadAsset(),
                true); //If localplayer, play the sfx at 1f volume
        else
            SoundManager.Instance.PlaySound(Assets.TunnelingSFX.LoadAsset(), true,
                0.2f); //else, play the sfx at 0.2 volume
        Player.AddModifier<InvisibleModifier>();

        if (Player != PlayerControl.LocalPlayer) return;

        HudManager.Instance.PlayerCam.ShakeScreen(Duration, 0.7f);
    }

    public override void OnDeactivate()
    {
        CreateVent().myAnim.m_animator.runtimeAnimatorController = Assets.VentDigAnimController.LoadAsset();
        Player.RemoveModifier<InvisibleModifier>();

        SoundManager.Instance.StopAllSound();
        base.OnTimerComplete();
    }

    private Vent CreateVent()
    {
        if (Player.Data.Role is MoleRole mole)
        {
            var prefab = UnityObject.FindObjectOfType<Vent>(true);
            var vent = UnityObject.Instantiate(prefab);
            Vector3 ppos = Player.GetTruePosition();
            vent.transform.localPosition = new Vector3(1.2f, 1.2f, 1);
            vent.transform.position = new Vector3(ppos.x, ppos.y, 1f);

            PluginSingleton<ReachForStars>.Instance.Log.LogDebug("Managed to create vent!");

            SoundManager.Instance.PlaySoundAtLocation(Assets.DigSfx.LoadAsset(), ppos,
                PlayerControl.LocalPlayer.GetTruePosition(), SoundManager.Instance.SfxChannel);

            PluginSingleton<ReachForStars>.Instance.Log.LogDebug("Managed to create animator!");
            vent.gameObject.name = $"MoleVent{mole.MinedVents.Count()}";

            vent.Id = VentUtils.GetAvailableId();
            vent.Center = null;

            var newAllVents = ShipStatus.Instance.AllVents.ToList();
            newAllVents.Add(vent);
            ShipStatus.Instance.AllVents = newAllVents.ToArray();
            if (mole.MinedVents.Count > 0)
            {
                vent.Left = mole.MinedVents.Last();
                mole.MinedVents.Last().Right = vent;
                mole.MinedVents.First().Right = vent;
            }
            else
            {
                vent.Left = null;
                vent.Right = null;
            }

            vent.Center = null;
            vent.gameObject.GetComponent<VentCleaningConsole>()?.DestroyImmediate();

            mole.MinedVents.Add(vent);

            return vent;
        }

        return null; //if player somehow isn't mole
    }

    [RegisterEvent]
    public static void OnMurder(AfterMurderEvent e)
    {
        if (e.Source.HasModifier<TunnelingModifier>()) e.Source.RemoveModifier<TunnelingModifier>();
    }
}