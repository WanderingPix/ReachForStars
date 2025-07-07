using MiraAPI.Utilities.Assets;
using Reactor.Utilities;
using UnityEngine;

namespace ReachForStars;

public static class Assets
{
    public static readonly AssetBundle Bundle = AssetBundleManager.Load("rfsbundle");

    public static LoadableResourceAsset DetectiveIndicator = new("ReachForStars.Resources.UI.DetectiveIndicator.png");

    //Misc
    public static LoadableResourceAsset GrayScaleUsesCounter { get; } =
        new("ReachForStars.Resources.AbilityCounters.AbilityCounter.png");

    public static LoadableResourceAsset RedKillButton { get; } =
        new("ReachForStars.Resources.Abilities.KillButtonRed.png");

    public static LoadableResourceAsset BlueKillButton { get; } =
        new("ReachForStars.Resources.Abilities.KillButtonBlue.png");

    //Chiller
    public static LoadableBundleAsset<GameObject> FrozenBodyPrefab { get; } = new("FrozenBodyPrefab.prefab", Bundle);
    public static LoadableResourceAsset FreezeButton { get; } = new("ReachForStars.Resources.Abilities.Freeze.png");

    public static LoadableAudioResourceAsset FreezeSFX { get; } =
        new("ReachForStars.Resources.SoundEffects.Freeze.wav");

    public static LoadableResourceAsset ChillerIcon { get; } = new("ReachForStars.Resources.RoleIcons.chiller.png");

    //Bounty Hunter
    public static LoadableBundleAsset<GameObject> BountyPrefab { get; } = new("BountyHudPrefab.prefab", Bundle);

    public static LoadableBundleAsset<RuntimeAnimatorController> BountyHudCloseAnimController { get; } =
        new("HudCloseAnimController.controller", Bundle);

    public static LoadableBundleAsset<RuntimeAnimatorController> BountyHudOpenAnimController { get; } =
        new("HudOpenAnimController.controller", Bundle);

    //PlaceHolder
    public static LoadableResourceAsset PlaceHolder { get; } = new("ReachForStars.Resources.PlaceHolder.png");

    //Executioner
    public static LoadableResourceAsset ExileButton { get; } = new("ReachForStars.Resources.UI.Exile.png");

    public static LoadableBundleAsset<RuntimeAnimatorController> HammerAnimController { get; } =
        new("ExileAnimationController.controller", Bundle);

    //Detective
    public static LoadableResourceAsset Inspect { get; } = new("ReachForStars.Resources.Abilities.Inspect.png");
    public static LoadableResourceAsset DetectiveIcon { get; } = new("ReachForStars.Resources.RoleIcons.detective.png");

    public static LoadableResourceAsset MagnifyingGlass { get; } =
        new("ReachForStars.Resources.UI.MagnifyingGlass.png");

    //Witch
    public static LoadableResourceAsset PoisonButton { get; } = new("ReachForStars.Resources.Abilities.Poison.png");

    public static LoadableResourceAsset RoleblockButton { get; } =
        new("ReachForStars.Resources.Abilities.RoleBlock.png"); //Unused

    public static LoadableResourceAsset AdminButton { get; } = new("ReachForStars.Resources.Abilities.AdminButton.png");

    //Sheriff
    public static LoadableResourceAsset Shoot { get; } = new("ReachForStars.Resources.Abilities.Shoot.png");
    public static LoadableResourceAsset SheriffIcon { get; } = new("ReachForStars.Resources.RoleIcons.sheriff.png");

    //Stickster
    public static LoadableResourceAsset Glue0 { get; } = new("ReachForStars.Resources.Glues.Glue0.png");
    public static LoadableResourceAsset Glue1 { get; } = new("ReachForStars.Resources.Glues.Glue1.png");
    public static LoadableResourceAsset Glue2 { get; } = new("ReachForStars.Resources.Glues.Glue2.png");

    public static LoadableResourceAsset GlueVar0 { get; } =
        new("ReachForStars.Resources.Glues.Variations.GlueVar0.png");

    public static LoadableResourceAsset GlueVar1 { get; } =
        new("ReachForStars.Resources.Glues.Variations.GlueVar1.png");

    public static LoadableResourceAsset GlueVar2 { get; } =
        new("ReachForStars.Resources.Glues.Variations.GlueVar2.png");

    public static LoadableAudioResourceAsset GlueSFX { get; } = new("ReachForStars.Resources.SoundEffects.Glue.wav");

    public static LoadableAudioResourceAsset SticksterIntroSFX { get; } =
        new("ReachForStars.Resources.SoundEffects.SticksterIntro.wav");

    public static LoadableResourceAsset Glue { get; } = new("ReachForStars.Resources.Abilities.Glue.png");
    public static LoadableResourceAsset SticksterIcon { get; } = new("ReachForStars.Resources.RoleIcons.stickster.png");

    //Jester
    public static LoadableAudioResourceAsset JesterIntroSFX { get; } =
        new("ReachForStars.Resources.SoundEffects.JesterIntro.wav");

    public static LoadableResourceAsset jesterIcon { get; } = new("ReachForStars.Resources.RoleIcons.jester.png");

    //Mole
    public static LoadableBundleAsset<RuntimeAnimatorController> VentDigAnimController { get; } =
        new("VentDigAnimController.controller", Bundle);

    public static LoadableResourceAsset DigButton { get; } = new("ReachForStars.Resources.Abilities.DigButton.png");

    public static LoadableAudioResourceAsset DigSfx { get; } = new("ReachForStars.Resources.SoundEffects.Dig.wav");

    //Electroman
    public static LoadableResourceAsset ElectrocuteButton { get; } =
        new("ReachForStars.Resources.Abilities.ElectrocuteButton.png");

    public static LoadableAudioResourceAsset ElectromanIntroSfx { get; } =
        new("ReachForStars.Resources.SoundEffects.ElectromanIntro.wav");

    public static LoadableBundleAsset<AnimationClip> ElectrocutedAnimation { get; } =
        new("ElectrocutedAnimation.anim", Bundle);

    public static LoadableAudioResourceAsset ElectricSound { get; } =
        new("ReachForStars.Resources.SoundEffects.ElectricalSound.wav");

    public static LoadableResourceAsset[] EnergyCounters { get; } =
    [
        new("ReachForStars.Resources.AbilityCounters.EnergyCounter0.png"),
        new("ReachForStars.Resources.AbilityCounters.EnergyCounter1.png"),
        new("ReachForStars.Resources.AbilityCounters.EnergyCounter2.png"),
        new("ReachForStars.Resources.AbilityCounters.EnergyCounter3.png")
    ];

    public static LoadableResourceAsset BurntBody { get; } = new("ReachForStars.Resources.Objects.BurntBody.png");
}