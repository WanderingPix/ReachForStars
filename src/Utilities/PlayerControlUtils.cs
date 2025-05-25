using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using MiraAPI.Utilities;
using HarmonyLib;
using ReachForStars;
using UnityEngine;
using Hazel;
using Reactor.Networking.Attributes;
using Reactor.Networking.Rpc;
using ReachForStars.Networking;
using Reactor.Utilities;
using System.Collections;
using MiraAPI.Utilities;
using MiraAPI.Networking;
using TMPro;
using System.Threading;
using System.Threading.Tasks;
using MiraAPI.Utilities.Assets;
using System.Collections.Generic;
using Rewired;
using System.Linq;

namespace ReachForStars.Utilities;

public static class PlayerControlUtils
{
    public static void Resize(this PlayerControl player, Vector3 size)
    {
        player.transform.localScale = size;
    }

    public static IEnumerator Respawn(this PlayerControl player, float time)
    {
        // Implementation for respawn
        yield break;
    }

    public static void MoveCamera(this PlayerControl player, Vector3 endPosition, float duration)
    {
        Coroutines.Start(MoveCameraCoroutine(player.GetTruePosition(), endPosition, duration));
        player.MyPhysics.CoSpawnPlayer(LobbyBehaviour.Instance);
        player.transform.position = endPosition;
    }

    private static IEnumerator MoveCameraCoroutine(Vector3 startPosition, Vector3 endPosition, float duration)
    {
        while (Camera.main.orthographicSize > 4f)
        {
            Camera.main.orthographicSize += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            Camera.main.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.position = endPosition;
        while (Camera.main.orthographicSize > 3.2f)
        {
            Camera.main.orthographicSize -= 0.2f;
            yield return new WaitForSeconds(0.1f);
        }

        yield break;
    }
    public static PlayerControl GetPlayerById(byte id)
    {
        return PlayerControl.AllPlayerControls.ToArray().ToList().FirstOrDefault(x => x.PlayerId == id);
    }
}
