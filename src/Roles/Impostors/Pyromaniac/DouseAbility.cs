using Il2CppSystem;
using MiraAPI.Hud;
using MiraAPI.Modifiers;
using MiraAPI.Utilities;
using MiraAPI.Utilities.Assets;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Pyromaniac;

public class DouseAbility : CustomActionButton<PlayerControl>
{
    protected override void OnClick()
    {
        Target?.RpcAddModifier<DousedModifier>(PlayerControl.LocalPlayer);
    }

    public override bool Enabled(RoleBehaviour role)
    {
        return role is  PyromaniacRole;
    }

    public override string Name => "Douse";

    public override float Cooldown => 3;

    public override LoadableAsset<Sprite> Sprite => Assets.PlaceHolder;

    public override PlayerControl GetTarget()
    {
        return PlayerControl.LocalPlayer.GetClosestPlayer(false, Distance);
    }

    public override void SetOutline(bool active)
    {
        Target?.cosmetics.SetOutline(active, new Nullable<Color>(Palette.ImpostorRed));
    }
}