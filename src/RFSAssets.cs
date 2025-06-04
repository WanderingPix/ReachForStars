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
    public static LoadableResourceAsset  Glue0 { get; } = new("ReachForStars.Resources.Glues.Glue0.png");
    public static LoadableResourceAsset  Glue1 { get; } = new("ReachForStars.Resources.Glues.Glue1.png");
    public static LoadableResourceAsset  Glue2 { get; } = new("ReachForStars.Resources.Glues.Glue2.png");
    public static LoadableResourceAsset  Glue3 { get; } = new("ReachForStars.Resources.Glues.Glue3.png");
    public static LoadableAudioResourceAsset  GlueSFX { get; } = new("ReachForStars.Resources.SoundEffects.Glue.wav");
    public static LoadableResourceAsset Glue { get; } = new("ReachForStars.Resources.Glue.png");
    public static LoadableResourceAsset Droplet0 { get; } = new("ReachForStars.Resources.Glues.Droplets.Droplet0.png");
    public static LoadableResourceAsset Droplet1 { get; } = new("ReachForStars.Resources.Glues.Droplets.Droplet1.png");
    public static LoadableResourceAsset Droplet2 { get; } = new("ReachForStars.Resources.Glues.Droplets.Droplet2.png");
    public static LoadableResourceAsset Droplet3 { get; } = new("ReachForStars.Resources.Glues.Droplets.Droplet3.png");
    public static LoadableResourceAsset Droplet4 { get; } = new("ReachForStars.Resources.Glues.Droplets.Droplet4.png");



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