using System;
using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;

namespace ReachForStars.Roles.Neutrals.Jester
{
    public class JesterOptions : AbstractOptionGroup<JesterRole>
    {
        public override string GroupName => "Jester Settings";

        [ModdedToggleOption("Jester Can Call Meeting")]
        public bool CanCallMeeting { get; set; } = false;
    }
}