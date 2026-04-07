using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GoogleAdSettings
{
    [Header("バナー")]
    public AdIDs Banner;

    [Header("インターステーシャル")]
    public AdIDs InterStatial;

    [Header("リワード")]
    public AdIDs Reward;

    [Header("起動時")]
    public AdIDs AppOpenAd;

    [Header("リワード サブイベント")]
    public AdIDs RewardSubEvent;

    [Header("ローディングシーン")]
    public AdIDs LoadingScene;

    public string GetUnitID(Master.GameMode gameMode, AdType type)
    {
        switch (type)
        {
            case AdType.Banner:
                return GetID(gameMode, GetDebies(Banner), type);

            case AdType.InterStatial:
                return GetID(gameMode, GetDebies(InterStatial), type);

            case AdType.Reward:
                return GetID(gameMode, GetDebies(Reward), type);

            case AdType.AppOpenAd:
                return GetID(gameMode, GetDebies(AppOpenAd), type);

            case AdType.RewardSubEvent:
                return GetID(gameMode, GetDebies(RewardSubEvent), type);

            case AdType.LoadingScene:
                return GetID(gameMode, GetDebies(LoadingScene), type);

            default:
                return null;
        }
    }

    private string GetID(Master.GameMode gameMode, UnitIDs unitIDs, AdType type)
    {
        if (gameMode == Master.GameMode.Prodact)
        {
            return unitIDs.unitID;
        }
        else
        {
            return GetSampleID(type);
        }
    }

    private UnitIDs GetDebies(AdIDs id)
    {
#if UNITY_ANDROID
        return id.Android;

#elif UNITY_IOS
                return id.ios;
#else
                return id.unexpected;
#endif
    }

    private string GetSampleID(AdType type)
    {
        switch (type)
        {
#if UNITY_ANDROID

            case AdType.Banner:
                return "ca-app-pub-3940256099942544/6300978111";

            case AdType.InterStatial:
                return "ca-app-pub-3940256099942544/1033173712";

            case AdType.Reward:
                return "ca-app-pub-3940256099942544/5224354917";

            case AdType.AppOpenAd:
                return "ca-app-pub-3940256099942544/9257395921";

            case AdType.RewardSubEvent:
                return "ca-app-pub-3940256099942544/5224354917";

            case AdType.LoadingScene:
                return "ca-app-pub-3940256099942544/1033173712";

            default:
                return "";

#elif UNITY_IOS

                    case AdType.Banner:
                        return "ca-app-pub-3940256099942544/2934735716";

                    case AdType.InterStatial:
                        return "ca-app-pub-3940256099942544/4411468910";

                    case AdType.Reward:
                        return "ca-app-pub-3940256099942544/1712485313";

                    case AdType.AppOpenAd:
                        return "ca-app-pub-3940256099942544/5575463023";
                    
                    case AdType.RewardSubEvent:
                        return "ca-app-pub-3940256099942544/1712485313";

                    case AdType.LoadingScene:
                        return "ca-app-pub-3940256099942544/4411468910";

                    default:
                        return"";
#else
                    default:
                        return"";
#endif

        }
    }

    public enum AdType
    {
        Banner, InterStatial, Reward, AppOpenAd,RewardSubEvent,LoadingScene
    }

}

[Serializable]
public class AdIDs
{
    public UnitIDs Android;
    public UnitIDs ios;
    [HideInInspector] public UnitIDs unexpected;
}

[Serializable]
public class UnitIDs
{
    public string unitID;
    //public string smapleID;
}