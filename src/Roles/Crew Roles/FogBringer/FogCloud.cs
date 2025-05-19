using MiraAPI.Roles;
using MiraAPI.Utilities.Assets;
using ReachForStars.Translation;
using ReachForStars.Utilities;
using Reactor.Utilities;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.FogBringer;

public class FogCloud : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerControl>();
        if (player != null && player.Data.Role.IsImpostor)
        {
            player.MyPhysics.SetBodyType(PlayerBodyTypes.Seeker);
        }
    }
}