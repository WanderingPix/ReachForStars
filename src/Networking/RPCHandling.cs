using System.Collections;
using System.Linq;
using MiraAPI.GameOptions;
using MiraAPI.Modifiers;
using MiraAPI.Utilities;
using ReachForStars.Components;
using ReachForStars.Components.Tasks;
using ReachForStars.Roles.Crewmates.Actor;
using ReachForStars.Roles.Crewmates.Lightener;
using ReachForStars.Roles.Crewmates.Paranoiac;
using ReachForStars.Roles.Impostors.Cowboy;
using ReachForStars.Roles.Impostors.Sleepcaster;
using ReachForStars.Roles.Neutrals.GhostBuster;
using ReachForStars.Utilities;
using Reactor.Networking.Attributes;
using Reactor.Utilities;
using Reactor.Utilities.Extensions;
using UnityEngine;
using Helpers = MiraAPI.Utilities.Helpers;
using Object = UnityEngine.Object;

namespace ReachForStars.Networking;

public static class RPCHandler
{
    [MethodRpc((uint)RPC.Yeehaw)]
    public static void RpcLasso(this PlayerControl source, PlayerControl Target)
    {
        var line = new GameObject("Lasso").AddComponent<LineRenderer>();
        line.textureMode = LineTextureMode.Tile;
        line.material.mainTexture = Assets.DigButton.LoadAsset().texture;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.loop = false;
        line.useWorldSpace = true;
        line.SetWidth(0.25f, 0.25f);
        Color lineColor = new(0.8f, 0.6f, 0, 1);
        Color lineColor2 = new(0.5f, 0.3f, 0, 1);
        line.SetColors(lineColor, lineColor2);
        Coroutines.Start(CoLasso(source, Target, line));
    }

    private static IEnumerator CoLasso(PlayerControl source, PlayerControl Target, LineRenderer line)
    {
        source.moveable = false;
        Target.moveable = false;
        Target.NetTransform.Halt();
        float grabDuration = 1.75f;
        line.SetPosition(0, source.transform.position);
        line.material = Assets.ropeMaterial.LoadAsset();
        line.textureMode = LineTextureMode.Tile;
        for (float t = 0f; t < grabDuration; t += Time.deltaTime)
        {
            line.SetPosition(1, Vector3.Lerp(source.transform.position, Target.transform.position, t / grabDuration));
            yield return null;
        }
        line.SetPosition(1, Target.transform.position);
        Target.AddModifier<WrangledModifier>();
        yield return new WaitForSeconds(0.7f);
        
        float pullDuration = 0.75f;
        for (float t = 0f; t < pullDuration; t += Time.deltaTime)
        {
            Vector3 newPosition = Vector3.Lerp(Target.transform.position, source.transform.position, t / pullDuration);
            line.SetPosition(1, newPosition);
            Target.transform.position = newPosition;
            yield return null;
        }
        Target.transform.position = source.transform.position;
        Target.moveable = true;
        source.moveable = true;
        Target.NetTransform.Halt();
        line.gameObject.Destroy();
        yield break;
    }

    [MethodRpc((uint)RPC.SeekerScream)]
    public static void RpcScream()
    {
        PlayerControl.LocalPlayer.transform.GetComponent<HnSImpostorScreamSfx>().LocalImpostorScream();
    }

    [MethodRpc((uint)RPC.ChangeBodyType)]
    public static void RpcSetBodyType(this PlayerControl target, PlayerBodyTypes type)
    {
        target.MyPhysics.SetBodyType(type);
        if (type == PlayerBodyTypes.Seeker)
        {
            target.cosmetics.SetBodyCosmeticsVisible(false);
        }
        if (type == PlayerBodyTypes.Long || type == PlayerBodyTypes.LongSeeker)
        {
            target.cosmetics.ShowLongModeParts(true);
        }
        else target.cosmetics.SetBodyCosmeticsVisible(true);
    }

    [MethodRpc((uint)RPC.LightUp)]
    public static void RpcPlaceLantern(this PlayerControl Source)
    {
        if (Source.Data.Role is LightenerRole light)
        {
            var L = Object.Instantiate(Assets.LanternObject.LoadAsset());
            L.transform.position = Source.GetTruePosition();
            Lantern lantern = L.AddComponent<Lantern>();
            lantern.LightRadius = 2f;
        }
    }
    
    [MethodRpc((uint)RPC.Act)]
    public static void RpcAct(this PlayerControl Source, PlayerControl Target, int gain)
    {
        if (Target.HasModifier<ActModifier>()) Target.GetModifier<ActModifier>().OnAct(gain);
        else Target.AddModifier<ActModifier>(Source).OnAct(gain);
    }

