using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using BepInEx;
using BepInEx.Configuration;
using Microsoft.VisualBasic;
using MiraAPI;
using MiraAPI.LocalSettings;
using MiraAPI.LocalSettings.Attributes;
using MiraAPI.Utilities;
using MiraAPI.Utilities.Assets;
using ReachForStars.Features;
using Reactor.Utilities;
using Reactor.Utilities.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace ReachForStars.Options;

public class ClientSettings(ConfigFile config) : LocalSettingsTab(config)
{
    public override string TabName => "RFS";
        
    public override LocalSettingTabAppearance TabAppearance => new()
    {
        TabIcon = MiraAssets.SettingsIcon,
        TabColor = RFSPalette.RFSColor2,
        TabButtonColor = RFSPalette.RFSColor,
        TabButtonActiveColor = RFSPalette.RFSColor2,
        TabButtonHoverColor = RFSPalette.RFSColor2,
    };

    [LocalToggleSetting]
    public ConfigEntry<bool> EnableColorfulBubbles { get; private set; } =
        config.Bind("Chat", "Make chat bubbles player-coloured", true);
    
    [LocalToggleSetting]
    public ConfigEntry<bool> ShowLeaveConfirmationPopup { get; private set; } = config.Bind("Menus", "Show confirmation pop-up when exiting game", false);
    
    [LocalToggleSetting]
    public ConfigEntry<bool> EnableInGamePlayerList { get; private set; } = config.Bind("Menus", "Enable In-Game Player list while in lobby", true);

    [LocalToggleSetting]
    public ConfigEntry<bool> EnableBetterMinimap { get; private set; } = config.Bind("Menus", "Enable Better Minimap", true);

    public override void OnOptionChanged(ConfigEntryBase configEntry)
    {
        if (configEntry == EnableInGamePlayerList)
        {
            if (Features.InGamePlayerList.PlayerListButton)
            {
                Features.InGamePlayerList.PlayerListButton.gameObject.SetActive(EnableInGamePlayerList.Value);
            }
        }
    }
}