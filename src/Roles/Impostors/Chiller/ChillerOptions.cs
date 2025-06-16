using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.GameOptions.OptionTypes;
using ReachForStars.Translation;

namespace ReachForStars.Roles.Impostors.Chiller
{
    public sealed class ChillerOptions : AbstractOptionGroup<FreezerRole>
    {
        public override string GroupName => "Chiller Options";
        public ModdedToggleOption CanVent { get; set; } = new("Chiller Can Vent", true);
    }
}
