using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;

namespace ReachForStars.Roles.Neutrals.GhostBuster;

public class ToBeSuckedModifier(PlayerControl GhostBuster) : TimedModifier
{
    public PlayerControl GB = GhostBuster; //Ghost Buster
    public override string ModifierName { get; } = "ToBeSucked";
    public override bool ShowInFreeplay { get; } = false;
    public override bool HideOnUi { get; } = true;
    public override float Duration { get; } = 5f;

    public override void OnActivate()
    {
        Player.MyPhysics.GhostSpeed *= 0.5f;
        HudManager.Instance.StartCoroutine(Effects.Slide2DWorld(Player.transform, Player.GetTruePosition(),
            GB.GetTruePosition(), 5f));
        SoundManager.Instance.PlaySound(Assets.VacuumGhostSFX.LoadAsset(), false, 0.6f, SoundManager.instance.sfxMixer);
    }

    public override void OnDeactivate()
    {
        Player.AddModifier<VacuumedModifier>(GB);
    }
}