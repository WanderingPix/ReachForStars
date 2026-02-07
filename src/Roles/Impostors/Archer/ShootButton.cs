using System;
using MiraAPI.Hud;
using MiraAPI.Utilities;
using MiraAPI.Utilities.Assets;
using ReachForStars.Utilities;
using ReachForStars.Utilities.Buttons;
using Reactor.Utilities.Extensions;
using Rewired;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Archer;

public class ShootButton : TargetedPositionActionButton
{
    public override void OnSelectTargetPosition(Vector2 position)
    {
        
    }

    public override bool IsTargetValid(Vector2 pos)
    {
        return Helpers.GetRoom(pos);
    }

    protected override void OnClick()
    {
    }

    public override bool Enabled(RoleBehaviour role)
    {
        return role is ArcherRole;
    }

    public override string Name => "Shoot";
    
    public override float Cooldown => 60;

    public override LoadableAsset<Sprite> TargetSprite => Assets.Circle;

    public override LoadableAsset<Sprite> Sprite => Assets.PlaceHolder;
}