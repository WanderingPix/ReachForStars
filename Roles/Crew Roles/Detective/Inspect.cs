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

namespace ReachForStars.Roles.Crewmates.Detective;
public class Inspect : CustomActionButton<DeadBody>
{
    public override string Name => ButtonName.GetTranslatedText();

    public TranslationPool ButtonName = new TranslationPool(
        english: "Inspect",
        spanish: "Inspeccionar",
        portuguese: "Inspecionar",
        french: "Inspecter",
        italian: "Ispeziona"
    );
    public override float Cooldown => 25;
    public override float EffectDuration => 3;

    public override int MaxUses => 1;

    public override LoadableAsset<Sprite> Sprite => Assets.Shoot;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is DetectiveRole;
    }

    public override DeadBody? GetTarget()
    {
        return PlayerControl.LocalPlayer.GetNearestDeadBody(Distance);
    }

    public override void SetOutline(bool active)
    {
    }
    
    public override bool IsTargetValid(DeadBody? target)
    {
        return true;
    }
    protected override void OnClick()
    {
        
    }
    ContactFilter2D filter;
    public override void OnEffectEnd()
    {
        foreach (PlayerControl pc in Helpers.GetClosestPlayersInCircle(PlayerControl.LocalPlayer.GetTruePosition(), 6f))
        {
            if (PlayerControl.LocalPlayer.Data.Role is DetectiveRole det && !det.ActualEvils.Contains(pc))
            {
                det.Suspects.Remove(pc);
            }
        }
    }
}
