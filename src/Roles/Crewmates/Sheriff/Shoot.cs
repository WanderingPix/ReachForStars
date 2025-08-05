using AmongUs.GameOptions;
using Il2CppSystem;
using MiraAPI.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Networking;
using MiraAPI.Utilities;
using MiraAPI.Utilities.Assets;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Sheriff;

public class Shoot : CustomActionButton<PlayerControl>
{
    public TranslationPool ButtonName = new(
        "Shoot",
        "Disparar",
        "Tirer",
        "выстрелить"
    );

    public override string Name => ButtonName.GetTranslatedText();
    public override float Cooldown => 25;
    public override float EffectDuration => 0;
    public override int MaxUses => 0;

    public override float Distance =>
        GameOptionsManager.Instance.CurrentGameOptions.GetInt(Int32OptionNames.KillDistance);

    public override LoadableAsset<Sprite> Sprite => Assets.Shoot;
    public override ButtonLocation Location => ButtonLocation.BottomRight;

    private int BulletCount { get; set; }

    public override void CreateButton(Transform parent)
    {
        base.CreateButton(parent);
        BulletCount = 0;
        Button.usesRemainingText.enabled = false;
        Button.usesRemainingSprite.sprite = Assets.BulletCounters[0].LoadAsset();
        Button.usesRemainingSprite.color = Palette.CrewmateBlue;
        Button.usesRemainingSprite.gameObject.SetActive(true);
    }

    public override bool Enabled(RoleBehaviour? role)
    {
        return role is SheriffRole;
    }

    public override PlayerControl? GetTarget()
    {
        return PlayerControl.LocalPlayer.GetClosestPlayer(true, Distance);
    }

    public override void SetOutline(bool active)
    {
        Target?.cosmetics.SetOutline(active, new Nullable<Color>(Palette.CrewmateBlue));
    }

    public override bool IsTargetValid(PlayerControl target)
    {
        return BulletCount > 0 && target != null;
    }

    protected override void OnClick()
    {
        if (OptionGroupSingleton<SheriffOptions>.Instance.SheriffKnowsIfRight) Notify(Target);
        PlayerControl.LocalPlayer.RpcCustomMurder(Target, playKillSound: false);
        SoundManager.Instance.PlaySound(Assets.SheriffKillSFX.LoadAsset(), false, 1f, SoundManager.instance.sfxMixer);
        BulletCount--;
        Button.usesRemainingSprite.sprite = Assets.BulletCounters[BulletCount].LoadAsset();
    }

    public void AddBullet()
    {
        if (BulletCount < 3)
        {
            BulletCount++;
            Button.usesRemainingSprite.sprite = Assets.BulletCounters[BulletCount].LoadAsset();
        }
    }

    private void Notify(PlayerControl Target)
    {
        var message = "if You're reading this, it's a bug";
        if (Target.Data.Role.TeamType == RoleTeamTypes.Impostor)
        {
            message = $"{Target.Data.PlayerName} was <color=red>an Impostor!</color>";
        }

        else
        {
            message =
                $"{Target.Data.PlayerName} was <color=#{ColorUtility.ToHtmlStringRGBA(Palette.CrewmateBlue)}>not an Impostor!</color>";
        }

        var notif = Helpers.CreateAndShowNotification(
            message,
            Color.white);
        notif.transform.localPosition = new Vector3(0f, 1f, -20f);
    }
}