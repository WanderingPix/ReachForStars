using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using MiraAPI.Networking;
using MiraAPI.Utilities;
using ReachForStars.Utilities;

namespace ReachForStars.Roles.Impostors.Electroman;

public class ElectrocutedModifier : TimedModifier
{
    public override string ModifierName { get; } = "Electrocuted";
    public override float Duration { get; } = 3f;

    public override void OnActivate()
    {
        SoundManager.Instance.PlaySoundAtLocation(Assets.ElectricSound.LoadAsset(), Player.GetTruePosition(),
            PlayerControl.LocalPlayer.GetTruePosition(), SoundManager.Instance.SfxChannel);
        Player.cosmetics.currentPet.SetScared();
        Player.NetTransform.Halt();
        Player.moveable = false;
        //Player.cosmetics.Toggle(false, true, false, false, true);
        Player.AnimateCustom(Assets.ElectrocutedAnimation.LoadAsset());
    }

    public override void FixedUpdate()
    {
        var ClosestPlayer =
            Player.GetClosestPlayer(false, 1.2f, false, x => !x.HasModifier<ElectrocutedModifier>());
        if (ClosestPlayer) ClosestPlayer.AddModifier<ElectrocutedModifier>();
        base.FixedUpdate();
    }

    public override void OnTimerComplete()
    {
        Player.moveable = true;
        Player.cosmetics.Toggle(true, true, true, true, true);
        Player.CustomMurder(Player, MurderResultFlags.DecisionByHost, false, true, false, false);
    }
}