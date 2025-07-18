﻿using System.Collections.Generic;
using MiraAPI.GameOptions;
using MiraAPI.Roles;
using ReachForStars.Translation;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Stickster;

public class SticksterRole : ImpostorRole, ICustomRole
{
    public TranslationPool RoleDescLong = new(
        "Spill glue to slow down crewmates!",
        "Derrama pegamento para ralentizar a los tripulantes!",
        "Verse de la colle pour ralentir l'équipage!",
        "Разлей клей чтобы замедлять экипаж!"
        //italian: ""
    );

    public TranslationPool RoleDescShort = new(
        "Spill glue all over the map!",
        "¡Derrama pegamento por todo el mapa!",
        "Verse de la colle partout sur la carte!",
        "Пролей клей по всей карте!"
        //italian: ""
    );

    public List<Glue> PlacedGlues { get; set; } = new();

    public string RoleDescriptionLong => RoleDescLong.GetTranslatedText();
    public override bool IsAffectedByComms => false;
    public string RoleName => "Stickster";
    public string RoleDescription => RoleDescShort.GetTranslatedText();
    public string RoleLongDescription => RoleDescription;
    public Color RoleColor => Palette.ImpostorRed;
    public ModdedRoleTeams Team => ModdedRoleTeams.Impostor;

    public CustomRoleConfiguration Configuration => new(this)
    {
        IntroSound = Assets.SticksterIntroSFX,
        TasksCountForProgress = false,
        Icon = Assets.SticksterIcon
    };

    public override void SpawnTaskHeader(PlayerControl playerControl)
    {
        // remove existing task header.
    }

    public override void OnMeetingStart()
    {
        if (OptionGroupSingleton<SticksterOptions>.Instance.DoesGlueDespawn)
        {
            foreach (var g in PlacedGlues) g.gameObject.Destroy();

            PlacedGlues.Clear();
        }
    }

    public override bool DidWin(GameOverReason gameOverReason)
    {
        return GameManager.Instance.DidImpostorsWin(gameOverReason);
    }
}