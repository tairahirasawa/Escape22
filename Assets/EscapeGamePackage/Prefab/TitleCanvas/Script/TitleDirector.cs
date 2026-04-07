using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using I2.Loc;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Assertions.Must;

public partial class GameData
{
    public bool IsAtt;
}

public class TitleDirector : MonoBehaviour
{
    public Button newGameBtn, loadGameBtn, anotherGameBtn,twitterBtn;
    public TitleBGMDirector titleBGMDirector;
    public CanvasGroup cautionScr;
    
    public GameObject TennpaLogo;
    public GameObject AttScr;

    public static bool Islogoed;
    public static bool IsLoadAd;

    public GameObject ShortStoryScreen, LongStoryScreen,FindItemScreen;

    public Button shortStoryButton, longStoryButton, findItemButton;

    bool IsOpening;

    private void Awake()
    {
        AttScr.SetActive(false);

        loadGameBtn.onClick.AddListener(OnClickLoadGameBtn);
        newGameBtn.onClick.AddListener(OnClickNewGameBtn);
        anotherGameBtn.onClick.AddListener(OnClickAnotherGameBtn);
        twitterBtn.onClick.AddListener(OnClickTwitterBtn);

        shortStoryButton.onClick.AddListener(OnClickShortStoryButton);
        longStoryButton.onClick.AddListener(OnClickLongStoryButton);
        findItemButton.onClick.AddListener(OnClickFindItemButton);

        OnClickShortStoryButton();
    }

    private async void Start()
    {
        //InitializeGoogleAD();
        AttScr.SetActive(false);

        GameStartLogo.I.PresentLogo();


#if UNITY_IOS && !UNITY_EDITOR
        
        if(!DataPersistenceManager.I.gameData.IsAtt)
        {
            AttScr.SetActive(true);
        }
#endif
        await GameStartLogo.I.WaitForEndLogoAsync();

#if UNITY_ANDROID && !UNITY_EDITOR
        GDRPMessage.I.StartGdpr();
#endif

        titleBGMDirector.PlayTilteBGM();
    }


    public async UniTask WaitForEndAttAsync()
    {
        while (true)
        {
            if (AttScr.GetComponent<CanvasGroup>().alpha == 0 || !AttScr.activeSelf)
            {
                break;
            }

            await UniTask.Delay(100); // 適切な待機時間を設定してください
        }
    }

    public void InitializeGoogleAD()
    {
        // Initialize the Google Mobile Ads SDK.
        if (DataPersistenceManager.I.gameData.blockAd) return;

        // Initialize the Google Mobile Ads SDK.

        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.

        });

    }

    public void OnClickAnotherGameBtn()
    {
        Application.OpenURL("https://tsgamedev.com/");
    }

    public void OnClickTwitterBtn()
    {
        Application.OpenURL("https://twitter.com/TennpaGames");
    }

    public void OnClickShortStoryButton()
    {
        ShortStoryScreen.SetActive(true);
        LongStoryScreen.SetActive(false);
        FindItemScreen.SetActive(false);
    }
    
    public void OnClickLongStoryButton()
    {
        ShortStoryScreen.SetActive(false);
        LongStoryScreen.SetActive(true);
        FindItemScreen.SetActive(false);
    }

    public void OnClickFindItemButton()
    {
        FindItemScreen.SetActive(true);
        ShortStoryScreen.SetActive(false);
        LongStoryScreen.SetActive(false);
    }

    public async void OnClickLoadGameBtn()
    {
        LoadingScr.I.PresentLoadingScr();
        //await LoadStageScean();

        await UniTask.Yield();
        
        SceneManager.LoadScene("StageScean");

        /*
        if (!IsLoadAd)
        {
            TitleBGMDirector.I.forceMute();
            GoogleAdManager.I.googleAD_LoadingScene.ShowAdAndLoadScene("StageScean");
            IsLoadAd = true;
        }
        else
        {
            SceneManager.LoadScene("StageScean");
        }
        */

    }

    public async void OnClickNewGameBtn()
    {
        if(DataPersistenceManager.I.gameData.S_Played == true)
        {
            CautionScr.I.PresentCautionScrLocalize("DeleteSaveData", OnClickYesBtn);
        }
        else
        {
            LoadingScr.I.PresentLoadingScr();
            await LoadStageScean();
            //SceneManager.LoadScene("StageScean");
        }
    }


    public async void OnClickYesBtn()
    {
        LoadingScr.I.PresentLoadingScr();
        CautionScr.I.HideCautionScr();
        DataPersistenceManager.I.NewGame();

        await LoadStageScean();
        //SceneManager.LoadScene("StageScean");
    }

    public async UniTask LoadStageScean()
    {
        //AppOpenAdTitle.I.ShowAppOpenAd();

        //await UniTask.WaitUntil(() => AppOpenAdTitle.I.IsPresent == false);
        await UniTask.Yield();
        SceneManager.LoadScene("StageScean");
    }

}
