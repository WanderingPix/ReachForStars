using System.Collections.Generic;
using MiraAPI.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Roles;
using ReachForStars.Translation;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Chiller;


public class FreezerRole : ImpostorRole, ICustomRole
{
    public string RoleName => rolename.GetTranslatedText();
    public TranslationPool rolename = new
    (
        english: "Chiller",
        french: "Refrigirateur",
        spanish: "",
        portuguese: "",
        russian: "Охладитель",
        italian: ""
    );
    public string RoleDescription => RoleDescShort.GetTranslatedText();
    public TranslationPool RoleDescShort = new
    (
        english: "Freeze bodies to hide identities!",
        spanish: "",
        portuguese: "",
        french: "Figez des cadavres pour cacher leurs identités!",
        russian: "Замораживай трупы чтобы избежать личностей!",
        italian: ""
    );

    public string RoleLongDescription => RoleDescLong.GetTranslatedText();
    public TranslationPool RoleDescLong = new
    (
        english: "Freeze bodies to stop the crewmates from reporting them!",
        spanish: "",
        portuguese: "",
        french: "Figez les cadavres pour empécher les coéquipiers de lés trouver!",
        russian: "",
        italian: ""
    );
    public Color RoleColor => Palette.ImpostorRed;
    public ModdedRoleTeams Team => ModdedRoleTeams.Impostor;

    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        UseVanillaKillButton = true,
        CanGetKilled = true,
        CanUseVent = OptionGroupSingleton<ChillerOptions>.Instance.CanVent,
        Icon = Assets.ChillerIcon
    };

    public override void SpawnTaskHeader(PlayerControl playerControl)
    {
        // remove existing task header.
    }

    public List<Vent> MinerVents;

    public override bool DidWin(GameOverReason gameOverReason)
    {
        return GameManager.Instance.DidImpostorsWin(gameOverReason);
    }
}
