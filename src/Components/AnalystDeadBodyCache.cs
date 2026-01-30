using System;
using AmongUs.GameOptions;
using MiraAPI.Utilities;
using ReachForStars.Roles.Crewmates.Analyst;
using Reactor.Utilities.Attributes;
using UnityEngine;

namespace ReachForStars.Components;

[RegisterInIl2Cpp]
public class AnalystDeadBodyCache(IntPtr ptr) : MonoBehaviour(ptr)
{
    public AnalystDeadBodyData data;
    public void Initialize(PlayerControl player, PlayerControl killer)
    {
        data = new(player.Data.PlayerName, killer.Data.Role.Role);
    }

    private void Update()
    {
        if (data == null) return;
        
        data.Lifetime += Time.deltaTime;
    }
}