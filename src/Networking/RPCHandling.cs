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
            if (p.Data.Role is MoleRole mole) Coroutines.Start(DoDigAnim(p, mole));
        }
        public static System.Collections.IEnumerator DoDigAnim(PlayerControl p, MoleRole mole)
        {
            yield return new WaitForSeconds(2f);
            Vent prefab = Object.FindObjectOfType<Vent>(true);
            Vent vent = Object.Instantiate<Vent>(prefab);
            vent.transform.position = p.GetTruePosition();

            mole.MinedVents.Add(vent);

            vent.gameObject.name = $"MoleVent{mole.MinedVents.Count()}";


            vent.Id = VentUtils.GetAvailableId();
            vent.Left = null;
            vent.Right = null;

            if (mole.MinedVents[^1] != null)
            {
                vent.Right = mole.MinedVents[^1];
                mole.MinedVents[^1].Left = vent;
            }

            vent.StartCoroutine(Effects.Bounce(vent.transform, 1f));
            vent.StartCoroutine(Effects.ColorFade(vent.myRend, Palette.Black, Palette.White, 1.4f));
            List<Vent> newAllVents = ShipStatus.Instance.AllVents.ToList();
            newAllVents.Add(vent);
            ShipStatus.Instance.AllVents = newAllVents.ToArray();
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
            GameObject FrozenBody = new GameObject($"FrozenBody{targetBody.ParentId}");
            FrozenBody.transform.position = targetBody.gameObject.transform.position;
            FrozenBody.transform.localScale = targetBody.gameObject.transform.localScale;
            FrozenBody.AddComponent<FrozenBody>().SetTargetBody(targetBody);
        }
        [MethodRpc((uint)RPC.DamageFrozenBody)]
        public static void RpcDamageFrozenBody(byte id)
        {
            var body = Object.FindObjectsOfType<FrozenBody>().ToList().FirstOrDefault(x => x.id == id);
            body.Level--;
            body.Durability = 10;
            body.UpdateVisuals();
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
            GameObject t = new GameObject("trap");
            t.transform.position = p.GetTruePosition();
            Trap trapcomp = t.AddComponent<Trap>();
            trapcomp.Trapper = p;
        }
    }
}
