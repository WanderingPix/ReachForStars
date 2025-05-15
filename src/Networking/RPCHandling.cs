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
using ReachForStars.Roles.Crewmates.Snoop;
using Rewired;

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
        public static void RpcChangeBodyType(this PlayerControl target, PlayerBodyTypes type)
        {
            target.MyPhysics.SetBodyType(type);
        }

        [MethodRpc((uint)RPC.PlaceDaVent)]
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
        [MethodRpc((uint)RPC.ResizePlayer)]
        public static void RpcResize(this PlayerControl player, float x, float y, float z)
        {
            player.Resize(new Vector3(x, y, z));
        }
        /*[MethodRpc((uint)RPC.Revive)]
        public static void RpcRevivey(this PlayerControl player, bool ShouldAnimate, byte bodyId)
        {
            DeadBody body = Object.FindObjectsOfType<DeadBody>().FirstOrDefault(x => x.ParentId == bodyId);
            if (!ShouldAnimate)
            {
                player.Revive();
                player.transform.position = body.TruePosition;
            }
            else if (ShouldAnimate) Coroutines.Start(DoReviveAnim(player, body));
        }
        public static System.Collections.IEnumerator DoReviveAnim(PlayerControl p, Vector3 body)
        {
            p.MyPhysics.enabled = false;
            p.StartCoroutine(Effects.Slide2D(p.transform, p.transform.position, body.TruePosition, 0.8f));

            yield return new WaitForSeconds(1f);

            p.MyPhysics.enabled = true;
            p.Revive();
            p.Shapeshift(PlayerControl.AllPlayerControls.ToArray().ToList().FirstOrDefault(x => x.Data.PlayerId == body.ParentId), false);

            body.DestroyImmediate();

            yield break;
        }*/
        [MethodRpc((uint)RPC.FreezeBody)]
        public static void RpcFreezeBody(this PlayerControl player)
        {
            DeadBody targetBody = player.GetNearestDeadBody(2f);
            GameObject FrozenBody = new GameObject($"FrozenBody{targetBody.ParentId}");
            FrozenBody.transform.position = targetBody.gameObject.transform.position;
            FrozenBody.transform.localScale = targetBody.gameObject.transform.localScale;
            FrozenBody.AddComponent<FrozenBody>().SetTargetBody(targetBody);
        }
        [MethodRpc((uint)RPC.DamageFrozenBody)]
        public static void RpcDamageFrozenBody(int id, int NewDurability)
        {
            var body = Object.FindObjectsOfType<FrozenBody>().ToList().Where(x => x.myBody.ParentId == id).ToList()[0];
            body.Durability = NewDurability;
            switch (body.Durability)
            {
                case 20:
                    HudManager.Instance.StartCoroutine(Effects.ScaleIn(body.gameObject.transform, body.gameObject.transform.localScale.x, body.gameObject.transform.localScale.x * 0.6f, 0.4f));
                    break;
                case 10:
                    HudManager.Instance.StartCoroutine(Effects.ScaleIn(body.gameObject.transform, body.gameObject.transform.localScale.x, body.gameObject.transform.localScale.x * 0.6f, 0.4f));
                    break;
                case 0:
                    body.DestroyImmediate();
                    break;
            }
        }
        [MethodRpc((uint)RPC.Possess)]
        public static void RpcPossess(this PlayerControl p, byte id)
        {
            Coroutines.Start(DoPossessAnim(p, Object.FindObjectsOfType<DeadBody>().FirstOrDefault(x => x.ParentId == id)));
        }
        public static System.Collections.IEnumerator DoPossessAnim(PlayerControl p, DeadBody body)
        {
            p.MyPhysics.enabled = false;
            p.StartCoroutine(Effects.Slide2D(p.transform, p.transform.position, body.TruePosition, 0.8f));
            Animator anim = body.transform.GetChild(1).GetComponent<Animator>();
            anim.speed *= -1f;
            anim.Play(0, 0, 1);

            yield return new WaitForSeconds(1f);

            PlayerControl target = PlayerControl.AllPlayerControls.ToArray().ToList().FirstOrDefault(x => x.Data.PlayerId == body.ParentId);

            p.MyPhysics.enabled = true;
            p.Revive();
            p.Shapeshift(target, false);

            body.gameObject.DestroyImmediate();
            yield break;
        }
    }
}
