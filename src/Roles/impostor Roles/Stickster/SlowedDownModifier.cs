using UnityEngine;
using MiraAPI.Modifiers.Types;
using MiraAPI.Utilities;
using MiraAPI.Modifiers;
using MiraAPI.Utilities.Assets;
using MiraAPI.GameOptions;

namespace ReachForStars.Roles.Impostors.Stickster
{
    public class SlowedDownModifier : TimedModifier
    {
        public override float Duration => 3f;
        public override void OnDeactivate()
        {
            Player.MyPhysics.body.velocity *= new Vector2(2.5f, 5f);
        }
        Vector2 Multiplier = new Vector2(OptionGroupSingleton<SticksterOptions>.Instance.SlowedDownSpeed.Value, OptionGroupSingleton<SticksterOptions>.Instance.SlowedDownSpeed.Value/2);
        public override string ModifierName => "Slowed Down";

        public override string GetHudString()
        {
            return "You are slowed down!";
        }

        public override bool ShowInFreeplay => false;
    }
}