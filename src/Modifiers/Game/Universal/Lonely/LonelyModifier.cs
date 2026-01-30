using System;
using MiraAPI.Events;
using MiraAPI.Events.Vanilla.Gameplay;
using MiraAPI.Events.Vanilla.Map;
using MiraAPI.Events.Vanilla.Player;
using MiraAPI.GameOptions;
using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using MiraAPI.Networking;
using MiraAPI.Utilities;
using ReachForStars.Utilities;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace ReachForStars.Modifiers.Game.Universal.Lonely;

public class LonelyModifier : GameModifier
{
    public override string ModifierName => "Lonely";

    public float DangerValue;

    public override string GetDescription()
    {
        return "Avoid staying with lots of players.";
    }

    public override int GetAssignmentChance()
    {
        return OptionGroupSingleton<LonelyModifierOptions>.Instance.AssignmentChance;
    }

    public override int GetAmountPerGame()
    {
        return OptionGroupSingleton<LonelyModifierOptions>.Instance.AmountPerGame;
    }

    public DangerMeter meter;

    public override void OnActivate()
    {
        if (!Player.AmOwner) return;

        var pref = UnityObject.FindObjectOfType<DangerMeter>(true);
        meter = UnityObject.Instantiate(pref, HudManager.Instance.transform);
        meter.gameObject.SetActive(true);
        meter.transform.localPosition = pref.transform.parent.localPosition + Vector3.down;
        meter.safeColor = Palette.CrewmateRoleHeaderDarkBlue;
        meter.cautionColor = Palette.CrewmateBlue;
        meter.dangerColor = Color.white;
    }

    public override void FixedUpdate()
    {
        if (!Player.AmOwner) return;
        if (Player.Data.IsDead) return;
        
        var nearbyPlayers = Helpers.GetClosestPlayers(Player, 2, false);

        float toBeAdded = 0;
        if (nearbyPlayers.Count == 0)
        {
            toBeAdded = -0.75f * Time.deltaTime;
        }
        else
        {
            foreach (var p in nearbyPlayers)
            {
                toBeAdded += Time.deltaTime;
            }
        }

        DangerValue += toBeAdded;
        DangerValue = Mathf.Clamp(DangerValue, 0, 6.5f);
        meter.SetDangerValue((int)DangerValue, 0);
        if (DangerValue > 6)
        {
            Player.RpcCustomMurder(Player);
            meter.gameObject.Destroy();
        }
    }
}