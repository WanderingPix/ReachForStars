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

namespace ReachForStars.Roles.Impostors.Mole;
public class Dig : CustomActionButton
{
    public override string Name => buttonName.GetTranslatedText();
    public TranslationPool buttonName = new TranslationPool(
    english: "Dig",
    spanish: "excavar",
    french: "creuser",
    russian: "копать",// tbh, i love this role actually :) - lime
    italian: "scavare"// btw really that sounds kida unprofessional (I'm using slang) XoXo pengun
    );

    public override float Cooldown => 0;
    public override float EffectDuration => 5;

    public override ButtonLocation Location => ButtonLocation.BottomRight;

    public override int MaxUses => 1;

    public override LoadableAsset<Sprite> Sprite => Assets.PoisonButton;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is MoleRole;
    }

    public override bool CanUse()
    {
        return UsesLeft != 0;
    }
    protected override void OnClick()
    {
        PlayerControl.LocalPlayer.RpcPlaceVent();
        PlayerControl.LocalPlayer.MyPhysics.enabled = false;
    }
    public override void OnEffectEnd()
    {
        PlayerControl.LocalPlayer.MyPhysics.enabled = true;
    } 
}
