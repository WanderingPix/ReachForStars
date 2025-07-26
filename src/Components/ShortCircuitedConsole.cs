using System.Linq;
using MiraAPI.Modifiers;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace ReachForStars.Roles.Impostors.Electroman;

public class ShortCircuitedConsole : MonoBehaviour
{
    public ElectromanRole electroman;
    private Console console;

    public void Start()
    {
        console = gameObject.GetComponent<Console>();
    }

    public void Use()
    {
        var electrocuted = PlayerControl.LocalPlayer.AddModifier<ElectrocutedModifier>();
        electrocuted.Electroman = electroman;
        electrocuted.Task = ShipStatus.Instance.GetAllTasks().First(x => x.ValidConsole(console));
        this.DestroyImmediate();
    }
}