using AmongUs.GameOptions;
using MiraAPI.GameOptions;
using MiraAPI.Modifiers;
using MiraAPI.Patches.Stubs;
using MiraAPI.Roles;
using UnityEngine;

namespace ReachForStars.Roles.Neutrals.Jester;

public class JesterRole : ImpostorRole, ICustomRole
{
    public override bool IsAffectedByComms => false;
    public string RoleName => "Jester";
    public string RoleDescription => "Get voted out!";
    public string RoleLongDescription => "Be as suspicious as possible to get ejected!";
    public Color RoleColor => RFSPalette.JesterColor;
    public ModdedRoleTeams Team => ModdedRoleTeams.Custom;

    public CustomRoleConfiguration Configuration => new(this)
    {
        UseVanillaKillButton = false,
        CanGetKilled = true,
        CanUseVent = true,
        CanUseSabotage = false,
        TasksCountForProgress = false,
        IntroSound = Assets.JesterIntroSfx,
        Icon = Assets.JesterIcon
    };

    public string GetCustomEjectionMessage(NetworkedPlayerInfo player)
    {
        var message = "\"You've all been fooled!\\n P was the Jester!\n\n\"";
        message = message.Replace("P", player.PlayerName);
        return message;
    }

    public override void Initialize(PlayerControl p)
    {
        RoleBehaviourStubs.Initialize(this, p);
        if (Player != PlayerControl.LocalPlayer) return;
        if (!OptionGroupSingleton<JesterOptions>.Instance.CanCallMeeting && Player == PlayerControl.LocalPlayer) ShipStatus.Instance.EmergencyButton.enabled = false;
    }

    public override void Deinitialize(PlayerControl p)
    {
        ShipStatus.Instance.EmergencyButton.enabled = true;
    }


    public override void SpawnTaskHeader(PlayerControl playerControl)
    {
        // remove existing task header.
    }

    public override bool DidWin(GameOverReason gameOverReason)
    {
        return Player.HasModifier<NeutralWinner>();
    }

    public override void OnDeath(DeathReason deathreason)
    {
        if (deathreason == DeathReason.Exile)
        {
            Player.AddModifier<NeutralWinner>();
            SoundManager.instance.PlaySound(Assets.JesterIntroSfx.LoadAsset(), false, 0.7f);
        }

        Player.StartCoroutine(Player.CoSetRole((RoleTypes)RoleId.Get(typeof(NeutralGhost)), true));
    }
}