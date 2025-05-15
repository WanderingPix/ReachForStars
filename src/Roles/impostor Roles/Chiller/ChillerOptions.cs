using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.GameOptions.OptionTypes;

namespace ReachForStars.Roles.Impostors.Chiller
{
    public class ChillerOptions : AbstractOptionGroup<FreezerRole>
    {
        public override string GroupName => "Chiller Options";
        [ModdedToggleOption("Chiller Can Vent")]
        public bool CanVent { get; set; } = true;
    }
}