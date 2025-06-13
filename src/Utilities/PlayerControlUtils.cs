using UnityEngine;
using Reactor.Utilities;
using System.Collections;
using System.Linq;

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
}
