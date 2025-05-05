using MiraAPI.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using Reactor.Utilities;
using System.Collections;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Networking;
using TMPro;
using ReachForStars.Translation;
using Reactor.Utilities.Extensions;

namespace ReachForStars.Roles.Crewmates.Snoop;
public class AdminButton : CustomActionButton
{
    public override string Name => ButtonName.GetTranslatedText();

    public TranslationPool ButtonName = new TranslationPool(
        english: "Admin",
        spanish: "TBD",
        portuguese: "TBD",
        french: "Administration",
        russian: "Админ",
        italian: "Amministrazone"
    );
    public override float Cooldown => 0;
    public override float EffectDuration => 0;

    public override int MaxUses => 0;

    public override LoadableAsset<Sprite> Sprite => Assets.Shoot;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is SnoopRole;
    }
    MapOptions opts = new MapOptions()
    {
        ShowLivePlayerPosition = true,
        IncludeDeadBodies = true,
    };
    protected override void OnClick()
    {
        HudManager.Instance.ToggleMapVisible(opts);
    }
}
