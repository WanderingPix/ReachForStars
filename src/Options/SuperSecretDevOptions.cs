using System;
using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.GameOptions.OptionTypes;
using MiraAPI.Utilities;
using Reactor.Utilities;
using UnityEngine;

namespace ReachForStars;

public class DevModeOptions : AbstractOptionGroup
{
    public override string GroupName => "Super Secret Dev Options";
    public override Color GroupColor => new Color(0f, 0.5f, 0f, 1f);
    public ModdedToggleOption ShowAllRoles { get; } = new ModdedToggleOption("Show everyone's roles", false);



    // this is a func<bool>
    public override Func<bool> GroupVisible => () => ShouldShow();
    public bool ShouldShow()
    {
        return PlayerControl.LocalPlayer.Data.FriendCode == "shinyrake#9382" && PluginSingleton<ReachForStars>.Instance.IsDev;
    }
}