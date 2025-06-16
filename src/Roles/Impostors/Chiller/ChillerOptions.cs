using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.GameOptions.OptionTypes;
using MiraAPI.PluginLoading;
using ReachForStars.Translation;

namespace ReachForStars.Roles.Impostors.Chiller
{
    [MiraIgnore]
    public sealed class ChillerOptions : AbstractOptionGroup<FreezerRole>
    {
        public override string GroupName => "Chiller Options";
    }
}
