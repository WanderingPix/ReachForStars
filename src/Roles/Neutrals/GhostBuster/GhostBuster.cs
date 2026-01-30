using System.Collections.Generic;
using MiraAPI.GameOptions;
using MiraAPI.Hud;
using MiraAPI.Modifiers;
using MiraAPI.Roles;
using MiraAPI.Utilities;
using ReachForStars.Utilities;
using Rewired;
using UnityEngine;

namespace ReachForStars.Roles.Neutrals.GhostBuster;

public class GhostBusterRole : CrewmateRole, ICustomRole
{
    public string RoleName => "Ghost Buster";

    public string RoleDescription => "Catch them ghosts";

    public string RoleLongDescription => "E";
    
    public Color RoleColor => RFSPalette.GhostBusterColor;

    public ModdedRoleTeams Team => ModdedRoleTeams.Custom;
    
    private List<PlayerControl> absorbedPlayers = new();

    public void AddAbsorbedPlayer(PlayerControl player)
    {
        absorbedPlayers.Add(player);
        if (absorbedPlayers.Count >= (int)OptionGroupSingleton<GhostBusterOptions>.Instance.GhostQuota.Value)
        {
            Player.AddModifier<NeutralWinner>();
            if (Player.AmOwner)
            {
                HudManager.Instance.SetHudActive(Player, this, true);
            }
        }
        HudManager.Instance.SpawnTextOverlay("+1 Ghost");
    }

    public CustomRoleConfiguration Configuration => new(this)
    {
        Icon = Assets.GhostbusterRoleIcon,
    };
}