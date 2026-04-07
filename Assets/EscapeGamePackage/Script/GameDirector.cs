using Cysharp.Threading.Tasks;
using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class GameDirector : SingletonMonoBehaviour<GameDirector>
{
    public GameObject UICanvas;
    public GameObject adDeleteBtn1, adDeleteBtn2;
    public GameObject adDeleteScr;
    public bool PurChaseAdDeleteOnStore;

    public HelpScr helpScr;
    public CanvasGroup UIcanvasGroup;

    public List<EventActionCreator> AllEventActionCreators;

    public bool SetUpEnd;
    public bool IsEndGame;

    public StageContext.StageType debugStageType;

    protected override void OnAwake()
    {
        base.OnAwake();
    }

    protected override async void OnStart()
    {
        if(StageSwitcher.I) StageSwitcher.I.SetStage(StageContext.currentStageType);
        if(CollectionItemImageSwitcher.I) CollectionItemImageSwitcher.I.SetCollectionItemImage(StageContext.currentStageType);

        LoadingScr.I.PresentLoadingScr();

        UIcanvasGroup.interactable = false;
        UIcanvasGroup.blocksRaycasts = false;

        /*
        // 条件が満たされるまで待機し、かつ制限時間内であればループ
        await UniTask.WaitUntil(() => GoogleAdManager.I != null);

        Debug.Log("ここまではきてるよ");

        var timeout = 10f; // タイムアウトまでの秒数
        float time = 0;

        GoogleAdManager.I.googleAD_AppOpenAd.InitializeAD();
            
        while (GoogleAdManager.I.googleAD_AppOpenAd.appOpenAd == null)
        {
            time += Time.deltaTime;
            // 指定された制限時間を超えた場合、タイムアウトしてループを抜ける
            if (time  > timeout)
            {
                // タイムアウトした場合の処理をここに記述
                Debug.Log("タイムアウトしました。");
                break;
            }

            await UniTask.Yield(); // 次のフレームまで待機
        }

        GoogleAdManager.I.googleAD_AppOpenAd.ShowAppOpenAd();
        */

        /*
        if (DataPersistenceManager.I.gameData.S_Played == true)
        {
            await UniTask.WaitUntil(() => GoogleAdManager.I);
            GoogleAdManager.I.googleAD_Inter.ShowAd();
        }
        */

        await StageSwitcher.I.mapManager.AllMapPresent();

        await StageSwitcher.I.mapManager.SetUpMap();//システム上、全てActiveだからこのタイミングでも大丈夫

        await GetAllEventActionCreators();

        await InitializeGame();

        if (DataPersistenceManager.I.gameData.S_Played == false)
        {
            UIcanvasGroup.interactable = true;
            UIcanvasGroup.blocksRaycasts = true;

            helpScr.PresentHelpScr();
            DataPersistenceManager.I.gameData.S_Played = true;

            await UniTask.WaitUntil(() => !helpScr.IsPresetnScr);


            UIcanvasGroup.interactable = false;
            UIcanvasGroup.blocksRaycasts = false;


            await UniTask.Delay(1000);
        }

        LoadGame().Forget();


        await UniTask.Delay(500);

        SetUpEnd = true;

        LoadingScr.I.HideLoadingScr();

        UIcanvasGroup.interactable = true;
        UIcanvasGroup.blocksRaycasts = true;
    }

    public async UniTask GetAllEventActionCreators()
    {
        //AllEventActionCreators = FindObjectsByType<EventActionCreator>(FindObjectsInactive.Include,FindObjectsSortMode.None).ToList();
        AllEventActionCreators.AddRange(StageSwitcher.I.currentStageObject.GetComponentsInChildren<EventActionCreator>(true));
        await UniTask.Yield();
    }

    public async UniTask InitializeGame()
    {
        List<UniTask> initializeTasks = new List<UniTask>();

        for (int e = 0; e < AllEventActionCreators.Count; e++)
        {
            // InitializeGameがUniTaskを返すように変更されていることを想定
            initializeTasks.Add(AllEventActionCreators[e].InitializeGame());
        }

        await UniTask.WhenAll(initializeTasks);
    }

    public async UniTask LoadGame()
    {

        List<UniTask> LoadGameTasks = new List<UniTask>();

        for (int i = 0; i < AllEventActionCreators.Count; i++)
        {
            var targetKey = AllEventActionCreators[i].eventActionConfig.progressKey;

            for (int e = 0; e < AllEventActionCreators.Count; e++)
            {
                if (AllEventActionCreators[e].eventActionConfig.progressKey == targetKey)
                {
                    LoadGameTasks.Add(AllEventActionCreators[e].LoadGame());
                }
            }
        }

        await UniTask.WhenAll(LoadGameTasks);

        StageSwitcher.I.mapManager.OnMapChange?.Invoke(); //ここで再度実行しないと発生しないイベントがあるため。
    }

    void Update()
    {

        if (PurChaseAdDeleteOnStore)
        {
            if (DataPersistenceManager.I.gameData.blockAd)
            {
                adDeleteBtn1.SetActive(false);
                adDeleteBtn2.SetActive(false);
            }
            else
            {
                adDeleteBtn1.SetActive(true);
                adDeleteBtn2.SetActive(true);
            }
        }
    }
}
