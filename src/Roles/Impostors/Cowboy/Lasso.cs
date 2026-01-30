using MiraAPI.Hud;
using MiraAPI.Modifiers;
using MiraAPI.Utilities;
using MiraAPI.Utilities.Assets;
using ReachForStars.Networking;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Cowboy;

public class Lasso : CustomActionButton
{
    protected override void OnClick()
    {
        PlayerControl.LocalPlayer.transform.GetComponent<HnSImpostorScreamSfx>().LocalImpostorYeehaw();
        var Target = PlayerControl.LocalPlayer.GetClosestPlayer(false, 1000f, true);
        PlayerControl.LocalPlayer.RpcLasso(Target);
    }

    public override bool Enabled(RoleBehaviour role)
    {
        return role is CowboyRole;
    }

    public override string Name => "Lasso";

    public override float Cooldown => 30;

    public override float EffectDuration => 3;

    public override LoadableAsset<Sprite> Sprite => Assets.PlaceHolder;
}