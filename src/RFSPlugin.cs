using System.IO;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using MiraAPI;
using MiraAPI.PluginLoading;
using ReachForStars.Components;
using ReachForStars.Roles.Crewmates.Lightener;
using Reactor;
using Reactor.Networking;
using Reactor.Networking.Attributes;
using Reactor.Utilities;

namespace ReachForStars;

[BepInAutoPlugin("ReachForStars", "Reach For Stars", "2.0")]
[BepInProcess("Among Us.exe")]
[BepInDependency(ReactorPlugin.Id)]
[BepInDependency(MiraApiPlugin.Id)]
[ReactorModFlags(ModFlags.RequireOnAllClients)]
public partial class ReachForStars : BasePlugin, IMiraPlugin
{
    public Harmony Harmony { get; } = new(Id);
    public string OptionsTitleText => "Reach For Stars";

    public ConfigFile GetConfigFile()
    {
        return Config;
    }

    public override void Load()
    {
        Harmony.PatchAll();
        ReactorCredits.Register<ReachForStars>(ReactorCredits.AlwaysShow);
        ClassInjector.RegisterTypeInIl2Cpp<Lantern>();
        ClassInjector.RegisterTypeInIl2Cpp<SwingingLantern>();
        if (!Directory.Exists($"{Paths.GameRootPath}/Screenshots"))
        {
            Directory.CreateDirectory($"{Paths.GameRootPath}/Screenshots");
        }
        Log.LogInfo("Reach For Stars Loaded Successfully! >u<");
    }
}