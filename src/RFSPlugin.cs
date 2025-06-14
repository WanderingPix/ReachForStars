using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using MiraAPI.PluginLoading;
using Reactor;
using Reactor.Utilities;
using Reactor.Networking;
using Reactor.Networking.Attributes;
using MiraAPI;
using Il2CppInterop.Runtime.Injection;
using ReachForStars.Roles.Impostors.Stickster;
using ReachForStars.Roles.Crewmates.Trapper;
using ReachForStars.Roles.Neutrals.BountyHunter;

namespace ReachForStars;

[BepInAutoPlugin("ReachForStars", "Reach For Stars", "v1.0.0")]
[BepInProcess("Among Us.exe")]
[BepInDependency(ReactorPlugin.Id)]
[BepInDependency(MiraApiPlugin.Id)]
[ReactorModFlags(ModFlags.RequireOnAllClients)]
public partial class ReachForStars : BasePlugin, IMiraPlugin
{
    public Harmony Harmony { get; } = new(Id);
    public string OptionsTitleText => "Reach For The Stars"; 
    public bool IsDev = true;
    public ConfigFile GetConfigFile() => Config;
    public override void Load()
    {
        Harmony.PatchAll();
        RegisterMonoBehaviours();
        ReactorCredits.Register<ReachForStars>(ReactorCredits.AlwaysShow);
    }
    public static void RegisterMonoBehaviours()
    {
        ClassInjector.RegisterTypeInIl2Cpp<Glue>();
        ClassInjector.RegisterTypeInIl2Cpp<Trap>();
        ClassInjector.RegisterTypeInIl2Cpp<BountyHud>();
    }
}
