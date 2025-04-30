using MiraAPI.GameOptions;
using MiraAPI.GameOptions.OptionTypes;
using MiraAPI.Utilities;
using UnityEngine;

namespace ReachForStars.GameModes.Options;

public class MainOptions : AbstractOptionGroup
{
    public override string GroupName => "Main Options";
    public override uint GroupPriority => 0;

    public ModdedEnumOption<SelectableGamemodes> GMOpt { get; } = new("Current Gamemode", SelectableGamemodes.Classic)
    {
        ChangedEvent = x => MiraAPI.GameModes.CustomGameModeManager.SetGameMode(1),
    };
    public SelectableGamemodes GamemodeOpt { get; set; } = SelectableGamemodes.Classic;
}
public enum SelectableGamemodes
{
    Classic,
    BattleRoyale,
    HideAndSeek,
    HotPotato,
}