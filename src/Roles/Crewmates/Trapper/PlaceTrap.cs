using MiraAPI.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using Reactor.Utilities;
using System.Collections;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Networking;
using TMPro;
using ReachForStars.Translation;
using MiraAPI.Roles;
using Rewired;
using AmongUs.GameOptions;
using Hazel;
using ReachForStars.Networking;

namespace ReachForStars.Roles.Crewmates.Trapper
{
    public class TrapAbility : CustomActionButton
    {
        public override string Name => ButtonName.GetTranslatedText();
        public override Color TextOutlineColor => Palette.CrewmateBlue;
        public TranslationPool ButtonName = new TranslationPool(
            english: "Trap",
            spanish: "Trampa",
            french: "piéger",
            russian: "Трапнуть",
            italian: ""
        );
        public override float Cooldown => 25;
        public override float EffectDuration => 0;

        public override int MaxUses => 3;

        public override LoadableAsset<Sprite> Sprite => Assets.PlaceTrapButton;

        public override bool Enabled(RoleBehaviour? role)
        {
            return role is TrapperRole;
        }
        protected override void OnClick()
        {
            PlayerControl.LocalPlayer.RpcPlaceTrap();
        }
        public override ButtonLocation Location => ButtonLocation.BottomRight;
    }
}
