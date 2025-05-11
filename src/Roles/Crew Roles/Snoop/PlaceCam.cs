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
using ReachForStars.Networking;

namespace ReachForStars.Roles.Crewmates.Snoop;
public class PlaceCameras : CustomActionButton
{
    public override string Name => ButtonName.GetTranslatedText();

    public TranslationPool ButtonName = new TranslationPool(
        english: "Place Camera",
        spanish: "TBD",
        portuguese: "TBD",
        french: "TBD",
        russian: "Поставить Камеру",
        italian: "TBD"
    );
    public override float Cooldown => 3;
    public override float EffectDuration => 0;

    public override int MaxUses => 5; // TODO make this a setting

    public override LoadableAsset<Sprite> Sprite => Assets.Shoot;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is SnoopRole;
    }
    protected override void OnClick()
    {
        PlayerControl.LocalPlayer.RpcPlaceCamera(1);
    }
}
