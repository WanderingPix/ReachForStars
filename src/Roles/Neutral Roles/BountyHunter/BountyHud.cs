using System.Collections.Generic;
using System.Linq;
using MiraAPI.Roles;
using MiraAPI.Utilities;
using TMPro;
using UnityEngine;

namespace ReachForStars.Roles.Neutrals.BountyHunter;

public class BountyHud : MonoBehaviour
{
    public TextMeshPro Label;
    public HideAndSeekDeathPopupNameplate Nameplate;
    SpriteRenderer rend;
    public int Cur;
    int Max;
    void Start()
    {
        Label = Helpers.CreateTextLabel("BountyLabel", gameObject.transform, AspectPosition.EdgeAlignments.Top, Vector3.zero, 2, TextAlignmentOptions.Center);
        Label.text = $"{Cur}/{Max}";

        Nameplate = Object.Instantiate<HideAndSeekDeathPopupNameplate>(GameManagerCreator.Instance.HideAndSeekManagerPrefab.DeathPopupPrefab.nameplate, gameObject.transform);
        Nameplate.transform.position = Label.transform.position - new Vector3(0f, 1f, 0f);
    }
    public void SetTarget(PlayerControl target)
    {
        Nameplate.SetPlayer(target.Data);
    }
    public void SetDead()
    {
        HudManager.Instance.StartCoroutine(Effects.ScaleIn(gameObject.transform, 2f, 1f, 0.7f));
        foreach (var rend in Nameplate.GetComponentsInChildren<SpriteRenderer>())
        {
            rend.color = rend.color.DarkenColor(0.45f);
        }
    }
}