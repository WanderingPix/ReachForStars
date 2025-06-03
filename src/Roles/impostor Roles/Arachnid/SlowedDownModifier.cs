using UnityEngine;
using MiraAPI.Modifiers.Types;

namespace ReachForStars.Roles.Impostors.Arachnid
{
    public class SlowedDownModifier : TimedModifier
    {
        public override float Duration => 0.1f;

        public override string ModifierName => "Slowed Down";

        public override string GetHudString()
        {
            return "You are slowed down!";
        }

        public override bool ShowInFreeplay => false;
        public override void FixedUpdate()
        {
            Player.MyPhysics.body.velocity *= new Vector2(0.4f, 0.2f);
        }
    }
}