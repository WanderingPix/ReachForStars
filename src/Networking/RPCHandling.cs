using Reactor.Networking.Attributes;
using Reactor.Networking.Rpc;
using UnityEngine;
using Reactor.Utilities;
using MiraAPI.Utilities;
using ReachForStars.Utilities;
using System.Linq;
using Reactor.Utilities.Extensions;
using ReachForStars.Roles.Impostors.Chiller;
using Object = UnityEngine.Object;
using ReachForStars.Roles.Impostors.Mole;
using MiraAPI.Hud;
using MiraAPI.Modifiers;
using ReachForStars.Roles.Impostors.Stickster;
using ReachForStars.Roles.Crewmates.Trapper;
using System.Collections.Generic;
using IEnumerator = System.Collections.IEnumerator;
using BepInEx;

namespace ReachForStars.Networking
{
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
        public static void RpcChangeBodyType(this PlayerControl target, PlayerBodyTypes type, bool shouldAnimateForSeeker = false)
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
            if (p.Data.Role is MoleRole mole) Coroutines.Start(DoDigAnim(mole, p));
        }
        public static IEnumerator DoDigAnim(MoleRole mole, PlayerControl p)
        {
            Vent prefab = Object.FindObjectOfType<Vent>(true);
            Vent vent = Object.Instantiate<Vent>(prefab);
            Vector3 ppos = p.GetTruePosition();
            vent.transform.position = new(ppos.x, ppos.y, 1f);

            PluginSingleton<ReachForStars>.Instance.Log.LogInfo("Managed to create vent!");
            
            Animator myAnim = vent.myRend.gameObject.GetComponent<Animator>();

            PluginSingleton<ReachForStars>.Instance.Log.LogInfo("Managed to create animator!");
            vent.enabled = false;
            yield return myAnim.runtimeAnimatorController = Assets.VentDigAnimController.LoadAsset();
            vent.enabled = true;
            mole.MinedVents.Add(vent);

            vent.gameObject.name = $"MoleVent{mole.MinedVents.Count()}";


            vent.Id = VentUtils.GetAvailableId();
            vent.Left = null;
            vent.Right = null;
            vent.Center = null;

            if (mole.MinedVents.Last() != vent)
            {
                vent.Right = mole.MinedVents[^1];
                mole.MinedVents.Last().Left = vent;
            }

            List<Vent> newAllVents = ShipStatus.Instance.AllVents.ToList();
            newAllVents.Add(vent);
            ShipStatus.Instance.AllVents = newAllVents.ToArray();
            
            yield break;
        }
        [MethodRpc((uint)RPC.ResizePlayer)]
        public static void RpcResize(this PlayerControl player, float x, float y, float z)
        {
            player.Resize(new Vector3(x, y, z));
        }
        [MethodRpc((uint)RPC.FreezeBody)]
        public static void RpcFreezeBody(this PlayerControl player)
        {
            DeadBody targetBody = player.GetNearestDeadBody(2f);
            GameObject FrozenBody = Object.Instantiate(Assets.FrozenBodyPrefab.LoadAsset());
            FrozenBody.transform.position = targetBody.gameObject.transform.position;
            FrozenBody.transform.localScale = targetBody.gameObject.transform.localScale;
            FrozenBody.AddComponent<FrozenBody>().SetTargetBody(targetBody);
        }
        [MethodRpc((uint)RPC.DamageFrozenBody)]
        public static void RpcDamageFrozenBody(this PlayerControl p, byte id)
        {
            FrozenBody body = Object.FindObjectsOfType<FrozenBody>().ToList().FirstOrDefault(x => x.id == id);
            body.Damage();
        }
        [MethodRpc((uint)RPC.PlaceGlue)]
        public static void RpcPlaceGlue(this PlayerControl p)
        {
            GameObject glue = new GameObject("Glue");
            glue.transform.position = new Vector3(p.transform.position.x, p.transform.position.y, 1f);
            glue.AddComponent<Glue>();
        }
        [MethodRpc((uint)RPC.PlaceTrap)]
        public static void RpcPlaceTrap(this PlayerControl p)
        {
            GameObject t = Object.Instantiate(Assets.TrapPrefab.LoadAsset());
            Vector3 ppos = p.GetTruePosition();
            t.transform.position = new(ppos.x, ppos.y, 1f);
            Trap trapcomp = t.AddComponent<Trap>();
            trapcomp.Trapper = p;
        }
    }
}
