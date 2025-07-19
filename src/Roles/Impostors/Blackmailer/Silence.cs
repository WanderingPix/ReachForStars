using Il2CppSystem;
using MiraAPI.Hud;
using MiraAPI.Modifiers;
using MiraAPI.Utilities;
using MiraAPI.Utilities.Assets;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Blackmailer;

public class Silence : CustomActionButton<PlayerControl>
{
    public TranslationPool name = new
    (
        "Silence",
        french: "Silence",
        spanish: "",
        russian: ""
    );

    public override string Name => name.GetTranslatedText();

    public override float Cooldown => 30;

    public override ButtonLocation Location => ButtonLocation.BottomRight;

    public override int MaxUses => 0;

    public override float Distance => 1f;


    public override LoadableAsset<Sprite> Sprite => Assets.FreezeButton;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is BlackmailerRole;
    }

    public override void SetOutline(bool active)
    {
        Target?.cosmetics.SetOutline(active, new Nullable<Color>(Palette.ImpostorRed));
    }

    public override PlayerControl? GetTarget()
    {
        return PlayerControl.LocalPlayer.GetClosestPlayer(true, Distance, false);
    }

    public override bool IsTargetValid(PlayerControl? target)
    {
        return (target != null && target.Data.IsDead == false && !target.HasModifier<BlackmailedModifier>());
    }

    protected override void OnClick()
    {
        Target.RpcAddModifier<BlackmailedModifier>();
        var notification = Helpers.CreateAndShowNotification(
            $"<color=#{ColorUtility.ToHtmlStringRGBA(Target.Data.Color)}>{Target.Data.PlayerName}</color> has been silenced!\nThey will not be able to talk next meeting!",
            Palette.ImpostorRed, null);
        notification.transform.localPosition = new Vector3(0f, 1f, -20f);
    }
}