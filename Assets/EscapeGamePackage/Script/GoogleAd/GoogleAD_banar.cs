using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
using TMPro;

public class GoogleAD_banar : MonoBehaviour
{
    //public Master master;
    public BannerView _bannerView;

    //public TextMeshProUGUI debug;

    //string _adUnitId;

    public void Start()
    {
        if (DataPersistenceManager.I.gameData.blockAd) return;
        InitializeAD();
    }


    public void InitializeAD()
    {
        NetworkReachability networkReachability = Application.internetReachability;
        if (networkReachability == NetworkReachability.NotReachable) return;


        //CreateBannerView();
        //LoadAd();
    }

    private void Update()
    {
        
        if(DataPersistenceManager.I.gameData.blockAd)
        {
            if (this._bannerView != null) this._bannerView.Destroy();
            this.enabled = false;
        }
    }

    public void CreateBannerView()
    {
        Debug.Log("Creating banner view");
        //debug.text = "Creating banner view";

        string _adUnitId = GoogleAdManager.I.master.googleAdSettings.GetUnitID(GoogleAdManager.I.master.gameMode, GoogleAdSettings.AdType.Banner);

        // If we already have a banner, destroy the old one.
        if (_bannerView != null)
        {
            DestroyAd();
        }

        // Create a 320x50 banner at top of the screen
        _bannerView = new BannerView(_adUnitId, AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth), AdPosition.Bottom);

        //_bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Create an extra parameter that aligns the bottom of the expanded ad to the
        // bottom of the bannerView.
        
    }

    public void LoadAd()
    {
        // create an instance of a banner view first.
        
        if (_bannerView == null)
        {
            CreateBannerView();
        }
        
        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("game");

        //adRequest.Extras.Add("collapsible", "bottom");

        // send the request to load the ad.
        Debug.Log("Loading banner ad.");
        //debug.text = "Loading banner ad.";

        _bannerView.LoadAd(adRequest);
        ListenToAdEvents();
    }

    private void ListenToAdEvents()
    {
        // Raised when an ad is loaded into the banner view.
        _bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad with response : "
                + _bannerView.GetResponseInfo());
        };
        // Raised when an ad fails to load into the banner view.
        _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : "
                + error);

            //debug.text = "Banner view failed to load an ad with error :";

            //LoadAd();
        };
        // Raised when the ad is estimated to have earned money.
        _bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Banner view paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        _bannerView.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        _bannerView.OnAdClicked += () =>
        {
            Debug.Log("Banner view was clicked.");
        };
        // Raised when an ad opened full screen content.
        _bannerView.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        _bannerView.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view full screen content closed.");
        };
    }

    public void DestroyAd()
    {
        if (_bannerView != null)
        {
            Debug.Log("Destroying banner ad.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }

}
