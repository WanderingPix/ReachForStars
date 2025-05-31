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
using ReachForStars.Roles.Impostors.Arachnid;
using ReachForStars.Roles.Crewmates.Trapper;

namespace ReachForStars;

[BepInAutoPlugin("ReachForStars", "Reach For Stars Beta", "0.1")]
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
        ClassInjector.RegisterTypeInIl2Cpp<ExtendedPlayerControl>();
        ClassInjector.RegisterTypeInIl2Cpp<Cobweb>();
        ClassInjector.RegisterTypeInIl2Cpp<Trap>();
        ReactorCredits.Register<ReachForStars>(ReactorCredits.AlwaysShow);
    }
}
