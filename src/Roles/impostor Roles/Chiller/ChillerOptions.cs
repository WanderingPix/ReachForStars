using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.GameOptions.OptionTypes;
using ReachForStars.Translation;

namespace ReachForStars.Roles.Impostors.Chiller
{
    public class ChillerOptions : AbstractOptionGroup<FreezerRole>
    {
        public override string GroupName => groupName.GetTranslatedText();

        public TranslationPool groupName = new
        (
            english: "Chiller Options",
            spanish: "",
            portuguese: "",
            french: "Options Refrigirateur",
            russian: "",
            italian: ""
        );
        public ModdedToggleOption CanVent { get; set; } = new(Canvent.GetTranslatedText(), true);
        public static TranslationPool Canvent = new
        (
            english: "Chiller Can Vent",
            french: "Refrigirateur peut utiliser les conduits",
            portuguese: "",
            spanish: "",
            russian: "",
            italian: ""
        );
    }
}