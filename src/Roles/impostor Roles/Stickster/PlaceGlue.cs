using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using ReachForStars.Networking;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Networking;
using System.Collections.Generic;
using UnityEngine.UI;
using ReachForStars.Utilities;
using Reactor.Utilities;
using ReachForStars.Translation;

namespace ReachForStars.Roles.Impostors.Stickster;
public class PlaceGlue : CustomActionButton
{
    public override string Name => buttonName.GetTranslatedText();
    public TranslationPool buttonName = new TranslationPool(
    english: "Glue",
    spanish: "",
    portuguese: "",
    french: "",
    russian: "Клей",
    italian: ""
    );

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
