using Il2CppSystem;
using MiraAPI.Hud;
using MiraAPI.Networking;
using MiraAPI.Utilities;
using MiraAPI.Utilities.Assets;
using ReachForStars.Features;
using ReachForStars.Roles.Neutrals.Roles;
using UnityEngine;

namespace ReachForStars.Roles.Neutrals.BountyHunter;

public class BountyKill : CustomActionButton<PlayerControl>
{
    public override string Name => "kill";

    public override float Cooldown => 15f;
    public override float EffectDuration => 0;

    public override int MaxUses => 0;

    public override LoadableAsset<Sprite> Sprite => Assets.RedKillButton;

    public override ButtonLocation Location => ButtonLocation.BottomRight;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is BountyHunterRole;
    }

    public override PlayerControl? GetTarget()
    {
        return PlayerControl.LocalPlayer.GetClosestPlayer(true, Distance);
    }

    public override void SetOutline(bool active)
    {
        Target?.cosmetics.SetOutline(active, new Nullable<Color>(RFSPalette.BountyHunterColor));
    }

    public override bool IsTargetValid(PlayerControl? target)
    {
        return PlayerControl.LocalPlayer.Data.Role is BountyHunterRole BH && BH.Target == target;
    }

    protected override void OnClick()
    {
        var BH = PlayerControl.LocalPlayer.Data.Role.TryCast<BountyHunterRole>();
        if (BH)
        {
            PlayerControl.LocalPlayer.RpcCustomMurder(Target);
            HudManager.Instance.StartCoroutine(Effects.ScaleIn(Button.transform, 1.4f, 0.7f * SmolUI.ScaleFactor,
                0.7f));
            BH.OnTargetKill();
        }
    }

    public override void OnEffectEnd()
    {
    }
}