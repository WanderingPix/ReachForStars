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
    public class TrapAbility : CustomActionButton<PlayerControl>
    {
        public override string Name => ButtonName.GetTranslatedText();

        public TranslationPool ButtonName = new TranslationPool(
            english: "Trap",
            spanish: "",
            portuguese: "",
            french: "piéger",
            russian: "",
            italian: ""
        );
        public override float Cooldown => 25;
        public override float EffectDuration => 0;

        public override int MaxUses => 0;

        public override LoadableAsset<Sprite> Sprite => Assets.Shoot;

        public override bool Enabled(RoleBehaviour? role)
        {
            return role is TrapperRole;
        }

        public override PlayerControl? GetTarget()
        {
            return PlayerControl.LocalPlayer.GetClosestPlayer(true, Distance, false);
        }

        public override void SetOutline(bool active)
        {
            Target?.cosmetics.SetOutline(active, new Il2CppSystem.Nullable<Color>(new Color(1f, 1f, 0f, 1f)));
        }

        public override bool IsTargetValid(PlayerControl? target)
        {
            return true;
        }
        protected override void OnClick()
        {
            PlayerControl.LocalPlayer.RpcPlaceTrap();
        }
        public override ButtonLocation Location => ButtonLocation.BottomRight;
    }
}
