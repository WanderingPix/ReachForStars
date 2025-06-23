using MiraAPI.GameOptions;
using MiraAPI.GameOptions.OptionTypes;
using UnityEngine;

namespace ReachForStars;

public sealed class MapOptions : AbstractOptionGroup
{
    public enum MapDeco
    {
        Default = 0,
        Normal = 1,
        Halloween = 2,
        Christmas = 3,
        Celebration = 4
    }

    public override string GroupName => "Map Options";
    public override Color GroupColor => new(0f, 0.5f, 0f, 1f);
    public ModdedEnumOption DecoOption { get; } = new("Map Decoration", 0, typeof(MapDeco));
}