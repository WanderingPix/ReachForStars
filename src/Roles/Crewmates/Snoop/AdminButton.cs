using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Snoop;

public class AdminButton : CustomActionButton
{
    public TranslationPool ButtonName = new(
        "Admin",
        "Administración",
        "Administration",
        "Админ"
        //italian: "Amministrazone"
    );

    public override string Name => ButtonName.GetTranslatedText();
    public override float Cooldown => 0;
    public override float EffectDuration => 0;

    public override int MaxUses => 0;

    public override LoadableAsset<Sprite> Sprite => Assets.AdminButton;
    public override ButtonLocation Location => ButtonLocation.BottomRight;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is SnoopRole;
    }

    protected override void OnClick()
    {
        HudManager.Instance.InitMap();
        MapBehaviour.Instance.ShowCountOverlay(true, false, true);
    }
}