using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.GameOptions.OptionTypes;

namespace ReachForStars.Roles.Neutrals.Jester
{
    public sealed class JesterOptions : AbstractOptionGroup<JesterRole>
    {
        public override string GroupName => "Jester Options";
        [ModdedToggleOption("Jester Can Vent")]
        public bool CanVent { get; set; } = true;

        [ModdedToggleOption("Jester Can Call Meeting")]
        public bool CanCallMeeting { get; set; } = true;
    }
}