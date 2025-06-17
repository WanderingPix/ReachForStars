using System.Collections.Generic;
using System.Linq;
using MiraAPI.Roles;
using MiraAPI.Utilities;
using ReachForStars.Roles.Neutrals.BountyHunter;
using ReachForStars.Utilities;
using MiraAPI.GameEnd;
using UnityEngine;
using ReachForStars.Translation;
using Random = System.Random;
using MiraAPI.GameOptions;
using MiraAPI.Patches.Stubs;
using Reactor.Utilities.Extensions;
using MiraAPI.Modifiers;
using TMPro;
using MiraAPI.Hud;
using Hazel;

namespace ReachForStars.Roles.Neutrals.Roles;

public class BountyHunterRole : ImpostorRole, ICustomRole
{
    public string RoleName => roleName.GetTranslatedText();
    public TranslationPool roleName = new TranslationPool
    (
        english: "Bounty Hunter",
        french: "Chasseur De Prime",
        spanish: "cazarrecompensas",

        russian: "Охотник за Головами",
        italian: "Sicario"
    );
    public PlayerControl Target;
    public string RoleDescription => roleDescShort.GetTranslatedText();
    public TranslationPool roleDescShort = new
    (
        english: "Kill your targets to win!",
        spanish: "¡Mata a tus objetivos para ganar!",
        french: "Tuez vos primes pour gagner!",
        russian: "Убей свои цели, чтобв победить!",
        italian: "Asassina i tuoi target per vincere!"
    );
    public string RoleLongDescription => RoleDescription;
    public int SuccessfulKills = 0;
    public BountyHud hud;
    public Color RoleColor => RFSPalette.BountyHunterColor;
    public ModdedRoleTeams Team => ModdedRoleTeams.Custom;
    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        UseVanillaKillButton = false,
        CanGetKilled = true,
        CanUseVent = false,
        GhostRole = (AmongUs.GameOptions.RoleTypes)RoleId.Get<NeutralGhost>(),
        TasksCountForProgress = false,
        RoleHintType = RoleHintType.TaskHint
    };
    public override void Initialize(PlayerControl player)
    {
        RoleBehaviourStubs.Initialize(this, player);

        if (player == PlayerControl.LocalPlayer)
        {
            CustomButtonSingleton<BountyKill>.Instance.Button.Show();

            hud = Object.Instantiate(Assets.BountyPrefab.LoadAsset(), HudManager.Instance.transform).AddComponent<BountyHud>();
            GenerateNewBountyTarget();
        }
        HasWon = false;
    }
    public override void Deinitialize(PlayerControl targetPlayer)
    {
        hud.gameObject.DestroyImmediate();
    }

    public override void SpawnTaskHeader(PlayerControl playerControl)
    {
        // remove existing task header.
    }
    private void GenerateNewBountyTarget()
    {
        Random rnd = new Random();
        List<PlayerControl> Playerpool = Helpers.GetAlivePlayers().Where(x => x != PlayerControl.LocalPlayer).ToList();
        int index = rnd.Next(Playerpool.Count);
        Target = Playerpool[index];

        if (hud) hud.OnNewTargetGenerated(Target.Data);
    }

    public override void OnVotingComplete()
    {
        if (!HasWon)
        {
            hud.gameObject.SetActive(true);
            GenerateNewBountyTarget();
        }
    }
    public override void OnMeetingStart()
    {
        if (!HasWon)
        {
            hud.myPlayer.gameObject.DestroyImmediate();
            hud.gameObject.SetActive(false);
        }
    }
    public bool HasWon;
    public void OnTargetKill()
    {
        SuccessfulKills++;
        hud.UpdateCount(SuccessfulKills);
        if (SuccessfulKills >= (int)OptionGroupSingleton<BountyHunterOptions>.Instance.SuccessfulKillsQuota)
        {
            Player.AddModifier<NeutralWinner>();
            CustomButtonSingleton<BountyKill>.Instance.Button.Hide();
            hud.gameObject.DestroyImmediate();
            HasWon = true;
        }
    }
    public override bool DidWin(GameOverReason gameOverReason)
    {
        return Player.HasModifier<NeutralWinner>();
    }
}
