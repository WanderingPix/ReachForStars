using Reactor.Networking.Attributes;
using Reactor.Networking.Rpc;
using UnityEngine;
using Reactor.Utilities;
using MiraAPI.Utilities;
using ReachForStars.Utilities;
using System.Linq;
using Reactor.Utilities.Extensions;
using ReachForStars.Roles.Impostors.Miner;
using AmongUs.GameOptions;
using MiraAPI.Hud;
using ReachForStars.Roles.Impostors.Chiller;
using Epic.OnlineServices;

namespace ReachForStars.Networking
{
    public static class RPCS
    {
        [MethodRpc((uint) RPC.DeathReasons)]
        public static void DeathReasonsRPC(this PlayerControl source, string Sentence, int Time)
        {
            if (PlayerControl.LocalPlayer == source)
            {
                HudManager.Instance.SpawnHnSPopUp(source, Sentence);
            }
        }
        [MethodRpc((uint) RPC.DestroyObj)]
        public static void RpcDestroyImmediate(this GameObject go, bool shouldFade = false, int Fadetime = 0)
        {
            go.DestroyImmediate();
        }
        [MethodRpc((uint) RPC.RoleBlock)]
        public static void RoleBlockRPC(this PlayerControl target, int Time)
        {   
            target.DeathReasonsRPC("You've been RoleBlocked!", 3);

            //target.RpcAddModifier(RoleBlockedModifier.TypeId);
        }
        [MethodRpc((uint) RPC.Yeehaw)]
        public static void RpcYeehaw()
        {
            var SFX = PlayerControl.LocalPlayer.transform.GetComponent<HnSImpostorScreamSfx>();
            SFX.LocalImpostorYeehaw();
        }
        [MethodRpc((uint) RPC.SeekerScream)]
        public static void RpcScream()
        {
            var SFX = PlayerControl.LocalPlayer.transform.GetComponent<HnSImpostorScreamSfx>();
            SFX.LocalImpostorScream();
        }

        [MethodRpc((uint) RPC.ChangeBodyType)]
        public static void RpcChangeBodyType(this PlayerControl target, PlayerBodyTypes type)
        {
            target.MyPhysics.SetBodyType(type);
        }
        [MethodRpc((uint) RPC.PlaceDaVent)]
        public static void RpcPlaceVent(this PlayerControl PlayerPos, int MinerVentCount)
        {
            if (PlayerPos.Data.Role is MinerRole miner)
            {
                Object.Instantiate<MushroomMixupPlayerAnimation>(PrefabManager.CopyPrefab<MushroomMixupPlayerAnimation>(), PlayerPos.transform).CoAnimateCloud();
                var vent = Object.Instantiate<Vent>(PrefabManager.CopyPrefab<Vent>());
                
                vent.Id = ShipStatus.Instance.AllVents.Count + 1;
                vent.transform.position = PlayerPos.GetTruePosition();
                var ventpos = vent.transform.position;
                ventpos.z = 0.0009f;

                
                vent.Left = miner.MinerVents[0];

                miner.MinerVents.Add(vent);

                if (miner.MinerVents[miner.MinerVents.Count - 1] != null)
                {
                    vent.Right = miner.MinerVents[miner.MinerVents.Count - 1];
                }
                
                
                var allVents = ShipStatus.Instance.AllVents.ToList();
                allVents.Add(vent);
                ShipStatus.Instance.AllVents = allVents.ToArray();
                vent.StartCoroutine(Effects.Bounce(vent.transform, 1f));
                vent.StartCoroutine(Effects.ColorFade(vent.myRend, Palette.Black, Palette.White, 0.7f));
            }
        }
        [MethodRpc((uint) RPC.ResizePlayer)]
        public static void RpcResize(this PlayerControl player, float x, float y, float z)
        {
            player.Resize(new Vector3(x, y, z));
        }
        [MethodRpc((uint) RPC.Revive)]
        public static void RpcRevive(this PlayerControl player, bool ShouldAnimate)
        {
            //cutscene logic
            player.Revive();
        }
        [MethodRpc((uint) RPC.FreezeBody)]
        public static void RpcFreezeBody(this PlayerControl player)
        {
            DeadBody targetBody = player.GetNearestDeadBody(2f);
            GameObject FrozenBody = new GameObject($"FrozenBody{targetBody.ParentId}");
            FrozenBody.transform.position = targetBody.gameObject.transform.position;
            FrozenBody.transform.localScale = targetBody.gameObject.transform.localScale;
            FrozenBody.AddComponent<FrozenBody>().SetTargetBody(targetBody);
        }
    }
}
