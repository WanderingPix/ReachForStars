using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Modifiers;
using MiraAPI.GameOptions;
using ReachForStars.Translation;
using Rewired;
using MiraAPI.Networking;

namespace ReachForStars.Roles.Impostors.Manipulator;
public class ManipulatedKill : CustomActionButton<PlayerControl>
{
    public override string Name => btnName.GetTranslatedText();
    public TranslationPool btnName = new TranslationPool
    (
        english: "Kill",
        french: "Tuer",
        spanish: "Matar",
        portuguese: "Matar",
        russian: "",
        italian: ""
    );

    public override float Cooldown => 0;
    public override float EffectDuration => 0f;

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
        PlayerControl.LocalPlayer.RpcCustomMurder(Target);
        PlayerControl.LocalPlayer.GetModifier<ManipulatedModifier>().OnTimerComplete();
    }

    
}