    [MethodRpc((uint)RPC.Vacuum)]
    public static void RpcVacuum(this PlayerControl source)
    {
        foreach (var p in Helpers.GetClosestPlayers(source, 3000f).Where(x => x.Data.IsDead && !x.HasModifier<AbsorbedModifier>()))
        {
            Coroutines.Start(CoVacuum(p, source));
        }
    }
    public static  IEnumerator CoVacuum(PlayerControl toBeAbsorbed, PlayerControl source)
    {
        source.StartCoroutine(Effects.Slide2D(toBeAbsorbed.transform, toBeAbsorbed.transform.position, source.transform.position, Mathf.Clamp(Vector3.Distance(source.transform.position, toBeAbsorbed.transform.position)/2, 0.3f, 1f)));
        Vector3 prevSize = toBeAbsorbed.transform.localScale;
        source.StartCoroutine(Effects.ScaleIn(toBeAbsorbed.transform, prevSize.x, 0, 1.5f));
        
        if (source.AmOwner)
        {
            for (float t = 0; t < 1.5f; t += Time.deltaTime)
            {
                toBeAbsorbed.Visible = true;
                yield return null;
            }
            toBeAbsorbed.Visible = false;
            toBeAbsorbed.AddModifier<AbsorbedModifier>(source);
        }

        toBeAbsorbed.transform.localScale = prevSize;
        source.Data.Role.TryCast<GhostBusterRole>().AddAbsorbedPlayer(toBeAbsorbed);
    }

    [MethodRpc((uint)RPC.TriggerGhostTrap)]
    public static void RpcTriggerGhostTrap(this PlayerControl target, int id)
    {
        GhostTrap trap = GhostTrap.allGhostTraps.First(x => x.id == id);
        trap.isAnimating = true;
        Coroutines.Start(trap.CoAnimateTrapped(target));
    }
    
    [MethodRpc((uint)RPC.PlaceGhostTrap)]
    public static void RpcPlaceGhostTrap(this PlayerControl source)
    {
        GhostTrap trap = new GameObject("Trap").AddComponent<GhostTrap>();
        trap.transform.position = source.GetTruePosition();
        trap.isAnimating = false;
        trap.source = source;
        trap.gameObject.AddComponent<SpriteRenderer>().sprite = Assets.GhostTrap.LoadAsset();
        trap.gameObject.layer = LayerMask.NameToLayer("Objects");
        Coroutines.Start(RFSEffects.Boop(trap.transform, 1.75f, 0.5f, 0.1f));
    }
    
    [MethodRpc((uint)RPC.UseAbility)]
    public static void RpcUseAbility(this PlayerControl source)
    {
        PlayerControl.LocalPlayer.GetModifierComponent().TryGetModifier(out ParanoidModifier modifier);
        modifier?.ShowIndicator(source);
    }
    
    [MethodRpc((uint)RPC.Silence)]
    public static void RpcSilence(this PlayerControl source)
    {
        var lp = PlayerControl.LocalPlayer;
        PlayerTask.GetOrCreateTask<SilenceTask>(lp, int.MaxValue);
    }
    
    [MethodRpc((uint)RPC.Revive)]
    public static void RpcRevive(this PlayerControl source, PlayerControl target)
    {
        Coroutines.Start(CoRevive(source, target));
    }

    private static IEnumerator CoRevive(PlayerControl source, PlayerControl target)
    {
        target.gameObject.SetActive(false);
        if (target.AmOwner)
        {
            HudManager.Instance.StartCoroutine(Effects.Slide2DWorld(Camera.main.transform, Camera.main.transform.position,
                source.transform.position, 1));
        }
        yield return new WaitForSeconds(1.5f);
        target.NetTransform.SnapTo(source.GetTruePosition());
        target.gameObject.SetActive(true);
        target.Revive();
        if (target.AmOwner || source.AmOwner) HudManager.Instance.FadeScreen(Color.green, Color.green.ToClearColor(), 0.4f);
        yield break;
    }
    
    [MethodRpc((uint)RPC.Sleep)]
    public static void RpcPacifyPlayers(this PlayerControl source)
    {
        //Smoke cloud effect
        var cloudsParent = new GameObject("PacifyCloud");
        cloudsParent.transform.position = source.transform.position;
        for (int i = 0; i < 3; i++)
        {
            var rend = new GameObject("CloudRend").AddComponent<SpriteRenderer>();
            rend.transform.localScale = Vector3.one * OptionGroupSingleton<SleepcasterOptions>.Instance.AbilityRange.Value;
            rend.transform.parent = cloudsParent.transform;
            rend.sprite = Assets.Cloud.LoadAsset();
            source.StartCoroutine(Effects.Slide2D(rend.transform, Vector3.zero,
                new(UnityRandom.RandomRange(-2, 2), UnityRandom.RandomRange(-2, 2)), 1.2f));
            Coroutines.Start(RFSEffects.ColorFadeAndDestroy(rend, Color.blue.LightenColor(0.6f), Color.blue.ToClearColor(), 1.3f));
        }

        source.StartCoroutine(Effects.ActionAfterDelay(3, new System.Action(() =>
        {
            cloudsParent.Destroy();
        })));
        
        //Modifier assignment logic
        foreach (var p in Helpers.GetClosestPlayers(source, 3f * OptionGroupSingleton<SleepcasterOptions>.Instance.AbilityRange.Value))
        {
            p.AddModifier<SleepyModifier>();
        }
    }

    [MethodRpc((uint)RPC.ThrowShuriken)]
    public static void RpcThrowShuriken(this PlayerControl source, Vector2 vel)
    {
        ShurikenProjectileBehaviour.Create(source,vel);
        ShurikenProjectileBehaviour.Create(source,vel + new Vector2(1, 1));
        ShurikenProjectileBehaviour.Create(source,vel + new Vector2(-1, -1));
    }
}