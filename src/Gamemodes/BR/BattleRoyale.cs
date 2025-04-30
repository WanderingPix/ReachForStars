using AmongUs.GameOptions;
using Reactor.Utilities;
using Reactor.Utilities.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InnerNet;
using MiraAPI.GameModes;
using MiraAPI.GameOptions;
using TMPro;
using UnityEngine;
using Il2CppSystem.Web.Util;
using MiraAPI.Utilities;
using Helpers = MiraAPI.Utilities.Helpers;

namespace ReachForStars.GameModes.BattleRoyale;
public class BattleRoyale : CustomGameMode
{
    public override string Name => "Battle Royale";
    public override string Description => "Be the last one standing!";
    public override int Id => 1;

    public override void Initialize()
    {
        if (PlayerControl.LocalPlayer && PlayerControl.LocalPlayer.myTasks is not null)
        {
            PlayerControl.LocalPlayer.myTasks.Clear();
        }

        var random = ShipStatus.Instance.DummyLocations.Random();

        foreach (var player in GameData.Instance.AllPlayers) player.Object.cosmetics.TogglePet(false);

        PlayerControl.LocalPlayer.NetTransform.RpcSnapTo(random.position);

        foreach (var player in PlayerControl.AllPlayerControls)
        {
            player.MyPhysics.SetBodyType(PlayerBodyTypes.Seeker);
        }
    }

    public override void OnDeath(PlayerControl player)
    {
        Helpers.CreateAndShowNotification($"{player.GetClosestPlayer(true, 20f, false)}", Palette.ImpostorRed, PlayerControl.LocalPlayer.KillSfx, HudManager.Instance.KillButton.graphic.sprite);
    }
    public override void HudStart(HudManager instance)
    {
        instance.KillButton.transform.SetParent(instance.transform);
        AspectPosition pos = instance.KillButton.gameObject.AddComponent<AspectPosition>();
        pos.Alignment = AspectPosition.EdgeAlignments.Center;
        pos.DistanceFromEdge = new Vector3(0f, -2f, 0f);

        instance.transform.FindChild("Buttons").gameObject.SetActive(false);
    }

    public override void HudUpdate(HudManager instance)
    {
        
    }

    public override List<NetworkedPlayerInfo> CalculateWinners()
    {
        var alivePlayers = GameData.Instance.AllPlayers.ToArray().Where(player => !player.Disconnected && !player.IsDead).ToList();
        return alivePlayers;
    }

    public override void CanKill(out bool runOriginal, out bool result, PlayerControl target)
    {
        runOriginal = false;
        result = true;
    }

    public override bool CanReport(DeadBody body) => false;
    public override bool CanVent(Vent vent, NetworkedPlayerInfo playerInfo) => false;
    public override bool ShouldShowSabotageMap(MapBehaviour map) => false;
    public override bool CanUseMapConsole(MapConsole console) => false;
    public override bool CanUseSystemConsole(SystemConsole console) => false;
    public override void CheckGameEnd(out bool runOriginal, LogicGameFlowNormal instance)
    {
        runOriginal = false;
        if (Helpers.GetAlivePlayers().Count() > 1)
        {
            return;
        }
        instance.Manager.RpcEndGame(GameOverReason.ImpostorByKill, false);
    }

    public override void AssignRoles(out bool runOriginal, LogicRoleSelectionNormal instance)
    {
        runOriginal = false;

        foreach (PlayerControl p in Helpers.GetAlivePlayers())
        {
            p.RpcSetRole(RoleTypes.Impostor, true);
        }
    }
}