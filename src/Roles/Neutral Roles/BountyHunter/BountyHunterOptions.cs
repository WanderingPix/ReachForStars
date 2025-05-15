using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.GameOptions.OptionTypes;
using MiraAPI.Roles;
using ReachForStars.Roles.Impostors.Witch;
using ReachForStars.Roles.Neutrals.Roles;
using UnityEngine;

namespace ReachForStars.Roles.Neutrals.BountyHunter
{
    public class BountyHunterOptions : AbstractOptionGroup<BountyHunterRole>
    {
        public override string GroupName => "Bounty Hunter Options";

        [ModdedNumberOption("Required Assassinations Count", 3f, 6f, 1f, MiraAPI.Utilities.MiraNumberSuffixes.None)]
        public float SuccessfulKillsQuota { get; set; } = 3f;
    }
}