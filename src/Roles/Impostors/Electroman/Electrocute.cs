using MiraAPI.Hud;
using MiraAPI.Utilities;
using MiraAPI.Utilities.Assets;
using ReachForStars.Features;
using ReachForStars.Networking;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Electroman;

public class Electrocute : CustomActionButton<Console>
{
    public TranslationPool btnName = new(
        "Electrocute"
    );

    public override string Name => btnName.GetTranslatedText();

    public override float Cooldown => 25;
    public override float EffectDuration => 0;

    public override ButtonLocation Location => ButtonLocation.BottomRight;

    public override int MaxUses => 3;

    public override LoadableAsset<Sprite> Sprite => Assets.ElectrocuteButton;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is ElectromanRole;
    }

    public override Console GetTarget()
    {
        return PlayerControl.LocalPlayer.GetNearestObjectOfType<Console>(0.8f, new ContactFilter2D().NoFilter());
    }

    public override void SetOutline(bool active)
    {
        if (Target) Target.SetOutline(active, active);
    }

    public override void CreateButton(Transform parent)
    {
        base.CreateButton(parent);
        Button.usesRemainingText.enabled = false;
        Button.usesRemainingSprite.sprite = Assets.EnergyCounters[UsesLeft].LoadAsset();
        Button.usesRemainingSprite.color = Color.yellow;
        Button.usesRemainingSprite.gameObject.SetActive(true);
    }

    public override bool IsTargetValid(Console target)
    {
        return base.IsTargetValid(target) && target.GetComponent<Vent>() == null;
    }

    protected override void OnClick()
    {
        PlayerControl.LocalPlayer.RpcShortCircuit(Target.gameObject.name);
        Button.usesRemainingSprite.sprite = Assets.EnergyCounters[UsesLeft].LoadAsset();
        HudManager.Instance.StartCoroutine(Effects.ScaleIn(Button.usesRemainingSprite.transform, 1.5f, 1f, .7f));
        HudManager.Instance.StartCoroutine(Effects.ColorFade(Button.usesRemainingSprite, Color.white, Color.yellow,
            1f));
        if (UsesLeft == 0)
            HudManager.Instance.StartCoroutine(Effects.ScaleIn(Button.transform, 1.2f, 0.7f * SmolUI.ScaleFactor, .7f));
    }
}