using System.Linq;
using MiraAPI.Events;
using MiraAPI.Events.Vanilla.Meeting;
using MiraAPI.Utilities;
using ReachForStars.Roles.Impostors.Traitor;
using Reactor.Utilities.Extensions;
using TMPro;
using UnityEngine;

namespace ReachForStars.Components;

public class TraitorsRemainingTextController : MonoBehaviour
{
    public static TraitorsRemainingTextController Instance;
    public TextMeshPro text;

    public void Start()
    {
        Instance = this;
        text = GetComponent<TextMeshPro>();
        UpdateText();
    }

    public void OnTraitorDied()
    {
        HudManager.Instance.StartCoroutine(Effects.ColorFade(text, Color.red, Color.white, 0.7f));
        HudManager.Instance.StartCoroutine(Effects.ScaleIn(transform, 1.7f, 0.7f, 0.7f));
        UpdateText();
    }

    public void UpdateText()
    {
        var c = GetAliveTraitorsCount();
        if (c == 1) text.text = $"{c} Traitor Alive"; //Singular
        else if (c > 1) text.text = $"{c} Traitors Alive"; //Plural

        if (c == 0) text.text = "All Traitors Dead";
    }

    public int GetAliveTraitorsCount()
    {
        return Helpers.GetAlivePlayers().Where(x => x.Data.Role is TraitorRole).Count();
    }

    [RegisterEvent]
    public static void OnMeetingCalled(StartMeetingEvent e)
    {
        if (Instance && Instance.GetAliveTraitorsCount() == 0) Instance.gameObject.Destroy();
    }
}