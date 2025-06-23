using AmongUs.GameOptions;
using Il2CppSystem;
using MiraAPI.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Networking;
using MiraAPI.Utilities;
using MiraAPI.Utilities.Assets;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Sheriff;

public class Shoot : CustomActionButton<PlayerControl>
{
    public TranslationPool ButtonName = new(
        "Shoot",
        "Disparar",
        "Tirer",
        "выстрелить"
        //italian: "Sparare"
    );

    public override string Name => ButtonName.GetTranslatedText();
    public override float Cooldown => 25;
    public override float EffectDuration => 0;

    public override int MaxUses => (int)OptionGroupSingleton<SheriffOptions>.Instance.BulletCount;

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
        if (Target.Data.Role.IsImpostor)
        {
            PlayerControl.LocalPlayer.RpcCustomMurder(Target);
            HudManager.Instance.StartCoroutine(Effects.ScaleIn(Button.transform, 1.4f, 0.7f, 0.7f));
        }
        else if (!Target.Data.Role.IsImpostor)
        {
            switch (OptionGroupSingleton<SheriffOptions>.Instance.Consequence)
            {
                case MisfireResults.Demote:
                    PlayerControl.LocalPlayer.RpcSetRole(RoleTypes.Crewmate, true);
                    break;
                case MisfireResults.Suicide:
                    PlayerControl.LocalPlayer.RpcCustomMurder(PlayerControl.LocalPlayer, true, true, true, false,
                        false);
                    PlayerControl.LocalPlayer.RpcCustomMurder(Target, true, true, true, false, false);
                    break;
                case MisfireResults.None:
                    break;
            }
        }
    }
}