using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Modifiers;
using ReachForStars.Translation;

namespace ReachForStars.Roles.Impostors.Witch;
public class RoleBlock : CustomActionButton<PlayerControl>
{
    public override string Name => btnName.GetTranslatedText();
    public TranslationPool btnName = new TranslationPool
    (
        english: "Role Block",
        french: "Bloquer",
        spanish: "Bloque de roles",
        russian: "Ролевой блок",
        italian: "Blocco dei Ruoli"
    );

    public override float Cooldown => 5;
    public override float EffectDuration => 0;
    public override int MaxUses => 1;

    public override ButtonLocation Location => ButtonLocation.BottomRight;

    public override LoadableAsset<Sprite> Sprite => Assets.RoleblockButton;

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is WitchRole;
    }

    public override PlayerControl? GetTarget()
    {
        return PlayerControl.LocalPlayer.GetClosestPlayer(true, Distance, false);
    }

    public override void SetOutline(bool active)
    {
        Target?.cosmetics.SetOutline(active, new Il2CppSystem.Nullable<Color>(Palette.ImpostorRed));
    }

    public override bool IsTargetValid(PlayerControl? target)
    {
        return true;
    }
    protected override void OnClick()
    {
        Target.AddModifier<RoleBlockedModifier>();
    }

    public override void OnEffectEnd()
    {
    }
}
