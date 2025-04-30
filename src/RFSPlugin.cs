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
using Reactor.Utilities;
using ReachForStars.Roles.Impostors.Chiller;
using AmongUs.GameOptions;
using Reactor.Localization.Utilities;

namespace ReachForStars;

[BepInAutoPlugin("ReachForStars", "Reach For Stars", "0.0.2")]
[BepInProcess("Among Us.exe")]
[BepInDependency(ReactorPlugin.Id)]
[BepInDependency(MiraApiPlugin.Id)]
[ReactorModFlags(ModFlags.RequireOnAllClients)]
public partial class ReachForStars : BasePlugin, IMiraPlugin
{
    public Harmony Harmony { get; } = new(Id);
    public string OptionsTitleText => "Reach For The Stars";
    public ConfigFile GetConfigFile() => Config;
    public override void Load()
    {
        Harmony.PatchAll();
        ClassInjector.RegisterTypeInIl2Cpp<FrozenBody>();
        ReactorCredits.Register<ReachForStars>(ReactorCredits.AlwaysShow);
    }
}
