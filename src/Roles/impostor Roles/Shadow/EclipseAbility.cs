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

namespace ReachForStars.Roles.Impostors.Shadow;
public class Eclipse : CustomActionButton
{
    public override string Name => buttonName.GetTranslatedText();
    public TranslationPool buttonName = new TranslationPool(
    english: "Eclipse",
    spanish: "",
    french: "Eclipser",
    russian: "",
    italian: ""
    );

    public override float Cooldown => 45;
    public override float EffectDuration => 10;

    public override ButtonLocation Location => ButtonLocation.BottomRight;

    public override LoadableAsset<Sprite> Sprite => Assets.PoisonButton;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is ShadowRole;
    }
    protected override void OnClick()
    {
        PlayerControl.LocalPlayer.RpcEclipse(EffectDuration);
    }
    public override void OnEffectEnd()
    {
    } 
}
