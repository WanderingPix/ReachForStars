using MiraAPI.Events;
using MiraAPI.Events.Vanilla.Gameplay;
using MiraAPI.Roles;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace ReachForStars.Features.Roles;

public class RoleIcons
{
    public static bool CanLocalPlayerSeeRole(RoleBehaviour role)
    {
        if (role is ICustomRole custom) {
            return custom.CanLocalPlayerSeeRole(PlayerControl.LocalPlayer);
        }
        return (PlayerControl.LocalPlayer.Data.Role.IsImpostor && role.Player.Data.Role.IsImpostor) || PlayerControl.LocalPlayer.Data.IsDead;
    }

    [RegisterEvent]
    private static void OnSetRole(SetRoleEvent e)
    {
        TryClearRoleIcon(e.Player);
        var role = e.Player.Data.Role;
        if (CanLocalPlayerSeeRole(role))
        {
            var iconObj = new GameObject("RoleIcon");
            iconObj.transform.SetParent(e.Player.cosmetics.nameTextContainer.transform);
            iconObj.transform.localPosition = new(0.4f, 0.4f, 0);
            Sprite icon = GetRoleIcon(role);
            iconObj.transform.localScale = GetRoleIconScale(icon);
            iconObj.AddComponent<SpriteRenderer>().sprite = icon;
        }
    }

    private static Vector3 GetRoleIconScale(Sprite icon)
    {
        float spritePixelHeight = icon.rect.height;
        float ppu = icon.pixelsPerUnit;
        float currentWorldHeight = spritePixelHeight / ppu;
        float desiredWorldHeight = 0.4f;
        float scale = desiredWorldHeight / currentWorldHeight;
        return new(scale, scale, 1);
    }

    public static void TryClearRoleIcon(PlayerControl player)
    {
        var roleIcon = player.cosmetics.nameTextContainer.transform.FindChild("RoleIcon")?.gameObject;
        if (roleIcon)
        {
            roleIcon.gameObject.Destroy();
        }
    }
    public static Sprite GetRoleIcon(RoleBehaviour role)
    {
        if (role is ICustomRole custom)
        {
            return custom.Configuration.Icon?.LoadAsset();
        }

        return role.RoleIconSolid;
    }
}