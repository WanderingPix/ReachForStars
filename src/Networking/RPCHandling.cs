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
            Coroutines.Start(DoDigAnim(p));
        }
        public static System.Collections.IEnumerator DoDigAnim(PlayerControl p)
        {
            yield return new WaitForSeconds(2f);
            Dig dig = CustomButtonSingleton<Dig>.Instance;
            Vent prefab = Object.FindObjectOfType<Vent>(true);
            Vent vent = Object.Instantiate<Vent>(prefab);
            vent.transform.localScale = new Vector3(1f, 1f, 1f);
            dig.MinedVents.Add(vent);
            vent.name = $"MoleVent{dig.MinedVents.Count()}";
            vent.transform.position = new Vector3(p.GetTruePosition().x, p.GetTruePosition().y, prefab.transform.position.z);

            vent.Id = VentUtils.GetAvailableId();
            vent.Left = null;
            vent.Right = dig.MinedVents[^1];

            dig.MinedVents[^1].Left = vent;

            vent.StartCoroutine(Effects.Bounce(vent.transform, 1f));
            vent.StartCoroutine(Effects.ColorFade(vent.myRend, Palette.Black, Palette.White, 1.4f));
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
