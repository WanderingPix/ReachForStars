namespace ReachForStars.Features;

public class RoleNameTag
{
    /// <summary>
    ///     Executed by Patches/OnCutsceneBreak.cs
    /// </summary>
    public static void SetRoleNameTag(RoleBehaviour role)
    {
        if (role.Player == PlayerControl.LocalPlayer)
            if (role is CrewmateRole)
                role.Player.cosmetics.nameText.color = Palette.White;
    }
}