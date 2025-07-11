using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using MiraAPI;
using MiraAPI.PluginLoading;
using ReachForStars.Roles.Impostors.Electroman;
using ReachForStars.Roles.Impostors.Stickster;
using ReachForStars.Roles.Neutrals.BountyHunter;
using Reactor;
using Reactor.Networking;
using Reactor.Networking.Attributes;
using Reactor.Utilities;

namespace ReachForStars;

[BepInAutoPlugin("ReachForStars", "Reach For Stars", "v1.1.0")]
[BepInProcess("Among Us.exe")]
[BepInDependency(ReactorPlugin.Id)]
[BepInDependency(MiraApiPlugin.Id)]
[ReactorModFlags(ModFlags.RequireOnAllClients)]
public partial class ReachForStars : BasePlugin, IMiraPlugin
{
    public bool IsDev = true;
    public Harmony Harmony { get; } = new(Id);
    public string OptionsTitleText => "Reach For The Stars";

    public ConfigFile GetConfigFile()
    {
        return Config;
    }

    public override void Load()
    {
        Harmony.PatchAll();
        ReactorCredits.Register<ReachForStars>(ReactorCredits.AlwaysShow);
        ClassInjector.RegisterTypeInIl2Cpp<Glue>();
        ClassInjector.RegisterTypeInIl2Cpp<BountyHud>();
        ClassInjector.RegisterTypeInIl2Cpp<ShortCircuitedConsole>();

        Log.LogInfo("Reach For Stars Loaded Successfully! >u<");
    }
}