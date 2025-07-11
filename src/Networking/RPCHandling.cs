using System.Collections;
using System.Linq;
using MiraAPI.Utilities;
using ReachForStars.Roles.Impostors.Chiller;
using ReachForStars.Roles.Impostors.Electroman;
using ReachForStars.Roles.Impostors.Mole;
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

    [MethodRpc((uint)RPC.PlaceDaVent)]
    public static void RpcPlaceVent(this PlayerControl p)
    {
        if (p.Data.Role is MoleRole mole)
        {
            p.NetTransform.Halt();
            p.MyPhysics.Animations.PlayIdleAnimation();
            var prefab = Object.FindObjectOfType<Vent>(true);
            var vent = Object.Instantiate(prefab);
            Vector3 ppos = p.GetTruePosition();
            vent.transform.position = new Vector3(ppos.x, ppos.y, 1f);

            PluginSingleton<ReachForStars>.Instance.Log.LogDebug("Managed to create vent!");

            Animator myAnim;
            if (vent.gameObject.GetComponent<Animator>()) myAnim = vent.myRend.gameObject.GetComponent<Animator>();
            else myAnim = vent.myRend.gameObject.AddComponent<Animator>();

            SoundManager.Instance.PlaySoundAtLocation(Assets.DigSfx.LoadAsset(), ppos,
                PlayerControl.LocalPlayer.GetTruePosition(), SoundManager.Instance.SfxChannel);

            PluginSingleton<ReachForStars>.Instance.Log.LogDebug("Managed to create animator!");
            myAnim.runtimeAnimatorController = Assets.VentDigAnimController.LoadAsset();

            vent.gameObject.name = $"MoleVent{mole.MinedVents.Count()}";

            vent.Id = VentUtils.GetAvailableId();
            vent.Center = null;

            var newAllVents = ShipStatus.Instance.AllVents.ToList();
            newAllVents.Add(vent);
            ShipStatus.Instance.AllVents = newAllVents.ToArray();
            if (mole.MinedVents.Count > 0)
            {
                vent.Left = mole.MinedVents.Last();
                mole.MinedVents.Last().Right = vent;
                mole.MinedVents.First().Right = vent;
            }
            else
            {
                vent.Left = null;
                vent.Right = null;
            }

            vent.Center = null;
            vent.gameObject.GetComponent<VentCleaningConsole>()?.DestroyImmediate();

            mole.MinedVents.Add(vent);
            PluginSingleton<ReachForStars>.Instance.Log.LogDebug(
                $"new vent placed! total placed vent count for {p.Data.PlayerName} is now {mole.MinedVents.Count}");
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

    [MethodRpc((uint)RPC.Execute)]
    public static void RpcExecutePlayer(this PlayerControl Source, PlayerControl Target)
    {
        var area = Object.FindObjectsOfType<PlayerVoteArea>()
            .First(x => x.TargetPlayerId == Target.PlayerId);
        Coroutines.Start(CoExecute(area, Target));
    }

    public static IEnumerator CoExecute(PlayerVoteArea area, PlayerControl Target)
    {
        area.StartCoroutine(Effects.Shake(area.transform, 1.3f, 0.1f, true, true));
        var Go = new GameObject("ExileHammer");
        Go.layer = LayerMask.NameToLayer("UI");
        Go.gameObject.transform.SetParent(area.transform);
        Go.transform.localPosition = Vector3.zero;
        Go.AddComponent<SpriteRenderer>();
        Go.AddComponent<Animator>().runtimeAnimatorController = Assets.HammerAnimController.LoadAsset();
        HudManager.Instance.StartCoroutine(Effects.ScaleIn(Go.transform, 2f, 1f, 1.3f));
        MeetingHud.Instance.exiledPlayer = Target.Data;
        MeetingHud.Instance.VotingComplete(null, Target.Data, false);
        yield break;
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
}