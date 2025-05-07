using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiraAPI.GameOptions;
using MiraAPI.GameOptions.OptionTypes;

namespace ReachForStars.Roles.Impostors.Chiller
{
    public class ChillerOptions : AbstractOptionGroup<FreezerRole>
    {
        public override string GroupName => "Chiller Options";
        public ModdedToggleOption CanVent = new ModdedToggleOption("Chiller Can Vent", true);
    }
}