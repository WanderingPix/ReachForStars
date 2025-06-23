using Il2CppSystem;
using MiraAPI.Hud;
using MiraAPI.Networking;
using MiraAPI.Utilities;
using MiraAPI.Utilities.Assets;
using ReachForStars.Features;
using UnityEngine;

namespace ReachForStars.Roles.Neutrals.Exiled;

public class ExileKill : CustomActionButton<PlayerControl>
{
    public override string Name => "kill";

    public override float Cooldown => 0;
    public override float EffectDuration => 0;

    public override int MaxUses => 1;

    public override LoadableAsset<Sprite> Sprite => Assets.RedKillButton;

    public override ButtonLocation Location => ButtonLocation.BottomRight;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is Exiled;
    }

    public override PlayerControl? GetTarget()
    {
        return PlayerControl.LocalPlayer.GetClosestPlayer(true, Distance);
    }

    public override void SetOutline(bool active)
    {
        Target?.cosmetics.SetOutline(active, new Nullable<Color>(RFSPalette.ExiledColor));
    }

    public override bool IsTargetValid(PlayerControl? target)
    {
        return true;
    }

    protected override void OnClick()
    {
        if (PlayerControl.LocalPlayer.Data.Role is Exiled exiled)
        {
            if (exiled.EnemyTeam == ExiledEnemyTeam.Impostors && Target.Data.Role.IsImpostor)
            {
                PlayerControl.LocalPlayer.RpcCustomMurder(Target);
                HudManager.Instance.StartCoroutine(Effects.ScaleIn(Button.transform, 1.4f, 0.7f, 0.7f));
            }
            else if (!Target.Data.Role.IsImpostor && exiled.EnemyTeam == ExiledEnemyTeam.Crewmates)
            {
                PlayerControl.LocalPlayer.RpcCustomMurder(Target);
                HudManager.Instance.StartCoroutine(Effects.ScaleIn(Button.transform, 1.4f, 0.7f * SmolUI.ScaleFactor,
                    0.7f));
            }
            else
            {
                HudManager.Instance.StartCoroutine(Effects.SwayX(Button.transform, 0.7f));
            }
        }
    }

    public override void OnEffectEnd()
    {
    }
}