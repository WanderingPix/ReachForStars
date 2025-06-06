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

namespace ReachForStars.Roles.Neutrals.Roles;

public class BountyHunterRole : ImpostorRole, ICustomRole
{
    public string RoleName => roleName.GetTranslatedText();
    public TranslationPool roleName = new TranslationPool
    (
        english: "Bounty Hunter",
        french: "Chasseur De Prime",
        spanish: "",
        portuguese: "",
        russian: "Охотник за Головами",
        italian: "Mercenario"
    );
    PlayerControl Target;
    public string RoleDescription => "Make sure your targets are dead";
    public string RoleLongDescription => RoleDescription;
    public int SuccessfulKills = 0;
    public Color RoleColor => new Color(1f, 0.12f, 0.54f, 1f);
    public ModdedRoleTeams Team => ModdedRoleTeams.Custom;
    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        UseVanillaKillButton = true,
        CanGetKilled = true,
        CanUseVent = false,
    };
    public override void Initialize(PlayerControl player)
    {
        RoleBehaviourStubs.Initialize(this, player);
        HudManager.Instance.KillButton.Show();
        SetUpUI();
        GenerateNewBountyTarget();
    }
    public void SetUpUI()
    {
        if (Hud) Hud.gameObject.DestroyImmediate();
        GameObject go = new GameObject("BountyHud");
        go.transform.SetParent(HudManager.Instance.transform);
        AspectPosition pos = go.AddComponent<AspectPosition>();
        pos.Alignment = AspectPosition.EdgeAlignments.Top;
        pos.DistanceFromEdge = new Vector3(0f, 1f, 0f);
        pos.AdjustPosition();
        Hud = go.AddComponent<BountyHud>();
        Hud.Cur = SuccessfulKills;
    }
    public override void Deinitialize(PlayerControl targetPlayer)
    {
    }

    public override void SpawnTaskHeader(PlayerControl playerControl)
    {
        // remove existing task header.
    }

    public override bool DidWin(GameOverReason gameOverReason)
    {
        return gameOverReason == CustomGameOver.GameOverReason<BountyHunterWin>();
    }
    BountyHud Hud;
    private void GenerateNewBountyTarget()
    {
        Random rnd = new Random();
        List<PlayerControl> Playerpool = Helpers.GetAlivePlayers().Where(x => x != PlayerControl.LocalPlayer && x != MeetingHud.Instance.exiledPlayer).ToList();
        int index = rnd.Next(Playerpool.Count);
        Target = Playerpool[index];

        SetUpUI();
        Hud.SetTarget(Target);
    }

    public override void OnVotingComplete()
    {
        GenerateNewBountyTarget();
    }

    public override PlayerControl FindClosestTarget()
    {
        if (PlayerControl.LocalPlayer.GetClosestPlayer(true, 1f, false) == Target)
        {
            return Target;
        }
        else return null;
    }
    public void OnTargetKill()
    {
        SuccessfulKills++;
        Hud.SetDead();
        if (SuccessfulKills >= ((int)OptionGroupSingleton<BountyHunterOptions>.Instance.SuccessfulKillsQuota))
        {
            CustomGameOver.Trigger<BountyHunterWin>([Player.Data]);
        }
    }
}
