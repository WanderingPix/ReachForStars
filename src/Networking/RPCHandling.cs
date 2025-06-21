using System.Collections.Generic;
using System.Linq;
using MiraAPI.Utilities;
using ReachForStars.Roles.Crewmates.Trapper;
using ReachForStars.Roles.Impostors.Chiller;
using ReachForStars.Roles.Impostors.Mole;
using ReachForStars.Roles.Impostors.Stickster;
using ReachForStars.Utilities;
using Reactor.Networking.Attributes;
using Reactor.Utilities;
using Reactor.Utilities.Extensions;
using UnityEngine;
using Object = UnityEngine.Object;

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
                Vent prefab = Object.FindObjectOfType<Vent>(true);
                Vent vent = Object.Instantiate<Vent>(prefab);
                Vector3 ppos = p.GetTruePosition();
                vent.transform.position = new(ppos.x, ppos.y, 1f);

                PluginSingleton<ReachForStars>.Instance.Log.LogDebug("Managed to create vent!");

                Animator myAnim = vent.myRend.gameObject.GetComponent<Animator>();

                PluginSingleton<ReachForStars>.Instance.Log.LogDebug("Managed to create animator!");
                myAnim.runtimeAnimatorController = Assets.VentDigAnimController.LoadAsset();


                vent.gameObject.name = $"MoleVent{mole.MinedVents.Count()}";

                vent.Id = VentUtils.GetAvailableId();
                vent.Center = null;

                List<Vent> newAllVents = ShipStatus.Instance.AllVents.ToList();
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
            if (p.Data.Role is SticksterRole stickster)
            {
                var go = new GameObject("Glue");
                go.transform.position = new Vector3(p.transform.position.x, p.transform.position.y, 1f);
                var glue = go.AddComponent<Glue>();
                stickster.PlacedGlues.Add(glue);
            }
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