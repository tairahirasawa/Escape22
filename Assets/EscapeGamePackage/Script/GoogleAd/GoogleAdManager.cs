using GoogleMobileAds.Api;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoogleAdManager : SingletonMonoBehaviour<GoogleAdManager>
{
    public GoogleAD_banar googleAD_Banar;
    public GoogleAD_inter googleAD_Inter;
    public GoogleAD_rewerd googleAD_Rewerd;
    public GoogleAd_rewardSubEvent googleAD_RewardSubEvent;
    public GoogleAD_LoadingScene googleAD_LoadingScene;
    public GoogleAD_AppOpenAd googleAD_AppOpenAd;

    public Master master;

    private float ADtimer = 120;
    private float BannerTime = 5;
    public float time;

    public bool CanShowTimerAd;
    public bool DisabelAdTimer;

    public bool IsEndAd;

    public bool IsAlreadyLaunch;

    private static bool hasInitialized = false;

    //private NetworkReachability previousReachability;
    protected override void OnAwake()
    {
        base.OnAwake();

        if (!hasInitialized)
        {
            MobileAds.Initialize(initStatus =>
            {
                Debug.Log("Google Mobile Ads initialized.");
            });

            hasInitialized = true; // 初期化完了フラグを設定
        }
    }

    protected override void OnStart()
    {
        base.OnStart();
        // 初期のネットワーク接続状態を保存する
       // previousReachability = Application.internetReachability;
    }


    private void Update()
    {
        if (!DisabelAdTimer)
        {
            time += Time.deltaTime;

            if (time >= ADtimer)
            {
                CanShowTimerAd = true;
            }

            if(time > BannerTime && googleAD_Banar._bannerView == null && googleAD_Banar.enabled)
            {
                googleAD_Banar.CreateBannerView();
                googleAD_Banar.LoadAd();
            }

        }
    }

    public void StopAdTimer()
    {
        time = 0;
        DisabelAdTimer = true;
        CanShowTimerAd = false;
    }

    public void RestartAdTimer()
    {
        time = 0;
        DisabelAdTimer = false;
    }

    
    public void OnApplicationFocus(bool focus)
    {
        
        if (SceneManager.GetActiveScene().name == "Title") return;

        if (focus && !IsEndAd && IsAlreadyLaunch)
        {
            googleAD_Banar.DestroyAd();
            googleAD_AppOpenAd.ShowAppOpenAd();
        }

        IsAlreadyLaunch = true;
    }
    
    public void ShowTimerAd()
    {
        if(CanShowTimerAd)
        {
            googleAD_Inter.ShowAd();

        }
    }

    public IEnumerator EndAd()
    {
        yield return new WaitForSeconds(3f);
        IsEndAd = false;
    }

}
