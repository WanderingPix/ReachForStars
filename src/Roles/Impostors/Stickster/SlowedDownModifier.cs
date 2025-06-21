using MiraAPI.GameOptions;
using MiraAPI.Modifiers.Types;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Stickster
{
    public class SlowedDownModifier : TimedModifier
    {
        private readonly Vector2 Multiplier = new(OptionGroupSingleton<SticksterOptions>.Instance.SlowedDownSpeed.Value,
            OptionGroupSingleton<SticksterOptions>.Instance.SlowedDownSpeed.Value / 2);

        public override float Duration => 3f;
        public override string ModifierName => "Slowed Down";

        public override bool ShowInFreeplay => false;

        public override void OnDeactivate()
        {
            Player.MyPhysics.body.velocity *= new Vector2(1f / Multiplier.x, 1f / Multiplier.y);
        }

        public override string GetHudString()
        {
            return "You are slowed down!";
        }
    }
}