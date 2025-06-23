using System.Collections;
using MiraAPI.Hud;
using MiraAPI.Utilities;
using MiraAPI.Utilities.Assets;
using ReachForStars.Translation;
using ReachForStars.Utilities;
using Reactor.Utilities;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Detective;

public class Inspect : CustomActionButton<DeadBody>
{
    public TranslationPool ButtonName = new(
        "Inspect",
        "Inspeccionar",
        "Inspecter",
        "Исследовать"
        //italian: "Ispettore"
    );

    private ContactFilter2D filter;
    public override string Name => ButtonName.GetTranslatedText();
    public override float Cooldown => 25;
    public override float EffectDuration => 3;

    public override int MaxUses => 1;

    public override LoadableAsset<Sprite> Sprite => Assets.Inspect;
    public override ButtonLocation Location => ButtonLocation.BottomRight;

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
        Coroutines.Start(DoInspectAnimation());
    }

    public override void OnEffectEnd()
    {
        if (PlayerControl.LocalPlayer.Data.Role is DetectiveRole det) det.RegenerateSuspectList();
    }

    public IEnumerator DoInspectAnimation()
    {
        yield return new WaitForSeconds(3f);
        HudManager.Instance.SpawnTextOverlay("Info Gathered!");
        yield break;
    }
}