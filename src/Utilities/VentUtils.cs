using System;
using MiraAPI.Utilities;

namespace ReachForStars.Utilities;

public static class VentUtils
{
    public static int GetAvailableId()
    {
        int count = 0;
        while (Helpers.GetVentById(count) != null)
        {
            count++;
        }
        return count;
    }
}
