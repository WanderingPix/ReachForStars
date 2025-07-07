using Il2CppSystem;
using MiraAPI.Hud;
using MiraAPI.Modifiers;
using MiraAPI.Utilities;
using MiraAPI.Utilities.Assets;
using ReachForStars.Features;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Electroman;

public class Electrocute : CustomActionButton<PlayerControl>
{
    public TranslationPool btnName = new(
        "Electrocute",
        french: "",
        spanish: "",
        russian: ""
    );

    public override string Name => btnName.GetTranslatedText();

    public override float Cooldown => 25;
    public override float EffectDuration => 1;

    public override ButtonLocation Location => ButtonLocation.BottomRight;

    public override int MaxUses => 0;

    public override LoadableAsset<Sprite> Sprite => Assets.ElectrocuteButton;

    public int Charges { get; set; }

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is ElectromanRole;
    }

    public override bool CanUse()
    {
        return base.CanUse() && Charges == 3;
    }

    public override PlayerControl? GetTarget()
    {
        return PlayerControl.LocalPlayer.GetClosestPlayer(true, Distance);
    }

    public override void SetOutline(bool active)
    {
        Target?.cosmetics.SetOutline(active, new Nullable<Color>(Palette.ImpostorRed));
    }

    public override bool IsTargetValid(PlayerControl? target)
    {
        return true;
    }

    public void IncreaseCharge()
    {
        if (Charges < 3) Charges++;
        Button.usesRemainingSprite.sprite = Assets.EnergyCounters[Charges].LoadAsset();
        HudManager.Instance.StartCoroutine(Effects.ScaleIn(Button.usesRemainingSprite.transform, 1.5f, 1f, .7f));
        HudManager.Instance.StartCoroutine(Effects.ColorFade(Button.usesRemainingSprite, Color.white, Color.yellow,
            1f));
        if (Charges == 3)
            HudManager.Instance.StartCoroutine(Effects.ScaleIn(Button.transform, 1.2f, 0.7f * SmolUI.ScaleFactor, .7f));
    }

    public override void CreateButton(Transform parent)
    {
        base.CreateButton(parent);
        Charges = 0;
        Button.usesRemainingText.enabled = false;
        Button.usesRemainingSprite.sprite = Assets.EnergyCounters[Charges].LoadAsset();
        Button.usesRemainingSprite.color = Color.yellow;
        Button.usesRemainingSprite.gameObject.SetActive(true);
    }

    protected override void OnClick()
    {
    }

    public override void OnEffectEnd()
    {
        Charges = 0;
        Button.usesRemainingSprite.sprite = Assets.EnergyCounters[Charges].LoadAsset();
        Target.RpcAddModifier<ElectrocutedModifier>();
    }
}