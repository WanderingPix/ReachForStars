using MiraAPI.Utilities;

namespace ReachForStars.Utilities;

public static class VentUtils
{
    public static int GetAvailableId()
    {
        var count = 0;
        while (Helpers.GetVentById(count) != null) count++;
        return count;
    }
}