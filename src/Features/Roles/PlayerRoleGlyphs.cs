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
        if (r is ICustomRole c && c.CanLocalPlayerSeeRole(e.Player))
            e.Player.cosmetics.nameText.text = $"{GetTag(r)} {e.Player.cosmetics.nameText.text}";
        else if (r is not ICustomRole && r.IsImpostor && PlayerControl.LocalPlayer.Data.Role.IsImpostor)
            e.Player.cosmetics.nameText.text = $"{GetTag(r)}{e.Player.cosmetics.nameText.text}";
    }

    public static string GetTag(RoleBehaviour r)
    {
        return $"<sprite name={r.GetScriptClassName()}>";
    }
}