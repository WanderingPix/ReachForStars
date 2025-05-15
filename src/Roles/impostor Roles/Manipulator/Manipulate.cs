using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Modifiers;
using MiraAPI.GameOptions;
using ReachForStars.Translation;
using Rewired;

namespace ReachForStars.Roles.Impostors.Manipulator;
public class Manipulate : CustomActionButton<PlayerControl>
{
    public override string Name => btnName.GetTranslatedText();
    public TranslationPool btnName = new TranslationPool
    (
        english: "Manipulate",
        french: "Manipuler",
        spanish: "Manipular",
        portuguese: "Manipular",
        russian: "",
        italian: ""
    );

    public override float Cooldown => 5;
    public override float EffectDuration => 25f;

    public override ButtonLocation Location => ButtonLocation.BottomLeft;

    public override int MaxUses => 0;

    public override LoadableAsset<Sprite> Sprite => Assets.PoisonButton;
    public PlayerControl CurrentlyManipulatedPlayer;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is ManipulatorRole;
    }

    public override PlayerControl? GetTarget()
    {
        return PlayerControl.LocalPlayer.GetClosestPlayer(true, Distance, false);
    }

    public override void SetOutline(bool active)
    {
        Target?.cosmetics.SetOutline(active, new Il2CppSystem.Nullable<Color>(Palette.ImpostorRed));
    }

    public override bool IsTargetValid(PlayerControl? target)
    {
        return true;
    }
    public KillOverlay overlay;
    protected override void OnClick()
    {
        CurrentlyManipulatedPlayer = Target;
        PlayerControl.LocalPlayer.RpcAddModifier<ManipulatedModifier>();
        Target.RpcAddModifier<ManipulatedModifier>();
    }

    public override void OnEffectEnd()
    {
        overlay = Object.Instantiate<KillOverlay>(HudManager.Instance.KillOverlay, HudManager.Instance.transform);
        overlay.transform.localScale = new Vector3(0.5f, 0.5f, 1f);

        SoundManager.Instance.PlaySound(PlayerControl.LocalPlayer.KillSfx, false, 0.5f);
        if (CurrentlyManipulatedPlayer.Data.IsDead) overlay.ShowKillAnimation(PlayerControl.LocalPlayer.Data, CurrentlyManipulatedPlayer.Data);

        CurrentlyManipulatedPlayer = null;
    }
}
