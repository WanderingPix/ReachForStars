using MiraAPI.Utilities.Assets;
using Reactor.Utilities;
using TMPro;
using UnityEngine;

namespace ReachForStars;

public static class Assets
{
    public static readonly AssetBundle Bundle = AssetBundleManager.Load("rfsbundle");
    public static LoadableBundleAsset<GameObject> BodyTypeSwitcher { get; } = new("BodytypeSwitcher.prefab", Bundle);

    public static LoadableBundleAsset<GameObject> CreditsMenuPrefab = new("CreditsMenuPrefab.prefab", Bundle);

    public static LoadableResourceAsset PListActive = new("ReachForStars.Resources.UI.PlayerListActive.png");
    public static LoadableResourceAsset PListInactive = new("ReachForStars.Resources.UI.PlayerListInactive.png");

    public static LoadableResourceAsset Square { get; } =
        new("ReachForStars.Resources.UI.Square.png");
    public static LoadableResourceAsset Circle { get; } = 
        new("ReachForStars.Resources.UI.Circle.png", 75);
    public static LoadableResourceAsset PlaceHolder { get; } = new("ReachForStars.Resources.PlaceHolder.png"); //TODO Make this unused.

    public static LoadableResourceAsset AdminButton { get; } = new("ReachForStars.Resources.Abilities.AdminButton.png");
    
    //Jester
    public static LoadableAudioResourceAsset JesterIntroSfx { get; } =
        new("ReachForStars.Resources.SoundEffects.JesterIntro.wav");

    public static LoadableResourceAsset JesterIcon { get; } = new("ReachForStars.Resources.RoleIcons.jester.png");


    //Mole
    public static LoadableBundleAsset<AnimationClip> DigAnimation { get; } =
        new("DigAnimation.anim", Bundle);
    public static LoadableBundleAsset<GameObject> MoleVentPrefab { get; } =
        new("MoleVent.prefab", Bundle);
    public static LoadableResourceAsset DigButton { get; } = new("ReachForStars.Resources.Abilities.DigButton.png");
    public static LoadableAudioResourceAsset DigSfx { get; } = new("ReachForStars.Resources.SoundEffects.Dig.wav");
    
    public static LoadableBundleAsset<GameObject> LanternObject { get; } = new("lanternPrefab.prefab", Bundle);

    public static LoadableBundleAsset<AnimationClip> LanternBobAnim { get; } =
        new("LanternBobbingAnimation.anim", Bundle);

    public static LoadableAudioResourceAsset ChainsSfx { get; } = new("ReachForStars.Resources.SoundEffects.Chain.wav");

    public static LoadableAudioResourceAsset VacuumGhostSfx { get; } =
        new("ReachForStars.Resources.SoundEffects.VacuumGhost.wav");
    
    public static LoadableBundleAsset<GameObject> StalkMinigame { get; } = new("StalkerTabletPrefab.prefab", Bundle);
    public static LoadableBundleAsset<RenderTexture> StalkCamTex { get; } = new("StalkedCamTex.renderTexture", Bundle);
    public static LoadableBundleAsset<GameObject> RfsLogo { get; } = new("RFSLogo.prefab", Bundle);
    public static LoadableAudioResourceAsset MoneySfx { get; } = new("ReachForStars.Resources.SoundEffects.Money.wav");
    public static LoadableResourceAsset HandHoldingBody { get; } = new("ReachForStars.Resources.Objects.HandHoldingBody.png");
    public static LoadableResourceAsset DousedOverlay { get; } = new("ReachForStars.Resources.UI.DousedOverlay.png", 548);
    public static LoadableBundleAsset<RuntimeAnimatorController> BurnButtonAnimationController =
        new("burnButtonAnimationController.controller", Bundle);
    
    public static readonly LoadableBundleAsset<GameObject> flamePrefab =
        new("flamePrefab.prefab", Bundle);

