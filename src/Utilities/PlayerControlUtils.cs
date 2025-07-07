using System.Linq;
using PowerTools;
using UnityEngine;

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
}