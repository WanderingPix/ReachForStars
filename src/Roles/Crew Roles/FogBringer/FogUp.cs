using MiraAPI.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using Reactor.Utilities;
using System.Collections;
using UnityEngine;
using ReachForStars.Networking;
using ReachForStars.Translation;
using MiraAPI.Roles;
using Rewired;
using AmongUs.GameOptions;
using Hazel;
using InnerNet;

namespace ReachForStars.Roles.Crewmates.FogBringer
{
    public class FogUp : CustomActionButton
    {
        public override string Name => ButtonName.GetTranslatedText();

        public TranslationPool ButtonName = new TranslationPool(
            english: "Shoot",
            spanish: "Disparar",
            portuguese: "Atirar",
            french: "Tirer",
            russian: "выстрелить",
            italian: "Sparare"
        );
        public override float Cooldown => 25;
        public override float EffectDuration => 3;

        public override int MaxUses => 1;

        public override LoadableAsset<Sprite> Sprite => Assets.Shoot;

        public override bool Enabled(RoleBehaviour? role)
        {
            return role is FogBringerRole;
        }

        protected override void OnClick()
        {
            PlayerControl.LocalPlayer.RpcFogUp();
        }

        public override void OnEffectEnd()
        {
        }
    }
}