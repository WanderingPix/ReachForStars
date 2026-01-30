using MiraAPI.Events;
using MiraAPI.Events.Vanilla.Gameplay;
using MiraAPI.Hud;
using MiraAPI.Modifiers;
using MiraAPI.Utilities;
using ReachForStars.Components;
using ReachForStars.Utilities;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Carrier;

public class CarryingModifier(byte PlayerId) : BaseModifier
{
    public override string ModifierName => "Carrying A Body";
    public override bool HideOnUi => true;
    public override bool ShowInFreeplay => false;
    public DeadBody Body = Helpers.GetBodyById(PlayerId);
    private SpriteRenderer Hand;

    public override void OnActivate()
    {
        Body.transform.SetParent(Player.transform);
        Body.transform.localPosition = new(0.25f, 1, 0);
        
        Hand = new GameObject("Hand").AddComponent<SpriteRenderer>();
        Hand.transform.parent = Player.transform;
        Hand.transform.localPosition = new(0, 0, -50);
        Player.StartCoroutine(Effects.Slide2D(Hand.transform, Vector2.zero, new(0f, 1), 0.1f));
        Hand.transform.localScale = new Vector3(.7f, .7f, 1f);
        
        Hand.sprite = Assets.HandHoldingBody.LoadAsset();
        Hand.material = new Material(Shader.Find("Unlit/PlayerShader"));
        PlayerMaterial.SetColors(Player.cosmetics.ColorId, Hand);
        
        Player.MyPhysics.Speed /= 2;

        if (Player.AmOwner)
        {
            HudManager.Instance.KillButton.Hide();
            HudManager.Instance.ReportButton.Hide();
            HudManager.Instance.ImpostorVentButton.Hide();
            CustomButtonSingleton<ThrowBody>.Instance.Button.Show();
            CustomButtonSingleton<Carry>.Instance.Button.Hide();
        }
    }

    public override void OnDeactivate()
    {
        if (Player.AmOwner && MeetingHud.Instance == null)
        {
            HudManager.Instance.KillButton.Show();
            HudManager.Instance.ImpostorVentButton.Show();
            HudManager.Instance.ReportButton.Show();
            CustomButtonSingleton<ThrowBody>.Instance.Button.Hide();
            var carry = CustomButtonSingleton<Carry>.Instance;
            carry.Button.Show();
        }
        Player.MyPhysics.Speed *= 2;
        
        if (Body) Body.transform.SetParent(null);
        Hand.gameObject.Destroy();
        
        if (Player.MyPhysics.Velocity == Vector2.zero)
        {
            Body.StartCoroutine(Effects.Sequence([
                Effects.Slide2DWorld(Body.transform, Body.transform.position, Player.transform.position, 0.15f),
                Effects.Bounce(Body.transform, 0.2f, 0.25f)
            ]));
        }
        else
        {
            Body.gameObject.AddComponent<ThrowableObject>()
                .Initialize(Player.GetTruePosition() + (Player.MyPhysics.body.velocity * 2));
        }
    }
    
    public override void OnDeath(DeathReason reason)
    {
        ModifierComponent.RemoveModifier(this);
    }

    public override void OnMeetingStart()
    {
        ModifierComponent.RemoveModifier(this);
    }
}