using System.Linq;
using UnityEngine;

namespace ReachForStars.Utilities;

public static class ShipStatusUtils
{
    public static bool IsValidPointOnMap(this ShipStatus ship, Vector2 pos)
    {
        return ship.AllRooms.Count(x => x.roomArea.OverlapPoint(pos)) > 0;
    }
}