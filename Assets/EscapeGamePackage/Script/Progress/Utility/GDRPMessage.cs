using GoogleMobileAds.Api;
using GoogleMobileAds.Ump.Api;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api.Mediation.UnityAds;
using GoogleMobileAds.Api.Mediation.AppLovin;

public class GDRPMessage : SingletonMonoBehaviour<GDRPMessage>
{
    public Master master;

    public void StartGdpr()
    {
        if (master.gameMode == Master.GameMode.Develop) Test();
        else Prodact();
    }

    public void Test()
    {
        var debugSettings = new ConsentDebugSettings
        {
            DebugGeography = DebugGeography.EEA,
            TestDeviceHashedIds = new List<string>
            {
                "E7C4863510534D5BA750AED23F033A0A",
                "435A45E8B67B1349B2AF1267C7342739",
                "7B7431B7CCC19E650C2224D044296A12",
                "8067C79A-643D-4B4C-803A-4D34B7BFA5F5"
            }
        };

        ConsentRequestParameters request = new ConsentRequestParameters
        {
            TagForUnderAgeOfConsent = false,
            ConsentDebugSettings = debugSettings
        };

        ConsentInformation.Update(request, OnConsentInfoUpdated);
    }

    public void Prodact()
    {
        ConsentRequestParameters request = new ConsentRequestParameters
        {
            TagForUnderAgeOfConsent = false,
        };

        ConsentInformation.Update(request, OnConsentInfoUpdated);
    }

    void OnConsentInfoUpdated(FormError consentError)
    {
        if (consentError != null)
        {
            Debug.LogError(consentError);
            return;
        }

        // ★フォームが必要なら表示し、終わってから次へ
        ConsentForm.LoadAndShowConsentFormIfRequired((FormError formError) =>
        {
            if (formError != null)
            {
                Debug.LogError(formError);
                return;
            }

            // ★ここで「広告リクエストして良い状態」かチェック
            if (ConsentInformation.CanRequestAds())
            {
                bool gdprConsent = (ConsentInformation.ConsentStatus == ConsentStatus.Obtained);

                // Unity Ads / AppLovin に同意を渡す（必要なら）
                UnityAds.SetConsentMetaData("gdpr.consent", gdprConsent);
                AppLovin.SetHasUserConsent(gdprConsent);

                MobileAds.Initialize((InitializationStatus initstatus) =>
                {
                    // TODO: Request an ad.
                });
            }
        });
    }
}