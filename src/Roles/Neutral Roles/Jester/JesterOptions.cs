using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiraAPI.GameOptions;
using MiraAPI.GameOptions.OptionTypes;

namespace ReachForStars.Roles.Neutrals.Jester
{
    public class JesterOptions : AbstractOptionGroup<JesterRole>
    {
        public override string GroupName => "Jester Options";
        public ModdedToggleOption CanVent = new ModdedToggleOption("Jester Can Vent", true);
        public ModdedToggleOption CanCallMeeting = new ModdedToggleOption("Jester Can Call Meetings", false);
        public ModdedToggleOption CanReportBodies = new ModdedToggleOption("Jester Can Report Dead Bodies", false);
    }
}