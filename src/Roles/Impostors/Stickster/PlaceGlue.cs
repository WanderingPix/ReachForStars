using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using ReachForStars.Networking;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Stickster;

public class PlaceGlue : CustomActionButton
{
    public TranslationPool buttonName = new(
        "Glue",
        "Cola",
        "Colle",
        "Клей"
        //italian: "Colla"
    );

    public override string Name => buttonName.GetTranslatedText();

    public override float Cooldown => 10;
    public override float EffectDuration => 0;

    public override ButtonLocation Location => ButtonLocation.BottomRight;

    public override int MaxUses => 0;

    public override LoadableAsset<Sprite> Sprite => Assets.Glue;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is SticksterRole;
    }

    protected override void OnClick()
    {
        PlayerControl.LocalPlayer.RpcPlaceGlue();
    }
}