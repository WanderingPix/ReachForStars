using System;
using MiraAPI.Hud;
using MiraAPI.Utilities;
using MiraAPI.Utilities.Assets;
using ReachForStars.Roles.Impostors.Phaser;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace ReachForStars.Utilities.Buttons;

public abstract class TargetedPositionActionButton : CustomActionButton
{
    public SpriteRenderer targetRenderer;
    public override void ClickHandler()
    {
        if (!CanClick())
        {
            return;
        }
        
        EffectActive = !EffectActive;
        
        OnClick();
        
        if (EffectActive)
        {
            targetRenderer = new GameObject().AddComponent<SpriteRenderer>();
            targetRenderer.gameObject.layer = LayerMask.NameToLayer("UI");
            targetRenderer.sprite = Assets.Circle.LoadAsset();
            var btn = targetRenderer.gameObject.AddComponent<PassiveButton>();
            btn.ClickMask = targetRenderer.gameObject.AddComponent<CircleCollider2D>();
            btn.Colliders = new([btn.ClickMask]);
            btn.OnClick = new();
            btn.OnClick.AddListener(new Action(() =>
            {
                if (IsTargetValid(targetRenderer.transform.position)) return;
                OnSelectTargetPosition(targetRenderer.transform.position);
                EffectActive = false;
                ResetCooldownAndOrEffect();
                targetRenderer.gameObject.Destroy();
                Timer = Cooldown;
            }));
            btn.OnMouseOut = new();
            btn.OnMouseOver = new();
        }
        else
        {
            targetRenderer.gameObject.Destroy();
        }
    }

    public override void FixedUpdateHandler(PlayerControl playerControl)
    {
        base.FixedUpdateHandler(playerControl);
        if (EffectActive && targetRenderer != null)
        {
            targetRenderer.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    public abstract void OnSelectTargetPosition(Vector2 target);

    public abstract bool IsTargetValid(Vector2 pos);
    
    public abstract LoadableAsset<Sprite> TargetSprite { get; }
}