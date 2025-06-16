using MiraAPI.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using Reactor.Utilities;
using System.Collections;
using UnityEngine;
using MiraAPI.Utilities;
using ReachForStars.Utilities;
using ReachForStars.Translation;
using Reactor.Utilities.Extensions;
using System.Collections.Generic;

namespace ReachForStars.Roles.Crewmates.Detective;
public class Inspect : CustomActionButton<DeadBody>
{
    public override string Name => ButtonName.GetTranslatedText();

    public TranslationPool ButtonName = new TranslationPool(
        english: "Inspect",
        spanish: "Inspeccionar",
        french: "Inspecter",
        russian: "",
        italian: ""
    );
    public override float Cooldown => 25;
    public override float EffectDuration => 3;

    public override int MaxUses => 1;

    public override LoadableAsset<Sprite> Sprite => Assets.Inspect;

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
    ContactFilter2D filter;
    public override void OnEffectEnd()
    {
        if (PlayerControl.LocalPlayer.Data.Role is DetectiveRole det)
        {
            det.RegenerateSuspectList();
        }       
    }
    public IEnumerator DoInspectAnimation()
    {
        GameObject Anim = new GameObject("InspectAnimation");
        Anim.transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, 0f);
        Anim.transform.localScale = new Vector3(0.5f, 0.5f, 1f);

        Anim.AddComponent<SpriteRenderer>().sprite = Assets.MagnifyingGlass.LoadAsset();
        HudManager.Instance.StartCoroutine(Effects.SwayX(Anim.transform, 3f, 0.3f));
        yield return new WaitForSeconds(3f);
        HudManager.Instance.SpawnTextOverlay("Info Gathered!");
        Anim.DestroyImmediate();
        yield break;
    }
    public override ButtonLocation Location => ButtonLocation.BottomRight;
}
