using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using ReachForStars.Networking;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Mole;

public class Dig : CustomActionButton
{
    public TranslationPool buttonName = new(
        "Dig",
        "excavar",
        "creuser",
        "копать" // tbh, i love this role actually :) - lime
        //italian: "scavare"// btw really that sounds kida unprofessional (I'm using slang) XoXo pengun
    );

    public override string Name => buttonName.GetTranslatedText();

    public override float Cooldown => 0;
    public override float EffectDuration => 1;

    public override ButtonLocation Location => ButtonLocation.BottomRight;

    public override int MaxUses => 1;

    public override LoadableAsset<Sprite> Sprite => Assets.DigButton;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is MoleRole;
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