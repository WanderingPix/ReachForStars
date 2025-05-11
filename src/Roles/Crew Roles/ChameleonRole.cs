using MiraAPI.Roles;
using TMPro;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Chameleon;


public class ChameleonRole : CrewmateRole, ICustomRole
{
    public string RoleName => "Chameleon";
    public string RoleLongDescription => "Stay invisible while not moving.";
    public string RoleDescription => RoleLongDescription;
    public Color RoleColor => Palette.White;
    public Color OptionsMenuColor => Palette.CrewmateBlue;
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;
    public override void Deinitialize(PlayerControl p)
    {
        foreach (var rend in p.GetComponentsInChildren<SpriteRenderer>())
        {
            rend.color = Color.white;
        }
    }


    public CustomRoleConfiguration Configuration => new CustomRoleConfiguration(this)
    {
        //OptionsScreenshot = ExampleAssets.Banner,
    };

    public void PlayerControlFixedUpdate(PlayerControl playerControl)
    {
        if (playerControl.MyPhysics.Velocity.magnitude > 0)
        {
            SpriteRenderer rend = playerControl.cosmetics.currentBodySprite.BodySprite;
            TextMeshPro tmp = playerControl.cosmetics.nameText;
            tmp.color = Color.Lerp(tmp.color, new Color(tmp.color.r, tmp.color.g, tmp.color.b, 1), Time.deltaTime * 4f);
            rend.color = Color.Lerp(rend.color, new Color(1, 1, 1, 1), Time.deltaTime * 4f);

            foreach (var cosmetic in playerControl.cosmetics.transform.GetComponentsInChildren<SpriteRenderer>())
            {
                cosmetic.color = Color.Lerp(cosmetic.color, new Color(1, 1, 1, 1), Time.deltaTime * 4f);
            }
        }
        else
        {
            SpriteRenderer rend = playerControl.cosmetics.currentBodySprite.BodySprite;
            TextMeshPro tmp = playerControl.cosmetics.nameText;
            tmp.color = Color.Lerp(tmp.color, new Color(tmp.color.r, tmp.color.g, tmp.color.b, playerControl.AmOwner ? 0.3f : 0), Time.deltaTime * 4f);
            rend.color = Color.Lerp(rend.color, new Color(1, 1, 1, playerControl.AmOwner ? 0.3f : 0), Time.deltaTime * 4f);

            foreach (var cosmetic in playerControl.cosmetics.transform.GetComponentsInChildren<SpriteRenderer>())
            {
                cosmetic.color = Color.Lerp(cosmetic.color, new Color(1, 1, 1, playerControl.AmOwner ? 0.3f : 0), Time.deltaTime * 4f);
            }
        }
    }
}
