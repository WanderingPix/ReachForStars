using HarmonyLib;
using UnityEngine;

namespace ReachForStars.RanksSystem
{
    [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.OnPlayerJoined))]
    public class Ranks
    {
        public static void Postfix()
        {
            var Lavender = new Color(0.69f, 0.3f, 0.95f, 1f);
            var Blue = new Color(0f, 0.8f, 0f, 1f);
            string rank = "";
                if (PlayerControl.LocalPlayer.FriendCode ==  "shinyrake#9382")
                {
                    rank = "Owner";
                }
                if (PlayerControl.LocalPlayer.FriendCode ==  "tankermine#7020")
                {
                    rank = "Plagueboric";
                }
                string FinalName = "<size=2>" + "<color=#" + ColorUtility.ToHtmlStringRGB(PlayerControl.LocalPlayer.Data.Color) + ">" + rank + "\n" + PlayerControl.LocalPlayer.Data.PlayerName + "\n ";
                if (PlayerControl.LocalPlayer.Data.PlayerName != FinalName)
                {
                    PlayerControl.LocalPlayer.RpcSetName(FinalName);
                }
        }
    }
}
