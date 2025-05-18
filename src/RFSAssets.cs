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


    //Snoop
    public static LoadableResourceAsset SnoopCamOn { get; } = new("ReachForStars.Resources.SnoopCamera.SnoopCamOn.png");
    public static LoadableResourceAsset SnoopCamOff { get; } = new("ReachForStars.Resources.SnoopCamera.SnoopCamOff.png");
}