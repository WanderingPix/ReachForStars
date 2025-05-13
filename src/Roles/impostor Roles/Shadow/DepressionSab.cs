using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using ReachForStars.Networking;
using UnityEngine;
using MiraAPI.Utilities;
using MiraAPI.Networking;
using System.Collections.Generic;
using UnityEngine.UI;
using ReachForStars.Utilities;
using Reactor.Utilities;
using ReachForStars.Translation;
using Reactor.Utilities.Attributes;
using IEnumerator = System.Collections.IEnumerator;

namespace ReachForStars.Roles.Impostors.Shadow;
//[RegisterInIl2Cpp(typeof(SabotageSystemType))] I don't even have to make it a proper sabo
public class DepressionSab
{
    public static bool isActive;
    public static void Init(PlayerControl pc)
    {
        isActive = true;
        Coroutines.Start(DepressionSabCoroutine(pc));
    }
    public static IEnumerator DepressionSabCoroutine(PlayerControl shadow)
    {
        
        foreach (SpriteRenderer rend in Object.FindObjectsOfType<SpriteRenderer>())
        {
            Color startingColor = rend.color;
            // Convert to HSV, modify the value (brightness), and convert back to RGB
            Color.RGBToHSV(startingColor, out float h, out float s, out float v);
            v *= 0.5f; // Reduce brightness by 50%
            s *= 0.5f; // Reduce saturation by 50%

            Color targetColor = Color.HSVToRGB(h, s, v);

            HudManager.Instance.StartCoroutine(Effects.ColorFade(rend, startingColor, targetColor, 3f));

            yield return new WaitForSeconds(3f);

            HudManager.Instance.StartCoroutine(Effects.ColorFade(rend, targetColor, startingColor, 1f));
        }
        yield break;
    }
}