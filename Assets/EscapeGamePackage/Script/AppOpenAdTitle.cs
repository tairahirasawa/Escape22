using System;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Common;
using UnityEngine.Timeline;
using TMPro;
using System.Collections;
using Cysharp.Threading.Tasks;

public class AppOpenAdTitle : SingletonMonoBehaviour<AppOpenAdTitle>
{
    public AppOpenAd appOpenAd { get; private set; }
    public Master master;
    public bool IsPresent;


    protected override void OnStart()
    {
        if (DataPersistenceManager.I.gameData.blockAd) return;
        InitializeAD();
    }

    public void InitializeAD()
    {
        NetworkReachability networkReachability = Application.internetReachability;
        if (networkReachability == NetworkReachability.NotReachable) return;

        LoadAppOpenAd();

        //await UniTask.WaitUntil(() => LoadFirst = true);
    }

    private void OnDestroy()
    {
        // Always unlisten to events when complete.
        //AppStateEventNotifier.AppStateChanged -= OnAppStateChanged;
    }

    /// <summary>
    /// Loads the app open ad.
    /// </summary>
    public void LoadAppOpenAd()
    {
        string _adUnitId = master.googleAdSettings.GetUnitID(master.gameMode, GoogleAdSettings.AdType.AppOpenAd);
        Debug.Log(_adUnitId);

        // Clean up the old ad before loading a new one.
        if (appOpenAd != null)
        {
            appOpenAd.Destroy();
            appOpenAd = null;
        }

        //debugtext.text = "Loading the app open ad first";
        Debug.Log("Loading the app open ad first.");

        // Create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        AppOpenAd.Load(_adUnitId, adRequest,
            (AppOpenAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    //debugtext.text = "Loading the app open ad " + error;
                    Debug.LogError("app open ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                //debugtext.text = "App open ad loaded with response ";
                Debug.Log("App open ad loaded with response : "
                          + ad.GetResponseInfo());

                appOpenAd = ad;
                RegisterEventHandlers(ad);
            });

    }

    private void RegisterEventHandlers(AppOpenAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("App open ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("App open ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("App open ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("App open ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("App open ad full screen content closed.");
            LoadAppOpenAd();

            if (TitleBGMDirector.I)
            {
                TitleBGMDirector.I.DisableMute();
            }

            IsPresent = false;
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("App open ad failed to open full screen content " +
                           "with error : " + error);
            LoadAppOpenAd();

            if (TitleBGMDirector.I)
            {
                TitleBGMDirector.I.DisableMute();
            }

            IsPresent = false;
        };
    }

    /// <summary>
    /// Shows the app open ad.
    /// </summary>
    public void ShowAppOpenAd()
    {
        if (DataPersistenceManager.I.gameData.blockAd) return;

        if (appOpenAd != null && appOpenAd.CanShowAd())
        {
            IsPresent = true;
            if (TitleBGMDirector.I) TitleBGMDirector.I.forceMute();
            appOpenAd.Show();
        }
        else
        {
            Debug.LogError("App open ad is not ready yet.");
        }
    }
}
