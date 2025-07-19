using Il2CppSystem;
using MiraAPI.Hud;
using MiraAPI.Utilities;
using MiraAPI.Utilities.Assets;
using ReachForStars.Roles.Crewmates.Sheriff;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Jailor;

public class Jail : CustomActionButton<PlayerControl>
{
    public TranslationPool ButtonName = new(
        "Jail",
        "",
        "",
        ""
    );

    public override string Name => ButtonName.GetTranslatedText();
    public override float Cooldown => 25;
    public override float EffectDuration => 0;

    public override LoadableAsset<Sprite> Sprite => Assets.Shoot;
    public override ButtonLocation Location => ButtonLocation.BottomRight;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is SheriffRole;
    }

    public override PlayerControl? GetTarget()
    {
        return PlayerControl.LocalPlayer.GetClosestPlayer(true, Distance);
    }

    public override void SetOutline(bool active)
    {
        Target?.cosmetics.SetOutline(active, new Nullable<Color>(new Color(1f, 1f, 0f, 1f)));
    }

    public override bool IsTargetValid(PlayerControl? target)
    {
        return true;
    }

    protected override void OnClick()
    {
        //PlayerControl.LocalPlayer.RpcJail(Target);
    }
}