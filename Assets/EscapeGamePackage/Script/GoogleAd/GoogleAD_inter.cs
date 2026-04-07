using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
using UnityEngine.SceneManagement;

public class GoogleAD_inter : MonoBehaviour
{
    //public Master master;
    public InterstitialAd interstitialAd;

    public bool IsPlayAd;

    string LoadSceaneName;

    public void Start()
    {

        if (DataPersistenceManager.I.gameData.blockAd) return;
        InitializeAD();
    }

    public void InitializeAD()
    {
        NetworkReachability networkReachability = Application.internetReachability;
        if (networkReachability == NetworkReachability.NotReachable) return;

        LoadInterstitialAd();
    }

    private void Update()
    {
        if (DataPersistenceManager.I.gameData.blockAd)
        {
            if(this.interstitialAd != null) this.interstitialAd.Destroy();
            this.enabled = false;
        }

    }

    public void LoadInterstitialAd()
    {
        string _adUnitId = GoogleAdManager.I.master.googleAdSettings.GetUnitID(GoogleAdManager.I.master.gameMode, GoogleAdSettings.AdType.InterStatial);

        // Clean up the old ad before loading a new one.
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("game");

        // send the request to load the ad.
        InterstitialAd.Load(_adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    IsPlayAd = false;
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interstitialAd = ad;
                RegisterEventHandlers(interstitialAd);
            });
    }

    private void RegisterEventHandlers(InterstitialAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");

            if (AudioDirector.I) AudioDirector.I.forceMuteOff();
            StartCoroutine(GoogleAdManager.I.EndAd());
            IsPlayAd = false;
            GoogleAdManager.I.RestartAdTimer();

            LoadInterstitialAd();

            if (LoadSceaneName != null && LoadSceaneName != "")
            {
                var NextSceane = LoadSceaneName;
                LoadSceaneName = null;

                //LoadingScr.I.PresentLoadingScr();
                SceneManager.LoadScene(NextSceane);
            }
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);

            LoadInterstitialAd();

        };
    }

    public async void ShowAd(int milliseconds = 0)
    {
        if (this.interstitialAd.CanShowAd() && !DataPersistenceManager.I.gameData.blockAd)
        {
            IsPlayAd = true;
            GoogleAdManager.I.IsEndAd = true;
            GoogleAdManager.I.StopAdTimer();

            await Task.Delay(milliseconds);

            if(AudioDirector.I) AudioDirector.I.forceMute();
            
            this.interstitialAd.Show();
        }
    }

    public void ShowAdAndLoadScene(string sceneName)
    {
        if (this.interstitialAd.CanShowAd() && !DataPersistenceManager.I.gameData.blockAd)
        {
            IsPlayAd = true;

            if (AudioDirector.I) AudioDirector.I.forceMute();
            //if (TitleBGMDirector.I) TitleBGMDirector.I.forceMute();
            LoadSceaneName = sceneName;
            this.interstitialAd.Show();
        }
        else
        {
            Debug.Log("移動");
            //LoadingScr.I.PresentLoadingScr();
            SceneManager.LoadScene(sceneName);
        }
    }

    public async Task WaitForAdToFinish()
    {
        // IsPlayAdがfalseになるまでループします。
        while (IsPlayAd)
        {
            // 次のフレームまで待機します。
            await Task.Delay(100); // ここでは100ミリ秒待つことにしますが、任意の値に調整可能です。
        }
    }


    public void DestoryAd()
    {
        interstitialAd.Destroy();
    }
}
