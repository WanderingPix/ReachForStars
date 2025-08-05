using System.Linq;
using MiraAPI.Utilities;
using ReachForStars.Roles.Crewmates.Lightener;
using ReachForStars.Roles.Impostors.Chiller;
using ReachForStars.Roles.Impostors.Electroman;
using ReachForStars.Roles.Impostors.Stickster;
using ReachForStars.Utilities;
using Reactor.Networking.Attributes;
using Reactor.Utilities;
using Reactor.Utilities.Extensions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ReachForStars.Networking;

public static class RPCS
{
    [MethodRpc((uint)RPC.DestroyObj)]
    public static void RpcDestroyImmediate(this GameObject go, bool shouldFade = false, int Fadetime = 0)
    {
        go.DestroyImmediate();
    }

    [MethodRpc((uint)RPC.Yeehaw)]
    public static void RpcYeehaw()
    {
        var SFX = PlayerControl.LocalPlayer.transform.GetComponent<HnSImpostorScreamSfx>();
        SFX.LocalImpostorYeehaw();
    }

    [MethodRpc((uint)RPC.SeekerScream)]
    public static void RpcScream()
    {
        var SFX = PlayerControl.LocalPlayer.transform.GetComponent<HnSImpostorScreamSfx>();
        SFX.LocalImpostorScream();
    }

    [MethodRpc((uint)RPC.ChangeBodyType)]
    public static void RpcChangeBodyType(this PlayerControl target, PlayerBodyTypes type,
        bool shouldAnimateForSeeker = false)
    {
        target.MyPhysics.SetBodyType(type);
        if (type == PlayerBodyTypes.Seeker && shouldAnimateForSeeker)
        {
            target.AnimateCustom(HudManager.Instance.IntroPrefab.HnSSeekerSpawnAnim);
            target.cosmetics.SetBodyCosmeticsVisible(false);
        }
    }

    [MethodRpc((uint)RPC.ResizePlayer)]
    public static void RpcResize(this PlayerControl player, float x, float y, float z)
    {
        player.Resize(new Vector3(x, y, z));
    }

    [MethodRpc((uint)RPC.FreezeBody)]
    public static void RpcFreezeBody(this PlayerControl player)
    {
        var targetBody = player.GetNearestDeadBody(2f);
        var FrozenBody = Object.Instantiate(Assets.FrozenBodyPrefab.LoadAsset());
        FrozenBody.transform.position = targetBody.gameObject.transform.position;
        FrozenBody.transform.localScale = targetBody.gameObject.transform.localScale;
        FrozenBody.AddComponent<FrozenBody>().SetTargetBody(targetBody);
        FrozenBody.layer = LayerMask.NameToLayer("ShortObjects");
    }

    [MethodRpc((uint)RPC.DamageFrozenBody)]
    public static void RpcDamageFrozenBody(this PlayerControl p, byte id)
    {
        var body = Object.FindObjectsOfType<FrozenBody>().ToList().FirstOrDefault(x => x.id == id);
        body.Damage();
    }

    [MethodRpc((uint)RPC.PlaceGlue)]
    public static void RpcPlaceGlue(this PlayerControl p)
    {
        if (p.Data.Role is SticksterRole stickster)
        {
            var go = new GameObject("Glue");
            go.transform.position = new Vector3(p.transform.position.x, p.transform.position.y, 1f);
            var glue = go.AddComponent<Glue>();
            stickster.PlacedGlues.Add(glue);
        }
    }

    [MethodRpc((uint)RPC.ShortCircuit)]
    public static void RpcShortCircuit(this PlayerControl Source, string ConsoleGOName)
    {
        var console = GameObject.Find(ConsoleGOName)?.GetComponent<Console>();
        if (console == null)
        {
            PluginSingleton<ReachForStars>.Instance.Log.LogError("Console not found");
        }
        else
        {
            var shortcircuit = console.gameObject.AddComponent<ShortCircuitedConsole>();
            shortcircuit.electroman = Source.Data.Role.TryCast<ElectromanRole>();
        }
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
}