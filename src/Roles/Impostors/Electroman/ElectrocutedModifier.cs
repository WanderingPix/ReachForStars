using System.Collections;
using System.Linq;
using AmongUs.Data;
using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using MiraAPI.Networking;
using MiraAPI.Utilities;
using PowerTools;
using ReachForStars.Utilities;
using Reactor.Utilities;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Electroman;

public class ElectrocutedModifier : TimedModifier
{
    public override string ModifierName { get; } = "Electrocuted";
    public override float Duration { get; } = 3f;
    public ElectromanRole Electroman { get; set; }
    public PlayerTask Task { get; set; }

    public override void OnActivate()
    {
        if (Player == PlayerControl.LocalPlayer && DataManager.settings.gameplay.screenShake)
            HudManager.Instance.StartCoroutine(Effects.Shake(HudManager.Instance.PlayerCam.transform, 3f, 0.25f, true,
                true));
        SoundManager.Instance.PlaySoundAtLocation(Assets.ElectricSound.LoadAsset(), Player.GetTruePosition(),
            PlayerControl.LocalPlayer.GetTruePosition(), SoundManager.Instance.SfxChannel);
        Player.cosmetics.currentPet.SetScared();
        Player.cosmetics.Toggle(false, true, false, false, true);
        Player.NetTransform.Halt();
        Player.moveable = false;
        Player.AnimateCustom(Assets.ElectrocutedAnimation.LoadAsset());
    }

    public override void OnTimerComplete()
    {
        Coroutines.Start(CoDie());

        if (Electroman.Player == PlayerControl.LocalPlayer) NotifyOfVictimDeath(Task);
    }

    public void NotifyOfVictimDeath(PlayerTask task)
    {
        var notification = Helpers.CreateAndShowNotification(
            $"{Player.Data.PlayerName} got zapped whilst doing {TranslationController.Instance.GetString(Task.TaskType)}",
            Color.yellow, null, Assets.EnergyCounters.Last().LoadAsset());
        notification.transform.localPosition = new Vector3(0f, 1f, -20f);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        var closestPlayer = Player.GetClosestPlayer(false, 1f);
        if (closestPlayer && !closestPlayer.HasModifier<ElectrocutedModifier>())
        {
            var mod = closestPlayer.AddModifier<ElectrocutedModifier>();
            mod.Electroman = Electroman;
            mod.Task = Task;
        }
    }

    public IEnumerator CoDie()
    {
        yield return new WaitForAnimationFinish(Player.GetAnimator(), Assets.AshDeathAnimation.LoadAsset());
        Player.moveable = true;
        Player.cosmetics.Toggle(true, true, true, true, true);
        Electroman.Player.CustomMurder(Player, MurderResultFlags.Succeeded, false, false, false, false, false);

        var Ash = new GameObject($"Ashes{Player.PlayerId}");
        var rend = Ash.AddComponent<SpriteRenderer>();

        rend.sprite = Assets.Ash.LoadAsset();
        Vector3 ppos = Player.GetTruePosition();
        Ash.transform.position = new Vector3(ppos.x, ppos.y + 0.2f, ppos.z);
        Ash.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        rend.material = new Material(Shader.Find("Unlit/PlayerShader"));
        PlayerMaterial.SetColors(Player.cosmetics.ColorId, rend);

        yield break;
    }
}