using TMPro;
using UnityEngine;

namespace ReachForStars.Roles.Neutrals.BountyHunter
{
    public class BountyHud : MonoBehaviour
    {
        public SpriteRenderer myRend;
        public TextMeshPro Counter;
        public void Start()
        {
            Counter = gameObject.transform.GetChild(2).GetComponent<TextMeshPro>();
            myRend = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
        public void UpdateCount(int count)
        {
            
        }
    }
}