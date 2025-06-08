using MiraAPI.GameOptions;
using TMPro;
using UnityEngine;

namespace ReachForStars.Roles.Neutrals.BountyHunter
{
    public class BountyHud : MonoBehaviour
    {
        public SpriteRenderer myRend;
        public TextMeshPro Counter;
        public AspectPosition myPos;
        public void Start()
        {
            Counter = gameObject.transform.GetChild(2).GetComponent<TextMeshPro>();
            myRend = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
            myPos = gameObject.AddComponent<AspectPosition>();
            myPos.Alignment = AspectPosition.EdgeAlignments.Top;
            myPos.DistanceFromEdge = new Vector3(0f, 0.5f, 0f);
            myPos.AdjustPosition();
        }
        public void UpdateCount(int count)
        {
            Counter.text = "";
            int i;
            for (i = 0; i == count; i++) //Killed Bounties
            {
                Counter.text += "<sprite=5>";
            }
            for (int r = 0; r == OptionGroupSingleton<BountyHunterOptions>.Instance.SuccessfulKillsQuota - count; r++) //remaining bounties
            {
                Counter.text += "<sprite=6>";
            }
        }
    }
}