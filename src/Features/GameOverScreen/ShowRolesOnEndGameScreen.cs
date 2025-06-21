using System.Collections.Generic;
using System.Linq;
using Reactor.Utilities;

namespace ReachForStars.Features.GameOverScreen;

public static class ShowRolesOnEndGameScreen
{
    public static Dictionary<int, RoleBehaviour> CachedRoles = new();

    public static void CacheRoles()
    {
        CachedRoles.Clear();
        foreach (var p in PlayerControl.AllPlayerControls.ToArray().ToList())
        {
            CachedRoles.Add(p.cosmetics.ColorId, p.Data.Role);
            PluginSingleton<ReachForStars>.Instance.Log.LogDebug("Cached Players' roles!");
        }
    }

    public static void SetUp(EndGameManager end)
    {
        PluginSingleton<ReachForStars>.Instance.Log.LogDebug("Reading Player Roles...");
        foreach (var poolablePlayer in end.gameObject.GetComponentsInChildren<PoolablePlayer>())
        {
            poolablePlayer.ToggleName(true);
            CachedRoles.TryGetValue(poolablePlayer.cosmetics.ColorId, out var behaviour);
            poolablePlayer.SetNameColor(behaviour.NameColor);
            poolablePlayer.SetName(behaviour.NiceName);
            PluginSingleton<ReachForStars>.Instance.Log.LogDebug("Player Roles Read successfully!");
        }
    }
}