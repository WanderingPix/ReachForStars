using UnityEngine;

namespace ReachForStars.Features.Freeplay;

public class FreeplayOptionsLaptop
{
    public static void Create()
    {
        var pref = GameStartManager.Instance.LobbyPrefab.transform.FindChild("SmallBox").gameObject;
        
        var box = UnityObject.Instantiate(pref);
        box.GetComponent<SpriteRenderer>().color = Color.magenta;
        box.transform.position = PlayerControl.LocalPlayer.transform.position + Vector3.up;
        var laptop = box.transform.GetChild(0).GetComponent<OptionsConsole>();
        laptop.Outline.color = Color.magenta;
    }
}