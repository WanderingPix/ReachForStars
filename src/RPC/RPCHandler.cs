using Reactor.Networking.Attributes;
using Reactor.Networking.Rpc;
using UnityEngine;
using Reactor.Utilities;
using MiraAPI.Utilities;
using ReachForStars.Utilities;
using System.Linq;
using Reactor.Utilities.Extensions;
using ReachForStars.Roles.Impostors.Mole;
using AmongUs.GameOptions;
using MiraAPI.Hud;
using ReachForStars.Roles.Impostors.Chiller;
using System.Collections.Generic;

namespace ReachForStars.Networking
{
    public static class RPCS
    {
        [MethodRpc((uint) RPC.DestroyObj)]
        public static void RpcDestroyImmediate(this GameObject go, bool shouldFade = false, int Fadetime = 0)
        {
            go.DestroyImmediate();
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
        public static void RpcPlaceVent(this PlayerControl p)
        {
            int count = Object.FindObjectsByType<Vent>(FindObjectsSortMode.None).ToList().Count();
            Vent vent = Object.Instantiate<Vent>(Object.FindObjectOfType<Vent>(true)); 
            vent.transform.parent = ShipStatus.Instance.transform;
            vent.name = $"MoleVent{count.ToString()}";
            vent.Id = ShipStatus.Instance.AllVents.Count + count;        
            vent.transform.position = p.GetTruePosition();
            vent.Id = ShipStatus.Instance.AllVents.Count + count;
            vent.Right = null;
            if (count > 1)
            {
                vent.Right = Helpers.GetVentById(count - 1);
            }

            //TODO: smoke cloud
                    
            vent.StartCoroutine(Effects.Bounce(vent.transform, 1f));
            vent.StartCoroutine(Effects.ColorFade(vent.myRend, Palette.Black, Palette.White, 1.4f)); 
        }
        [MethodRpc((uint) RPC.ResizePlayer)]
        public static void RpcResize(this PlayerControl player, float x, float y, float z)
        {
            player.Resize(new Vector3(x, y, z));
        }
        [MethodRpc((uint) RPC.Revive)]
        public static void RpcRevive(this PlayerControl player, bool ShouldAnimate)
        {
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
        [MethodRpc((uint) RPC.DamageFrozenBody)]
        public static void RpcDamageBody(int id, int NewDurability)
        {
            var body = Object.FindObjectsOfType<FrozenBody>().ToList().FirstOrDefault(Where(x => x.myBody.parentId = id));
            body.Durability = NewDurability + 1; //Plus one because the New durability will always be a multiplication of 5, and calling Use() will decrease it by 1 and do sprite checks

            body.Use();
        }
    }
}
