using System.Text;
using MiraAPI.Events;
using MiraAPI.Events.Vanilla.Gameplay;
using MiraAPI.Events.Vanilla.Player;
using MiraAPI.GameOptions;
using MiraAPI.Modifiers;
using MiraAPI.Patches.Stubs;
using MiraAPI.Roles;
using ReachForStars.Utilities;
using UnityEngine;

namespace ReachForStars.Roles.Neutrals.Pirate;

public class PirateRole : CrewmateRole, ICustomRole
{
    public override bool IsAffectedByComms => false;
    public string RoleName => "Pirate";
    public string RoleDescription => "";
    public string RoleLongDescription => "Collect 1000 gold to win!" +"\nYou can gain gold by:\n" +
                                         "- Doing Tasks\n" +
                                         "- Stealing from players\n" +
                                         "- Looking for treasure";
    public Color RoleColor => RFSPalette.PirateRoleColor;
    public ModdedRoleTeams Team => ModdedRoleTeams.Custom;
    public int gold = 0;
    public CustomRoleConfiguration Configuration => new(this)
    {
        UseVanillaKillButton = false,
        IntroSound = Assets.MoneySfx,
        CanGetKilled = true,
        CanUseVent = false,
        CanUseSabotage = false,
        TasksCountForProgress = false,
        Icon = Assets.PirateRoleIcon
    };

    public void IncreaseGold(int amount)
    {
        gold += amount;
        HudManager.Instance.FadeScreen(Color.yellow, new(1, 1, 0, 0), 0.3f);
        SoundManager.Instance.PlaySound(Assets.MoneySfx.LoadAsset(), false);
        if (gold >= 1000) PlayerControl.LocalPlayer.RpcAddModifier<NeutralWinner>();
    }

    public override void Initialize(PlayerControl p)
    {
        RoleBehaviourStubs.Initialize(this, p);
    }

    public override bool DidWin(GameOverReason gameOverReason)
    {
        return Player.HasModifier<NeutralWinner>();
    }

    public StringBuilder SetTabText()
    {
        return new StringBuilder(RoleLongDescription
                                 + "\n\n\n" +
                                 $"<color=#{ColorUtility.ToHtmlStringRGBA(Color.yellow)}><b> Gold: {gold} </b></color>");
    }

    [RegisterEvent]
    public static void OnTaskComplete(CompleteTaskEvent e)
    {
        if (e.Player.AmOwner && e.Player.Data.Role is PirateRole pirate)
        {
            pirate.IncreaseGold(OptionGroupSingleton<PirateOptions>.Instance.GoldPerTask);
        }
    }
}