using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiraAPI.GameOptions;
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
        public override Color GroupColor => CustomRoleSingleton<WitchRole>.Instance.RoleColor;
        public ModdedNumberOption SuccessfulKillsQuota = new ModdedNumberOption("Required assassination Count", 3f, 3f, 1f, 6f, MiraAPI.Utilities.MiraNumberSuffixes.None);
    }
}