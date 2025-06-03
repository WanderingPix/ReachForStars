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

namespace ReachForStars.Roles.Impostors.Arachnid;
public class PlaceCobweb : CustomActionButton
{
    public override string Name => buttonName.GetTranslatedText();
    public TranslationPool buttonName = new TranslationPool(
    english: "Cobweb",
    spanish: "",
    portuguese: "",
    french: "",
    russian: "",
    italian: ""
    );

    public override float Cooldown => 10;
    public override float EffectDuration => 0;

    public override ButtonLocation Location => ButtonLocation.BottomRight;

    public override int MaxUses => 1;

    public override LoadableAsset<Sprite> Sprite => Assets.PoisonButton;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is ArachnidRole;
    }

    public override bool CanUse()
    {
        return UsesLeft != 0;
    }
    public List<Vent> MinedVents;
    protected override void OnClick()
    {
        PlayerControl.LocalPlayer.RpcPlaceCobweb();
    }
}
