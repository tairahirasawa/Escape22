using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
using System.Threading.Tasks;
using UnityEngine.UI;

public class GoogleAd_rewardSubEvent : MonoBehaviour
{
    public RewardedAd rewardedAd;

    public float time;
    public bool CanReward;
    public bool IsPresentReward;
    public Action rewardAct;

    //public bool disconnectedTest;

    void Start()
    {

        if (DataPersistenceManager.I.gameData.blockAd) return;

        //adRewardButtons = GameObject.FindObjectsOfType<AdRewardButton>(true);

        InitializeAD();

    }

    public void InitializeAD()
    {
        NetworkReachability networkReachability = Application.internetReachability;
        if (networkReachability == NetworkReachability.NotReachable) return;

        LoadRewardedAd();
    }


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            InitializeAD();
        }


        if (DataPersistenceManager.I.gameData.blockAd)
        {
            if (this.rewardedAd != null) this.rewardedAd.Destroy();

            this.enabled = false;
        }

        NetworkReachability networkReachability = Application.internetReachability;


        Reward();
    }

    public void LoadRewardedAd()
    {
        string _adUnitId = GoogleAdManager.I.master.googleAdSettings.GetUnitID(GoogleAdManager.I.master.gameMode, GoogleAdSettings.AdType.RewardSubEvent);

        Debug.Log("sub  "+_adUnitId);

        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("game");

        // send the request to load the ad.
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;
                RegisterEventHandlers(rewardedAd);
            });
    }

    public void ShowRewardedAd(Action rewardAction)
    {
        IsPresentReward = true;
        GoogleAdManager.I.IsEndAd = true;
        if(AudioDirector.I != null) AudioDirector.I.forceMute();
        if(TitleBGMDirector.I != null) TitleBGMDirector.I.forceMute();

        rewardAct = null;
        rewardAct += rewardAction;

        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                CanReward = true;
                Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
            });
        }
        else
        {
            Debug.Log("���s");
            CautionScr.I.PresentCautionScrLocalizeForReward("failReward");
            if (AudioDirector.I != null) AudioDirector.I.forceMuteOff();
            if (TitleBGMDirector.I != null) TitleBGMDirector.I.DisableMute();
            IsPresentReward = false;
        }
    }


    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");
            LoadRewardedAd();
            if (AudioDirector.I != null) AudioDirector.I.forceMuteOff();
            if (TitleBGMDirector.I != null) TitleBGMDirector.I.DisableMute();
            StartCoroutine(GoogleAdManager.I.EndAd());
            IsPresentReward = false;
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);

            LoadRewardedAd();
        };
    }

    private void Reward()
    {
        if (!CanReward)
        {
            return;
        }

        if (IsPresentReward)
        {
            return;
        }

        if (rewardAct != null)
        {
            rewardAct?.Invoke();
            rewardAct = null;
        }

        CanReward = false;
    }

}
