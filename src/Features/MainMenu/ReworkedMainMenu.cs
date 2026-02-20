using MiraAPI.Utilities;
using UnityEngine;

namespace ReachForStars.Features.MainMenu;

public static class ReworkedMainMenu
{
    public static void SetUp(MainMenuManager menu)
    {
        menu.quitButton.activeSprites.GetComponent<SpriteRenderer>().color = Color.red.LightenColor();
        menu.quitButton.inactiveSprites.GetComponent<SpriteRenderer>().color = Color.red.DarkenColor();

        var starfield = GameObject.Find("starfield").GetComponent<StarGen>();
        starfield.SetDirection(new(0, 5));
        starfield.RegenPositions();
        
        menu.fullScreenSprite.enabled = false;
        var leftPanel = menu.transform.FindChild("LeftPanel");
        leftPanel.GetComponent<AspectPosition>().enabled = false;
        leftPanel.transform.localPosition = new(0, -1, -1);
        leftPanel.transform.localScale = new(0.8f, 0.8f, 0.8f);
    }
}