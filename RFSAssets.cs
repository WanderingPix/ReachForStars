using MiraAPI.Utilities.Assets;

namespace ReachForStars;

public static class Assets
{
    public static LoadableResourceAsset ExampleButton { get; } = new("ReachForStars.Resources.ExampleButton.png");
    public static LoadableResourceAsset PoisonButton { get; } = new("ReachForStars.Resources.Poison.png");
    public static LoadableResourceAsset ShadowKillButton { get; } = new("ReachForStars.Resources.KillButtonVariants.ShadowKill.png");

    //Sheriff
    public static LoadableResourceAsset Shoot { get; } = new("ReachForStars.Resources.Shoot.png");
    public static LoadableResourceAsset SheriffIcon0 { get; } = new("ReachForStars.Resources.RoleIcons.Sheriff.sheriff0.png");
    public static LoadableResourceAsset SheriffIcon1 { get; } = new("ReachForStars.Resources.RoleIcons.Sheriff.sheriff1.png");
    public static LoadableResourceAsset SheriffIcon2 { get; } = new("ReachForStars.Resources.RoleIcons.Sheriff.sheriff2.png");


    public static LoadableResourceAsset Logo { get; } = new("ReachForStars.Resources.TSR.png");

    //Chiller Frozen Bodies
    public static LoadableResourceAsset FrozenBody0 { get; } = new("ReachForStars.Resources.FrozenBodyStages.FrozenBody0.png");
    public static LoadableResourceAsset FrozenBody1 { get; } = new("ReachForStars.Resources.FrozenBodyStages.FrozenBody1.png");
    public static LoadableResourceAsset FrozenBody2 { get; } = new("ReachForStars.Resources.FrozenBodyStages.FrozenBody2.png");
    public static LoadableResourceAsset Puddle { get; } = new("ReachForStars.Resources.FrozenBodyStages.Puddle.png");

    public static LoadableResourceAsset SpeedOMeter { get; } = new("ReachForStars.Resources.SpeedOMeter.png");
}