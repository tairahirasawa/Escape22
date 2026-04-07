using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManager : SingletonMonoBehaviour<AdManager>
{
    public GameObject googleAdManager;
    public GameObject crazyAdManager;

    public Action rewardAct;

    protected override void OnAwake()
    {
#if UNITY_WEBGL
        crazyAdManager.SetActive(true);
        googleAdManager.SetActive(false);
#else
        crazyAdManager.SetActive(false);
        googleAdManager.SetActive(true);

# endif
    }

    public void ShowRewardAd(Action successAct)
    {

#if UNITY_WEBGL
        CrazyAdManager.I.ShowRewardedAd(successAct);
#else
        GoogleAdManager.I.googleAD_Rewerd.ShowRewardedAd(successAct);

# endif

    }
}
