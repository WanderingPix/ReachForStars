using System.Linq;
using MiraAPI.Events;
using MiraAPI.Events.Vanilla.Gameplay;
using MiraAPI.Roles;

namespace ReachForStars.Features;

public static class PlayerRoleGlyphs
{
    [RegisterEvent]
    public static void OnSetRole(SetRoleEvent e)
    {
        e.Player.cosmetics.nameText.spriteAsset = Assets.RoleIcons.LoadAsset();
        var r = RoleManager.Instance.AllRoles.First(x => x.Role == e.Role);

        if (CanLocalPlayerSeeRole(r))
            e.Player.cosmetics.nameText.text = $"{GetTag(r)} {e.Player.cosmetics.nameText.text}";
    }

    public static string GetTag(RoleBehaviour r)
    {
        return $"<sprite name={r.GetScriptClassName()}>";
    }

    public static bool CanLocalPlayerSeeRole(RoleBehaviour r)
    {
        if (r is ICustomRole c) return c.CanLocalPlayerSeeRole(PlayerControl.LocalPlayer);
        if (r.IsImpostor) return PlayerControl.LocalPlayer.Data.Role.IsImpostor;
        if (r.IsImpostor == false) return r.Player == PlayerControl.LocalPlayer;

        return true;
    }
}