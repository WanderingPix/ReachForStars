using MiraAPI.Roles;

namespace ReachForStars.Features;

public static class RecolorableUsesCounter
{
    public static void SetUp(ActionButton button)
    {
        button.usesRemainingSprite.sprite = Assets.GrayScaleUsesCounter.LoadAsset();
    }

    public static void Update(RoleBehaviour role)
    {
        foreach (var button in HudManager.Instance.GetComponentsInChildren<AbilityButton>(true))
        {
            if (role is not ICustomRole) button.usesRemainingSprite.color = role.TeamColor;
            else if (role is ICustomRole custom) button.usesRemainingSprite.color = custom.RoleColor;
        }
    }
}