    public static readonly LoadableBundleAsset<AnimationClip> wrangledPlayerWalkAnim =
        new LoadableBundleAsset<AnimationClip>("WrangledPlayerWalk.anim", Bundle);
    
    public static readonly LoadableBundleAsset<Material> ropeMaterial =
        new LoadableBundleAsset<Material>("RopeMaterial.mat", Bundle);
    
    public static readonly LoadableBundleAsset<Material> GrayscaleMaterial =
        new LoadableBundleAsset<Material>("Custom_DesaturationShader.mat", Bundle);

    public static LoadableResourceAsset CowboyRoleIcon { get; set; } =
        new("ReachForStars.Resources.RoleIcons.cowboy.png");
    
    public static LoadableResourceAsset PyromaniacRoleIcon { get; set; } =
        new("ReachForStars.Resources.RoleIcons.pyromaniac.png");
    //Pirate:
    public static LoadableResourceAsset PirateRoleIcon { get; set; } =
        new("ReachForStars.Resources.RoleIcons.pirate.png");
    //Ghost buster:
    public static LoadableResourceAsset GhostbusterRoleIcon { get; } =
        new("ReachForStars.Resources.RoleIcons.ghostbuster.png");
    public static LoadableResourceAsset GhostTrap { get; } =
        new("ReachForStars.Resources.Objects.GhostTrap.png");
    public static LoadableResourceAsset GogglesOverlay { get; } = 
        new("ReachForStars.Resources.UI.GogglesOverlay.png", 548);
    public static LoadableResourceAsset Wind { get; } =
        new("ReachForStars.Resources.Objects.Wind.png");
    //Taskmaster:
    public static LoadableResourceAsset TaskmasterRoleIcon { get; } =
        new("ReachForStars.Resources.RoleIcons.taskmaster.png");
    public static LoadableResourceAsset DoTaskButton { get; } =
        new("ReachForStars.Resources.Abilities.DoTaskButton.png");
    //Paranoiac:
    public static LoadableResourceAsset ParanoiacRoleIcon { get; } =
        new("ReachForStars.Resources.RoleIcons.paranoiac.png");
    public static LoadableResourceAsset ParanoiaButton { get; } =
        new("ReachForStars.Resources.Abilities.ParanoiaButton.png");
    public static LoadableResourceAsset AbilityUsedVisual { get; } = 
        new("ReachForStars.Resources.UI.AbilityUsedVisual.png", 75);
    //Carrier
    public static LoadableResourceAsset CarrierRoleIcon { get; } =
        new("ReachForStars.Resources.RoleIcons.carrier.png");
    //Lightener
    public static LoadableResourceAsset LightenerRoleIcon { get; } =
        new("ReachForStars.Resources.RoleIcons.lightener.png");
    
    //Sleepcaster
    public static LoadableBundleAsset<GameObject> SleepOverlay =
        new LoadableBundleAsset<GameObject>("SleepOverlay.prefab", Bundle);
    
    public static LoadableBundleAsset<AnimationClip> SleepingPlayerAnimation =
        new LoadableBundleAsset<AnimationClip>("SleepingPlayerAnimation.anim", Bundle);
    public static LoadableResourceAsset Cloud { get; } = new("ReachForStars.Resources.Objects.Cloud.png");
    public static LoadableResourceAsset PacifyButton { get; } = new("ReachForStars.Resources.Abilities.PacifyButton.png");
    public static LoadableResourceAsset ReviveButton { get; } = new("ReachForStars.Resources.Abilities.ReviveButton.png");
    public static LoadableResourceAsset BodyIcon { get; } = new("ReachForStars.Resources.UI.BodyIcon.png", 256);
    public static LoadableResourceAsset HandHoldingTorch { get; } = new("ReachForStars.Resources.Objects.HandHoldingTorch.png");
    
    public static LoadableAudioResourceAsset TeleportSfx { get; } =
        new("ReachForStars.Resources.SoundEffects.Teleport.wav");
}