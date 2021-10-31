using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdbInit : MonoBehaviour
{
    void Awake()
    {
        MobileAds.Initialize(initStatus => { });
    }
}
