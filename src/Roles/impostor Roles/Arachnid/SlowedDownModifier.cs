using UnityEngine;
using MiraAPI.Modifiers.Types;
using MiraAPI.Utilities;
using MiraAPI.Modifiers;

namespace ReachForStars.Roles.Impostors.Arachnid
{
    public class SlowedDownModifier : TimedModifier
    {
        public void Update()
        {
            Player.MyPhysics.body.velocity *= new Vector2(0.4f, 0.2f);
            if (Player.GetNearestObjectOfType<Cobweb>(1f, new ContactFilter2D().NoFilter()) != null)
            {
                Player.RemoveModifier<SlowedDownModifier>();
            }
        }
        public override float Duration => 1f;
        public override string ModifierName => "Slowed Down";

        public override string GetHudString()
        {
            return "You are slowed down!";
        }

        public override bool ShowInFreeplay => false;
    }
}