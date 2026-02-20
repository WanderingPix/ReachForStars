using System;
using System.Collections.Generic;
using System.Linq;
using Cpp2IL.Core.Extensions;
using MiraAPI.Utilities;
using PowerTools;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace ReachForStars.Utilities;

public static class PlayerControlUtils
{
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
    
    public static PlayerControl CreateFakePlayer(PlayerControl source)
    {
        var clone = UnityObject.Instantiate(source);
        clone.transform.position = source.transform.position;
        clone.enabled = false;
        clone.MyPhysics.enabled = false;
        clone.cosmetics.enabled = false;
        return clone;
    }
}