using UnityEngine;
using MiraAPI.Modifiers.Types;
using MiraAPI.Utilities;
using MiraAPI.Modifiers;

namespace ReachForStars.Roles.Impostors.Arachnid
{
    public class SlowedDownModifier : TimedModifier
    {
        public override float Duration => 10f;
        public override void OnDeactivate()
        {
            Player.MyPhysics.body.velocity *= new Vector2(2.5f, 5f);
        }
        public override void FixedUpdate()
        {
            if (!TimerActive)
            {
                return;
            }

            if (TimeRemaining > 0 && Player.GetNearestObjectOfType<Glue>(0.3f, new ContactFilter2D().NoFilter()) != null)
            {
                TimeRemaining -= Time.fixedDeltaTime;
                ResumeTimer();
            }
            if (TimeRemaining == 0) Player.RemoveModifier<SlowedDownModifier>();

            if (Helpers.CheckChance(3))
            {
                GameObject droplet = new GameObject("Droplet");
                droplet.transform.position = Player.GetTruePosition();
                droplet.AddComponent<SpriteRenderer>().sprite = GetRandomDropletSprite();
            }
        }
        static Sprite[] Droplets = new[]
        {
            Assets.Droplet0.LoadAsset(),
            Assets.Droplet1.LoadAsset(),
            Assets.Droplet2.LoadAsset(),
            Assets.Droplet3.LoadAsset(),
            Assets.Droplet4.LoadAsset()
         };
        public static Sprite GetRandomDropletSprite()
        {
            System.Random rng = new System.Random();
            int index = rng.Next(0, 5);
            return Droplets[index];
        }
        public override string ModifierName => "Slowed Down";

        public override string GetHudString()
        {
            return "You are slowed down!";
        }

        public override bool ShowInFreeplay => false;
    }
}