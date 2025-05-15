using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.GameOptions.OptionTypes;

namespace ReachForStars.Roles.Impostors.Witch
{
    public class WitchOptions : AbstractOptionGroup<WitchRole>
    {
        public override string GroupName => "Witch Options";
        [ModdedNumberOption("Poison Kill Delay", 10f, 25f, 5f, MiraAPI.Utilities.MiraNumberSuffixes.Seconds)]
        public float PoisonDelay { get; set; } = 15f;
        [ModdedToggleOption("Witch Can Vent")]
        public bool CanVent { get; set; } = false;
        
        [ModdedToggleOption("Witch Can Do Normal Killing")]
        public bool CanDoNormalKilling { get; set; } = false;
    }
}