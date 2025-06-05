using UnityEngine;
using MiraAPI.Modifiers.Types;
using MiraAPI.Utilities;
using MiraAPI.Modifiers;
using MiraAPI.Utilities.Assets;

namespace ReachForStars.Roles.Impostors.Arachnid
{
    public class SlowedDownModifier : TimedModifier
    {
        public override float Duration => 10f;
        public override void OnDeactivate()
        {
            Player.MyPhysics.body.velocity *= new Vector2(2.5f, 5f);
        }
        public void Update()
        {
            Player.MyPhysics.body.velocity *= new Vector2(0.4f, 0.2f);
            if (Helpers.CheckChance(3))
            {
                GameObject droplet = new GameObject("Droplet");
                droplet.transform.position = Player.GetTruePosition();
                droplet.AddComponent<SpriteRenderer>().sprite = GetRandomDropletSprite().LoadAsset();
            }

            Player.MyPhysics.body.velocity *= new Vector2(0.4f, 0.2f);
        }
        static LoadableResourceAsset[] Droplets = new[]
        {
            Assets.Droplet0,
            Assets.Droplet1,
            Assets.Droplet2,
            Assets.Droplet3,
            Assets.Droplet4
         };
        public static LoadableResourceAsset GetRandomDropletSprite()
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