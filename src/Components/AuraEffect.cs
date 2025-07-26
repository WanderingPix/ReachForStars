using UnityEngine;

namespace ReachForStars.Components;

public class AuraEffect : MonoBehaviour
{
    public SpriteRenderer _renderer;
    public PlayerControl Player;

    public void Start()
    {
        Player.StartCoroutine(Effects.CycleColors(_renderer, Color.white, Color.clear, 1f,
            99999999f)); //POV: Too lazy to make a coroutine which does this forever
    }
}