using System;
using MiraAPI.Utilities;
using ReachForStars.Networking;
using ReachForStars.Utilities;
using Reactor.Utilities.Attributes;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Chiller;

[RegisterInIl2Cpp(typeof(IUsable))]
public class FrozenBody(IntPtr ptr) : MonoBehaviour(ptr)
{
    public bool isActive;
    public BoxCollider2D myCol;
    public SpriteRenderer myRend;
    public DeadBody myBody;
    public int Level = 3;
    public int Durability = 10;
    public byte id;
    public ImageNames UseIcon => ImageNames.FreeplayButton;
    public float UsableDistance => 50f;
    public float PercentCool => 0f;

    public void Start()
    {
        //Spawn Animation
        HudManager.Instance.StartCoroutine(Effects.Bounce(gameObject.transform, 0.7f, 0.45f));
        HudManager.Instance.StartCoroutine(Effects.ColorFade(myRend, new Color(1f, 1f, 1f, 0f),
            new Color(1f, 1f, 1f, 1f), 0.4f));
        HudManager.Instance.StartCoroutine(Effects.ScaleIn(gameObject.transform, 0f, 0.4f, 0.4f));


        myRend = gameObject.GetComponent<SpriteRenderer>();
        myCol = gameObject.GetComponent<BoxCollider2D>();

        myRend.SetMaterial(ShipStatus.Instance.EmergencyButton.GetComponent<SpriteRenderer>().material);
    }

    public void OnDestroy()
    {
        myBody.gameObject.SetActive(true);
    }

    public void SetTargetBody(DeadBody body)
    {
        myBody = body;
        id = body.ParentId;
        myBody.gameObject.SetActive(false);
    }

    public void Use()
    {
        Durability--;
        HudManager.Instance.StartCoroutine(Effects.SwayX(gameObject.transform, 0.7f, 0.2f));
        SoundManager.Instance.PlaySoundAtLocation(Assets.FrozenBodyImpactSfx.LoadAsset(), gameObject.transform.position,
            PlayerControl.LocalPlayer.GetTruePosition(), SoundManager.Instance.SfxChannel);
        if (Durability == 0)
        {
            PlayerControl.LocalPlayer.RpcDamageFrozenBody(id);
            Durability = 10;
        }
    }

    public void Damage()
    {
        Level--;
        var newSize = gameObject.transform.localScale.x * new Vector3(0.8f, 0.8f, 0.8f);
        HudManager.Instance.StartCoroutine(Effects.Bounce(gameObject.transform, 0.4f, 0.6f));
        HudManager.Instance.StartCoroutine(Effects.ScaleIn(gameObject.transform, gameObject.transform.localScale.x,
            newSize.x, 0.4f));
        if (Level == 0)
        {
            SoundManager.Instance.PlaySoundAtLocation(Assets.FrozenBodyBreakSfx.LoadAsset(),
                gameObject.transform.position, PlayerControl.LocalPlayer.GetTruePosition(),
                SoundManager.Instance.SfxChannel);
            gameObject.DestroyImmediate();
        }
    }

    public void SetOutline(bool on, bool mainTarget)
    {
        if (on) myRend.UpdateOutline(Palette.CrewmateRoleHeaderBlue);
        else myRend.UpdateOutline(new Color(0f, 0f, 0f, 0f));
    }

    public float CanUse(NetworkedPlayerInfo pc, out bool canUse, out bool couldUse)
    {
        var num = float.MaxValue;
        var @object = pc.Object;
        couldUse = !pc.IsDead;
        canUse = couldUse;
        if (canUse)
        {
            var truePosition = @object.GetTruePosition();
            var position = transform.position;
            num = Vector2.Distance(truePosition, position);
            canUse &= num <= UsableDistance;
        }

        return num;
    }
}