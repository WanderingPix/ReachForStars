using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiraAPI.Modifiers.Types;

namespace ReachForStars.Roles.Neutrals
{
    public class NeutralWinner : GameModifier
    {
        public override string ModifierName => "Neutral Winner";

        public override int GetAmountPerGame()
        {
            return 0;
        }

        public override int GetAssignmentChance()
        {
            return 0;
        }
    }
}