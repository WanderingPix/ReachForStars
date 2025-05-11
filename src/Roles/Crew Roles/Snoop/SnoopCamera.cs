using MiraAPI.Utilities.Assets;
using MiraAPI.Utilities;
using Reactor.Utilities.Extensions;
using Reactor.Utilities;
using System.Collections;
using System;
using Reactor.Utilities.Attributes;
using UnityEngine;
using Sentry.Unity.NativeUtils;
using ReachForStars.Networking;
using System.Linq;

namespace ReachForStars.Roles.Crewmates.Snoop;

[RegisterInIl2Cpp(typeof(SurvCamera))]
public class SnoopCamera(IntPtr ptr) : MonoBehaviour(ptr)
{
    SpriteRenderer myRend;
    void Start()
    {
        //Setup
        myRend = gameObject.AddComponent<SpriteRenderer>();
        myRend.sprite = Assets.SnoopCamOff.LoadAsset();
    }
    void SetAnimation(bool isActive)
    {
        if (isActive)
        {
            myRend.sprite = Assets.SnoopCamOn.LoadAsset();
        }
        else
        {
            myRend.sprite = Assets.SnoopCamOff.LoadAsset();
        }
    }
}
