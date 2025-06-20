using System.Collections.Generic;
using System.Linq;
using AmongUs.GameOptions;
using MiraAPI.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Modifiers;
using MiraAPI.Patches.Stubs;
using MiraAPI.Roles;
using MiraAPI.Utilities;
using ReachForStars.Roles.Neutrals.BountyHunter;
using ReachForStars.Translation;
using Reactor.Utilities.Extensions;
using UnityEngine;
using Random = System.Random;

namespace ReachForStars.Roles.Neutrals.Roles;

public class BountyHunterRole : ImpostorRole, ICustomRole
{
    public PlayerControl Target;
    public int SuccessfulKills;
    public BountyHud hud;
    public bool HasWon;

    public TranslationPool roleDescShort = new
    (
        "Kill your targets to win!",
        "¡Mata a tus objetivos para ganar!",
        "Tuez vos primes pour gagner!",
        "Убей свои цели, чтобв победить!"
        //italian: "Asassina i tuoi target per vincere!"
    );

    public TranslationPool roleName = new(
        "Bounty Hunter",
        french: "Chasseur De Prime",
        spanish: "cazarrecompensas",
        russian: "Охотник за Головами"
        //italian: "Sicario"
    );

    public override bool IsAffectedByComms => false;
    public string RoleName => roleName.GetTranslatedText();
    public string RoleDescription => roleDescShort.GetTranslatedText();
    public string RoleLongDescription => RoleDescription;
    public Color RoleColor => RFSPalette.BountyHunterColor;
    public ModdedRoleTeams Team => ModdedRoleTeams.Custom;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        UseVanillaKillButton = false,
        CanGetKilled = true,
        CanUseVent = false,
        GhostRole = (RoleTypes)RoleId.Get<NeutralGhost>(),
        TasksCountForProgress = false,
        RoleHintType = RoleHintType.TaskHint
    };

    public override void Initialize(PlayerControl player)
    {
        RoleBehaviourStubs.Initialize(this, player);

        if (player == PlayerControl.LocalPlayer)
        {
            CustomButtonSingleton<BountyKill>.Instance.Button.Show();

            hud = Instantiate(Assets.BountyPrefab.LoadAsset(), HudManager.Instance.transform)
                .AddComponent<BountyHud>();
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