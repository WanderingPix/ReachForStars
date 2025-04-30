using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using ReachForStars.Networking;
using UnityEngine;
using ReachForStars.Translation;
using MiraAPI.Roles;

namespace ReachForStars.Roles.Impostors.Mole;
public class Dig : CustomActionButton
{
    public override string Name => buttonName.GetTranslatedText();
    public TranslationPool buttonName = new TranslationPool(
    english: "Dig",
    spanish: "excavar",
    portuguese: "escavação",
    french: "creuser",
    russian: "выкопать",
    italian: "scavare"
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
        return PlayerControl.LocalPlayer.CanPet() && UsesLeft != 0;
    }
    protected override void OnClick()
    {
        PlayerControl.LocalPlayer.MyPhysics.enabled = false;
        
    }
    public int GlobalCount = 0;
    public int LocalCount = 0;

    public override void OnEffectEnd()
    {
        LocalCount++;
        PlayerControl.LocalPlayer.MyPhysics.enabled = true;
        PlayerControl.LocalPlayer.RpcPlaceVent(LocalCount);
    }
    
}
