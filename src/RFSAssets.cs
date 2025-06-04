using MiraAPI.Utilities.Assets;

namespace ReachForStars;

public static class Assets
{
    public static LoadableResourceAsset PoisonButton { get; } = new("ReachForStars.Resources.Poison.png");

    //Sheriff
    public static LoadableResourceAsset Shoot { get; } = new("ReachForStars.Resources.Shoot.png");
    public static LoadableResourceAsset SheriffIcon0 { get; } = new("ReachForStars.Resources.RoleIcons.Sheriff.sheriff0.png");
    public static LoadableResourceAsset SheriffIcon1 { get; } = new("ReachForStars.Resources.RoleIcons.Sheriff.sheriff1.png");
    public static LoadableResourceAsset SheriffIcon2 { get; } = new("ReachForStars.Resources.RoleIcons.Sheriff.sheriff2.png");

    //Chiller 
    public static LoadableResourceAsset Freeze { get; } = new("ReachForStars.Resources.Freeze.png");
    public static LoadableAudioResourceAsset FreezeSFX { get; } = new("ReachForStars.Resources.SoundEffects.Freeze.wav");
    public static LoadableResourceAsset FrozenBody0 { get; } = new("ReachForStars.Resources.FrozenBodyStages.FrozenBody0.png");
    public static LoadableResourceAsset SpeedOMeter { get; } = new("ReachForStars.Resources.SpeedOMeter.png");


    //Arachnid
    public static LoadableResourceAsset Cobweb0 { get; } = new("ReachForStars.Resources.Cobwebs.Cobweb0.png");
    public static LoadableResourceAsset Cobweb1 { get; } = new("ReachForStars.Resources.Cobwebs.Cobweb1.png");
    public static LoadableResourceAsset Cobweb2 { get; } = new("ReachForStars.Resources.Cobwebs.Cobweb2.png");
    public static LoadableResourceAsset Cobweb3 { get; } = new("ReachForStars.Resources.Cobwebs.Cobweb3.png");
    public static LoadableAudioResourceAsset CobwebSFX { get; } = new("ReachForStars.Resources.SoundEffects.Cobweb.wav");


    //Trapper
    public static LoadableResourceAsset Trap0 { get; } = new("ReachForStars.Resources.Traps.Trap0.png");
    public static LoadableResourceAsset Trap1 { get; } = new("ReachForStars.Resources.Traps.Trap1.png");
    public static LoadableResourceAsset Trap2 { get; } = new("ReachForStars.Resources.Traps.Trap2.png");
    public static LoadableResourceAsset Trap3 { get; } = new("ReachForStars.Resources.Traps.Trap3.png");

    //Stunned
    public static LoadableResourceAsset Stunned0 { get; } = new("ReachForStars.Resources.StunnedAnimation.Stunned0.png");
    public static LoadableResourceAsset Stunned1 { get; } = new("ReachForStars.Resources.StunnedAnimation.Stunned1.png");
    public static LoadableResourceAsset Stunned2 { get; } = new("ReachForStars.Resources.StunnedAnimation.Stunned2.png");
    public static LoadableResourceAsset Stunned3 { get; } = new("ReachForStars.Resources.StunnedAnimation.Stunned3.png");
}