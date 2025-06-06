using MiraAPI.Utilities.Assets;

namespace ReachForStars;

public static class Assets
{
    //PlaceHolder
    public static LoadableResourceAsset PlaceHolder { get; } = new("ReachForStars.Resources.PlaceHolder.png");
    //Detective
    public static LoadableResourceAsset Inspect { get; } = new("ReachForStars.Resources.Inspect.png");
    //Witch
    public static LoadableResourceAsset PoisonButton { get; } = new("ReachForStars.Resources.Poison.png");

    //Sheriff
    public static LoadableResourceAsset Shoot { get; } = new("ReachForStars.Resources.Shoot.png");
    public static LoadableResourceAsset SheriffIcon { get; } = new("ReachForStars.Resources.RoleIcons.sheriff.png");

    //Chiller 
    public static LoadableResourceAsset Freeze { get; } = new("ReachForStars.Resources.Freeze.png");
    public static LoadableAudioResourceAsset FreezeSFX { get; } = new("ReachForStars.Resources.SoundEffects.Freeze.wav");
    public static LoadableResourceAsset FrozenBody0 { get; } = new("ReachForStars.Resources.FrozenBodyStages.FrozenBody0.png");
    public static LoadableResourceAsset ChillerIcon { get; } = new("ReachForStars.Resources.RoleIcons.chiller.png");


    //Arachnid
    public static LoadableResourceAsset Glue0 { get; } = new("ReachForStars.Resources.Glues.Glue0.png");
    public static LoadableResourceAsset  Glue1 { get; } = new("ReachForStars.Resources.Glues.Glue1.png");
    public static LoadableResourceAsset  Glue2 { get; } = new("ReachForStars.Resources.Glues.Glue2.png");
    
    public static LoadableResourceAsset GlueVar0 { get; } = new("ReachForStars.Resources.Glues.Variations.GlueVar0.png");
    public static LoadableResourceAsset GlueVar1 { get; } = new("ReachForStars.Resources.Glues.Variations.GlueVar1.png");
    public static LoadableResourceAsset GlueVar2 { get; } = new("ReachForStars.Resources.Glues.Variations.GlueVar2.png");
    public static LoadableAudioResourceAsset GlueSFX { get; } = new("ReachForStars.Resources.SoundEffects.Glue.wav");
    public static LoadableResourceAsset Glue { get; } = new("ReachForStars.Resources.Glue.png");

    //Trapper
    public static LoadableResourceAsset Trap0 { get; } = new("ReachForStars.Resources.Traps.Trap0.png");
    public static LoadableResourceAsset Trap1 { get; } = new("ReachForStars.Resources.Traps.Trap1.png");
    public static LoadableResourceAsset Trap2 { get; } = new("ReachForStars.Resources.Traps.Trap2.png");

    //Stunned
    public static LoadableResourceAsset Stunned0 { get; } = new("ReachForStars.Resources.StunnedAnimation.Stunned0.png");
    public static LoadableResourceAsset Stunned1 { get; } = new("ReachForStars.Resources.StunnedAnimation.Stunned1.png");
    public static LoadableResourceAsset Stunned2 { get; } = new("ReachForStars.Resources.StunnedAnimation.Stunned2.png");
}