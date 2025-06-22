using MiraAPI.Utilities.Assets;
using Reactor.Utilities;
using TMPro;
using UnityEngine;

namespace ReachForStars;

public static class Assets
{
    public static readonly AssetBundle Bundle = AssetBundleManager.Load("rfsbundle");

    public static LoadableResourceAsset DetectiveIndicator = new("ReachForStars.Resources.DetectiveIndicator.png");

    //Misc
    public static LoadableResourceAsset GrayScaleUsesCounter { get; } =
        new("ReachForStars.Resources.AbilityCounter.png");

    public static LoadableResourceAsset RedKillButton { get; } = new("ReachForStars.Resources.KillButtonRed.png");

    public static LoadableResourceAsset BlueKillButton { get; } = new("ReachForStars.Resources.KillButtonBlue.png");

    //Chiller
    public static LoadableBundleAsset<GameObject> FrozenBodyPrefab { get; } = new("FrozenBodyPrefab.prefab", Bundle);
    public static LoadableResourceAsset FreezeButton { get; } = new("ReachForStars.Resources.Freeze.png");

    public static LoadableAudioResourceAsset FreezeSFX { get; } =
        new("ReachForStars.Resources.SoundEffects.Freeze.wav");

    public static LoadableResourceAsset ChillerIcon { get; } = new("ReachForStars.Resources.RoleIcons.chiller.png");

    //Bounty Hunter
    public static LoadableBundleAsset<GameObject> BountyPrefab { get; } = new("BountyHudPrefab.prefab", Bundle);

    public static LoadableBundleAsset<RuntimeAnimatorController> BountyHudCloseAnimController { get; } =
        new("HudCloseAnimController.controller", Bundle);

    public static LoadableBundleAsset<RuntimeAnimatorController> BountyHudOpenAnimController { get; } =
        new("HudOpenAnimController.controller", Bundle);

    //Emojis
    public static LoadableBundleAsset<TMP_SpriteAsset> EmojiIndex { get; } = new("Emojis.asset", Bundle);


    //Trapper
    public static LoadableResourceAsset PlaceTrapButton { get; } = new("ReachForStars.Resources.PlaceTrap.png");

    public static LoadableAudioResourceAsset TrapCloseSfx { get; } =
        new("ReachForStars.Resources.SoundEffects.TrapClose.wav");

    public static LoadableAudioResourceAsset TrapPlaceSfx { get; } =
        new("ReachForStars.Resources.SoundEffects.PlaceTrap.wav");

    public static LoadableBundleAsset<GameObject> StunnedPrefab { get; } = new("StunnedPrefab.prefab", Bundle);
    public static LoadableBundleAsset<GameObject> TrapPrefab { get; } = new("TrapPrefab.prefab", Bundle);

    public static LoadableBundleAsset<RuntimeAnimatorController> TrapCloseAnimationController { get; } =
        new("TrapPrefab.controller", Bundle);

    public static LoadableBundleAsset<RuntimeAnimatorController> TrapArrowAnimationController { get; } =
        new("ArrowAnimationController.controller", Bundle);

    //PlaceHolder
    public static LoadableResourceAsset PlaceHolder { get; } = new("ReachForStars.Resources.PlaceHolder.png");

    //Detective
    public static LoadableResourceAsset Inspect { get; } = new("ReachForStars.Resources.Inspect.png");

    public static LoadableResourceAsset MagnifyingGlass { get; } = new("ReachForStars.Resources.MagnifyingGlass.png");

    //Witch
    public static LoadableResourceAsset PoisonButton { get; } = new("ReachForStars.Resources.Poison.png");
    public static LoadableResourceAsset RoleblockButton { get; } = new("ReachForStars.Resources.RoleBlock.png");

    public static LoadableResourceAsset AdminButton { get; } = new("ReachForStars.Resources.AdminButton.png");

    //Sheriff
    public static LoadableResourceAsset Shoot { get; } = new("ReachForStars.Resources.Shoot.png");
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

    public static LoadableResourceAsset Glue { get; } = new("ReachForStars.Resources.Glue.png");

    //Jester
    public static LoadableAudioResourceAsset JesterIntroSFX { get; } =
        new("ReachForStars.Resources.SoundEffects.JesterIntro.wav");

    //Mole
    public static LoadableBundleAsset<RuntimeAnimatorController> VentDigAnimController { get; } =
        new("VentDigAnimController.controller", Bundle);

    public static LoadableResourceAsset DigButton { get; } = new("ReachForStars.Resources.DigButton.png");
}