using MiraAPI.GameOptions;
using MiraAPI.Modifiers;
using MiraAPI.Patches.Stubs;
using MiraAPI.Roles;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Neutrals.Jester;

public class JesterRole : ImpostorRole, ICustomRole
{
    public TranslationPool EjectMessage = new(
        "You've all been fooled!\n P was the Jester!\n\n", //PlayerName is referenced as P
        french: "Vous avez tous été dupés!\n P était le Plaisantin!\n\n",
        spanish: "¡Todos han sido engañados!\n P era el Bufón!\n\n",
        russian: "Вы все были надурачены!\n P был шутом!\n\n" // i hope i done it right :skull:
        //italian: ""
    );

    public TranslationPool roleName => new TranslationPool(
        english: "Jester",
        french: "Plaisantin",
        spanish: "Bufón",
        russian: "Шут"
        //italian: "Pagliaccio"
    );

    public TranslationPool RoleDescShort => new TranslationPool
    (
        english: "Fool The Crew!",
        french: "Trollez l'équipage!",
        spanish: "¡Engaña a la tripulación!",
        russian: "Надурачь весь экипаж!"
        //italian: ""
    );

    public override bool IsAffectedByComms => false;

    public TranslationPool RoleDescLong => new TranslationPool
    (
        english: "Get voted out to win.",
        french: "Faites-vous ejecter pour gagner.",
        spanish: "Ser expulsado para ganar",
        russian: "Будь выброшен, чтобы выиграть"
        //italian: ""
    );

    public string RoleName => roleName.GetTranslatedText();
    public string RoleDescription => RoleDescShort.GetTranslatedText();
    public string RoleLongDescription => RoleDescLong.GetTranslatedText();
    public Color RoleColor => new Color(1f, 0.18f, 0.81f, 1f);
    public ModdedRoleTeams Team => ModdedRoleTeams.Custom;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        UseVanillaKillButton = false,
        CanGetKilled = true,
        CanUseVent = true,
        CanUseSabotage = false,
        TasksCountForProgress = false,
        IntroSound = Assets.JesterIntroSFX,
        Icon = Assets.jesterIcon
    };

    public string GetCustomEjectionMessage(NetworkedPlayerInfo player)
    {
        string message = EjectMessage.GetTranslatedText();
        message = message.Replace("P", player.PlayerName);
        return message;
    }

    public override void Initialize(PlayerControl p)
    {
        RoleBehaviourStubs.Initialize(this, p);
        if (!OptionGroupSingleton<JesterOptions>.Instance.CanCallMeeting && Player == PlayerControl.LocalPlayer)
            ShipStatus.Instance.EmergencyButton.enabled = false;
    }

    public override void Deinitialize(PlayerControl p)
    {
        HudManager.Instance.ReportButton.Show();
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
            SoundManager.instance.PlaySound(Assets.JesterIntroSFX.LoadAsset(), false, 0.7f);
        }
    }
}