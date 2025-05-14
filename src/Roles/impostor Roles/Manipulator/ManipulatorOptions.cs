using MiraAPI.GameOptions;
using MiraAPI.GameOptions.OptionTypes;

namespace ReachForStars.Roles.Impostors.Manipulator
{
    public class ManipulatorOptions : AbstractOptionGroup<ManipulatorRole>
    {
        public override string GroupName => "Witch Options";
        public ModdedNumberOption PoisonDelay = new ModdedNumberOption("Poison Kill Delay", 15f, 10f, 25f, 5f, MiraAPI.Utilities.MiraNumberSuffixes.None);
        public ModdedToggleOption Canvent = new ModdedToggleOption("Witch Can Vent", true);
        public ModdedToggleOption CanDoNormalKilling = new ModdedToggleOption("Witch Can Do Normal Killing", false);
    }
}