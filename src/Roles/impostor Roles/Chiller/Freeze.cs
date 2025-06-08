using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using ReachForStars.Networking;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Networking;
using System.Collections.Generic;
using UnityEngine.UI;
using ReachForStars.Translation;

namespace ReachForStars.Roles.Impostors.Chiller;
public class Freeze : CustomActionButton<DeadBody>
{
    public override string Name => name.GetTranslatedText();
    public TranslationPool name = new
    (
        english: "Freeze",
        french: "Figer",
        spanish: "",
        portuguese: "",
        russian: "",
        italian: ""
    );

    public override float Cooldown => 0;

    public override ButtonLocation Location => ButtonLocation.BottomRight;

    public override int MaxUses => 0;

    public override float Distance => 2f;


    public override LoadableAsset<Sprite> Sprite => Assets.Freeze;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is FreezerRole;
    }
    public override bool IsTargetValid(DeadBody? target)
    {
        return true;
    }
    public override void SetOutline(bool active)
    {
        if (Target != null)
        {
            foreach (var rend in Target.bodyRenderers)
            {
                rend.UpdateOutline(active ? Palette.ImpostorRed : null);
            }
        }
    }
    public override DeadBody? GetTarget()
    {
        return PlayerControl.LocalPlayer.GetNearestDeadBody(Distance);
    }

    protected override void OnClick()
    {
        PlayerControl.LocalPlayer.RpcFreezeBody();
        SoundManager.Instance.PlaySound(Assets.FreezeSFX.LoadAsset(), false, 2f);
    }
}