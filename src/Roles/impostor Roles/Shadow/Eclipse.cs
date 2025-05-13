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
using Rewired;

namespace ReachForStars.Roles.Impostors.Shadow;
public class Ecliple : CustomActionButton
{
    public override string Name => buttonName.GetTranslatedText();
    public TranslationPool buttonName = new TranslationPool(
    english: "Eclipse",
    spanish: "",
    portuguese: "",
    french: "Eclipse",
    russian: "",
    italian: ""
    );

    public override float Cooldown => 0;
    public override float EffectDuration => 15;

    public override ButtonLocation Location => ButtonLocation.BottomRight;

    public override int MaxUses => 0;

    public override LoadableAsset<Sprite> Sprite => Assets.PoisonButton;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is ShadowRole;
    }
    public override bool CanUse()
    {
        return !DepressionSab.isActive;
    }

    protected override void OnClick()
    {
        DepressionSab.Init(PlayerControl.LocalPlayer);
    }

    public override void OnEffectEnd()
    {
    }
    
}
