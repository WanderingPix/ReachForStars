using System;
using System.Collections.Generic;
using System.Linq;
using MiraAPI.Utilities;
using PowerTools;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace ReachForStars.Utilities;

public static class PlayerControlUtils
{
    public static void Resize(this PlayerControl player, Vector3 size)
    {
        player.transform.localScale = size;
    }

    public static PlayerControl GetPlayerById(byte id)
    {
        return PlayerControl.AllPlayerControls.ToArray().ToList().FirstOrDefault(x => x.PlayerId == id);
    }

    public static void Toggle(this CosmeticsLayer c, bool showHat, bool showName, bool showVisor, bool showSkin,
        bool showPet)
    {
        c.ToggleHat(showHat);
        c.ToggleName(showName);
        c.ToggleVisor(showVisor);
        c.ToggleSkin(showSkin);
        c.TogglePet(showPet);
    }

    public static void ToggleSkin(this CosmeticsLayer c, bool show)
    {
        c.skin.gameObject.SetActive(show);
    }

    public static SpriteAnim GetAnimator(this PlayerControl p)
    {
        return p.MyPhysics.Animations.Animator;
    }

    public static void RegenerateTasks(this PlayerControl player)
    {
        NormalPlayerTask[] LongTasks = [];
        ShipStatus.Instance.LongTasks.ToList().CopyTo(LongTasks);

        List<byte> SelectedTasks = new();
        for (var i = 0; i != GameOptionsManager.Instance.CurrentGameOptions.TotalTaskCount; i++)
        {
            var SelectedTask = LongTasks.Random();
            SelectedTasks.Add((byte)SelectedTask.Index);
            var newLongtasks = LongTasks.ToList();
            newLongtasks.Remove(SelectedTask);
            LongTasks = newLongtasks.ToArray();
        }

        player.Data.SetTasks(SelectedTasks.ToArray());
    }

    public static PlayerControl? GetClosestGhost(
        this PlayerControl playerControl,
        float distance,
        bool ignoreColliders = false,
        Predicate<PlayerControl>? predicate = null)
    {
        var filteredPlayers = Helpers.GetClosestPlayers(playerControl, distance, ignoreColliders)
            .Where(playerInfo => !playerInfo.Data.Disconnected &&
                                 playerInfo.PlayerId != playerControl.PlayerId &&
                                 playerInfo.Data.IsDead)
            .ToList();

        return predicate != null ? filteredPlayers.Find(predicate) : filteredPlayers.FirstOrDefault();
    }
}