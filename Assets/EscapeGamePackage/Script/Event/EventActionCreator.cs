using Cysharp.Threading.Tasks;
using DG.Tweening;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class EventActionCreator : MonoBehaviour,IPointerClickHandler
{
    public EventActionConfig eventActionConfig;
    public bool FreeMove;
    private bool NowEvent;

    public bool targetProgressEnd;

    private EventActionCreator[] OtherEventActionCreatorsSameGameObject;

    public bool IsInitialized { get; private set; }
    public bool IsLoaded { get; private set; }
    public bool isAllLoaded { get; private set; }

    public static List<EventActionCreator> NowEventList = new List<EventActionCreator>();

    private CancellationTokenSource _cancellationTokenSource;

    private Collider2D col;

    private bool isCancelled = false;

    private void OnDisable()
    {
        // オブジェクトが非アクティブになったときに非同期操作をキャンセル
        _cancellationTokenSource?.Cancel();

    }

    public async UniTask WaitUntilConfigIsNotNullAsync()
    {
        while (eventActionConfig == null)
        {
            await UniTask.Yield(); // 他の処理が行われるように一時的に制御を返します。
        }
    }

    public  void CanselEvent()
    {
        isCancelled = true;
        FadeManager.I.FadeReset();
        
    }

    public async UniTask InitializeGame()
    {
        NowEventList.Clear();
        if(gameObject != null) eventActionConfig.gameObjectName = gameObject.name;

        await WaitUntilConfigIsNotNullAsync();


        for (int i = 0; i < eventActionConfig.actionList.Count; i++)
        {
            eventActionConfig.actionList[i].Initialize(eventActionConfig);
        }

        if (eventActionConfig.eventActionTrigger.triggerType == EventActionTriggerType.Gimmick)
        {
            eventActionConfig.eventActionTrigger.gimmick.ClearAction = EventAction;
        }

        if (eventActionConfig.eventActionTrigger.triggerType == EventActionTriggerType.PointerClick)
        {
            if (GetComponent<BoxCollider2D>() == null && GetComponent<Image>() == null)
            {
                gameObject.AddComponent<BoxCollider2D>();
            }
        }

        switch (eventActionConfig.eventActionTrigger.triggerType)
        {
            case EventActionTriggerType.Initialize:

                await EventAction();
                break;

        }

        await UniTask.WaitUntil(() => IsSelectActionNotNull());

        IsInitialized = true;

        col = GetComponent<BoxCollider2D>();

    }



    public async UniTask LoadGame()
    {
        switch (eventActionConfig.eventActionTrigger.triggerType)
        {

            case EventActionTriggerType.GameStart:
                if (!ProgressDirector.I.IsDoneProgress(eventActionConfig.progressKey) && !eventActionConfig.eventActionTrigger.dontStopIsDoneProgress)
                {
                    await EventAction();
                }
                else
                {
                    Load();
                }

                break;

            case EventActionTriggerType.PointerClick:

                if (ProgressDirector.I.IsDoneProgress(eventActionConfig.progressKey) && !eventActionConfig.eventActionTrigger.dontStopIsDoneProgress)
                {
                    Load();
                }

                break;

            case EventActionTriggerType.ProgressEnd:

                if (ProgressDirector.I.IsDoneProgress(eventActionConfig.progressKey) && !eventActionConfig.eventActionTrigger.dontStopIsDoneProgress)
                {
                    if (gameObject.name == "Search_RoomPW")
                    {
                        Debug.Log("ここを通っている");
                    }

                    targetProgressEnd = true;
                    Load();
                }

                break;

            case EventActionTriggerType.Gimmick:

                if (ProgressDirector.I.IsDoneProgress(eventActionConfig.eventActionTrigger.gimmick.progressKey) && !eventActionConfig.eventActionTrigger.dontStopIsDoneProgress)
                {
                    targetProgressEnd = true;
                    Load();
                }
                break;

            case EventActionTriggerType.ZoomIn:
                StageSwitcher.I.mapManager.OnZoomIn += EventAction;
                break;

            case EventActionTriggerType.ZoomOut:
                StageSwitcher.I.mapManager.OnZoomOut += EventAction;
                break;

            case EventActionTriggerType.Loop:

                StartRepeatActInfinite();
                break;

            case EventActionTriggerType.MapChange:

                if (!ProgressDirector.I.IsDoneProgress(eventActionConfig.progressKey) && !eventActionConfig.eventActionTrigger.dontStopIsDoneProgress)
                {
                    StageSwitcher.I.mapManager.OnMapChange += EventAction;

                    Debug.Log("追加したよ");
                }
                else
                {
                    Load();
                }

                break;

            case EventActionTriggerType.Complete:

                if (ProgressDirector.I.IsComplete() && DataPersistenceManager.I.gameData.IsCompleteEventAction)
                {
                    targetProgressEnd = true;
                    Load();
                }

                break;

            case EventActionTriggerType.None:

                if (ProgressDirector.I.IsDoneProgress(eventActionConfig.progressKey) && !eventActionConfig.eventActionTrigger.dontStopIsDoneProgress)
                {
                    Load();
                }

                break;
        }

        IsLoaded = true;
    }

    public bool IsSelectActionNotNull()
    {
        for (int i = 0; i < eventActionConfig.actionList.Count; i++)
        {
            if (!eventActionConfig.actionList[i].IsInitialized())
            {
                return false;
            }
        }

        return true;
    }

    private void Start()
    {
        OtherEventActionCreatorsSameGameObject = FindOtherEventActionCreatorsOnSameGameObject();
    }


    private async void Update()
    {
        /*
        if(eventActionConfig.progressKey.saveKey =="Element 21")
        {
            Debug.Log(gameObject.name);
        }
        */
        
        try
        {
            if (!GameDirector.I.SetUpEnd) return;

            for (int i = 0; i < eventActionConfig.actionList.Count; i++)
            {
                eventActionConfig.actionList[i].selectAction?.OnUpdate();
            }


            switch (eventActionConfig.eventActionTrigger.triggerType)
            {
                case EventActionTriggerType.ProgressEnd:

                    if (eventActionConfig.eventActionTrigger.DontStartNotShowStageScreen && ItemWindow.I.IsPresent)
                    {
                        return;
                    }

                    if (eventActionConfig.eventActionTrigger.targetSetting.JudgeSwitch() && targetProgressEnd == false && IsSelectActionNotNull() && eventActionConfig.collectionItemSwitch.CollectionItemJudge())
                    {
                        targetProgressEnd = true;
                        await EventAction();
                    }

                    break;
                
                case EventActionTriggerType.Complete:

                    if (ProgressDirector.I.IsComplete() && !DataPersistenceManager.I.gameData.IsCompleteEventAction)
                    {
                        targetProgressEnd = true;
                        DataPersistenceManager.I.gameData.IsCompleteEventAction = true;
                        
                        await EventAction();
                    }
                    break;

                case EventActionTriggerType.Gimmick:

                    //Gimmickの時にはGimmickのコンポーネントで管理。

                    break;

            }

        }
        catch
        {

        }
        
        if(col != null)
        {
            if (ProgressDirector.I.IsDoneProgress(eventActionConfig.progressKey) && OtherEventActionCreatorsIsDoneProgress() && !eventActionConfig.eventActionTrigger.dontStopIsDoneProgress)
            {
                col.enabled = false;
            }

        }
        
    }

    public bool OtherEventActionCreatorsIsDoneProgress()
    {
        for (int i = 0; i < OtherEventActionCreatorsSameGameObject.Length; i++)
        {
            if (!ProgressDirector.I.IsDoneProgress(OtherEventActionCreatorsSameGameObject[i].eventActionConfig.progressKey))
            {
                return false;
            }
        }

        return true;
    }


    public async void OnPointerClick(PointerEventData eventData)
    {

        if (eventActionConfig.eventActionTrigger.triggerType != EventActionTriggerType.PointerClick) return;

        if (eventActionConfig.eventActionTrigger.needZoom != StageSwitcher.I.mapManager.IsZoom && !eventActionConfig.eventActionTrigger.always) return;

        if (ProgressDirector.I.IsDoneProgress(eventActionConfig.progressKey) && !eventActionConfig.eventActionTrigger.dontStopIsDoneProgress) return;

        //if (!eventActionConfig.progressJudge.EnableAction()) return;

        //if (!eventActionConfig.collectionItemSwitch.CollectionItemJudge()) return;

        if (OthersUseItemCheck()) return;

        await EventAction();

    }

    public bool IsCorrectItem()
    {
        if(!eventActionConfig.eventActionTrigger.needItem) return false;

        for (int i = 0; i < eventActionConfig.eventActionTrigger.needItemTypes.Length; i++)
        {
            if (ItemManager.I.currentItem == eventActionConfig.eventActionTrigger.needItemTypes[i])
            {
                return true;
            }
        }

        return false;
    }

    public async UniTask PresentUnDoneMessage()
    {
        if (!ProgressDirector.I.IsDoneProgress(eventActionConfig.progressKey) && eventActionConfig.eventActionTrigger.presentUnDoneMessage)
        {
            NowEventList.Add(this);

            await eventActionConfig.eventActionTrigger.UnDoneMessage.Act();

            if (NowEventList.Contains(this))
            {
                NowEventList.Remove(this);
            }

        }
    }


    public bool OthersUseItemCheck()
    {
        for (int i = 0; i < OtherEventActionCreatorsSameGameObject.Length; i++)
        {
            if (OtherEventActionCreatorsSameGameObject[i].IsCorrectItem() && !ProgressDirector.I.IsDoneProgress(OtherEventActionCreatorsSameGameObject[i].eventActionConfig.progressKey))
            {
                return true;
            }
        }
        return false;
    }

    EventActionCreator[] FindOtherEventActionCreatorsOnSameGameObject()
    {
        // 自分自身のGameObjectにアタッチされているEventActionCreatorの全インスタンスを取得
        EventActionCreator[] allCreatorsOnSameGameObject = GetComponents<EventActionCreator>();

        // 自分自身をフィルターで除外
        EventActionCreator[] otherCreators = System.Array.FindAll(allCreatorsOnSameGameObject, creator => creator != this);

        return otherCreators;
    }

    public void Load()
    {
        for (int i = 0; i < eventActionConfig.actionList.Count; i++)
        {
            eventActionConfig.actionList[i].selectAction?.Load();
        }
    }

    public async UniTask EventAction()
    {

        if (NowEvent) return;

        if (eventActionConfig.eventActionTrigger.needItem)
        {
            if (!IsCorrectItem()) { await PresentUnDoneMessage(); return; }
        }
        

        if (!eventActionConfig.progressJudge.EnableAction()) { await PresentUnDoneMessage(); return; }

        if (!eventActionConfig.numCounterJudge.numCounterJudge()) { await PresentUnDoneMessage(); return; }

        if (!eventActionConfig.setActiveJudge.setActiveJudge()) { await PresentUnDoneMessage(); return; }

        if (!eventActionConfig.mapChangeJudge.JudeTargetMap()) { await PresentUnDoneMessage(); return; }


        if (!eventActionConfig.collectionItemSwitch.CollectionItemJudge()) { await PresentUnDoneMessage(); return; }


        if (eventActionConfig.eventActionTrigger.triggerType == EventActionTriggerType.MapChange)
        {
            if (!eventActionConfig.eventActionTrigger.targetMap.JudeTargetMap() || ProgressDirector.I.IsDoneProgress(eventActionConfig.progressKey)) { await PresentUnDoneMessage(); return; }
        }

        if (eventActionConfig.eventActionTrigger.triggerType != EventActionTriggerType.Loop && eventActionConfig.eventActionTrigger.triggerType != EventActionTriggerType.Initialize && !FreeMove)
        {
            NowEventList.Add(this);
        }

        NowEvent = true;
        isCancelled = false;

        try
        {
            if(eventActionConfig.progressKey.episodeType == StageContext.StageType.none)
            ProgressDirector.I.CurrentProgressKey = eventActionConfig.progressKey;

            for (int i = 0; i < eventActionConfig.actionList.Count; i++)
            {

                if (isCancelled)
                {
                    Debug.Log("EventAction がキャンセルされました");
                    break;
                }

                if (eventActionConfig.actionList[i].eventActionType == EventActionType.DoneProgress)
                {
                    ProgressDirector.I.DoneProgress(eventActionConfig.progressKey);
                }
                else
                {
                    await eventActionConfig.actionList[i].selectAction.Act();
                }
            }

            if (!ProgressDirector.I.IsDoneProgress(eventActionConfig.progressKey))
            {
                ProgressDirector.I.DoneProgress(eventActionConfig.progressKey);
            }
        }
        catch
        {
            //Debug.Log(gameObject.name+" " + hoge);
        }

        if (NowEventList.Contains(this))
        {
            NowEventList.Remove(this);
        }

        NowEvent = false;

        //MapManager.I.OnMapChange?.Invoke();

    }

    public void StartRepeatActInfinite()
    {
        // 以前の非同期操作があればキャンセルして新しいトークンソースを作成
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();

        // 非同期操作を開始
        RepeatActInfinite(_cancellationTokenSource.Token).Forget();
    }

    private async UniTask RepeatActInfinite(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (gameObject != null && gameObject.activeInHierarchy)
                {
                    // ここに非同期のロジックを書く
                    await EventAction();
                }
                else
                {
                    await UniTask.Delay(100, cancellationToken: cancellationToken);
                }
            }
        }
        catch
        {

        }

    }

}

[Serializable]
public class EventActionConfig
{
    [SerializeField] public EventActionTrigger eventActionTrigger;
    public TargetKey progressKey;
    [SerializeField] public ProgressJudge progressJudge;
    [SerializeField] public CollectionItemSwitch collectionItemSwitch;
    [SerializeField] public NumCounterJudge numCounterJudge;
    [SerializeField] public SetActiveJudge setActiveJudge;
    [SerializeField] public MapChangeJudge mapChangeJudge;

    [SerializeField] public List<EventAction> actionList;

    public string gameObjectName;
    
}


[Serializable]
public class EventAction
{
    [SerializeField] public EventActionType eventActionType;
    [SerializeField] public DebugEventAction debugEventAction;
    [SerializeField] public FadeIn fadeIn;
    [SerializeField] public FadeOut fadeOut;
    [SerializeField] public WaitTime waitTime;
    [SerializeField] public MessageEventAction messageEventAction;
    [SerializeField] public ZoomEventAction zoomEventAction;
    [SerializeField] public SetActiveEventAction setActiveEventAction;
    [SerializeField] public ChangeMapEventAction changeMapEventAction;
    [SerializeField] public ItemGetEventAction itemGetEventAction;
    [SerializeField] public PlaySeEventAction playSeEventAction;
    [SerializeField] public ItemRemoveEventAction itemRemoveEventAction;
    [SerializeField] public ColorChangeActionEvent colorChangeAction;
    [SerializeField] public ColorTwinkleActionEvent colorTwinkleAction;
    [SerializeField] public ShowAdEventAction showAdEventAction;
    [SerializeField] public MapCustomEventAction mapCustomEventAction;
    [SerializeField] public AutoZoomBackEventAction autoZoomBackEventAction;
    [SerializeField] public ObjectChangeEventAction objectChangeAnimationEventAction;
    [SerializeField] public CameraMoveEventAction cameraMoveEventAction;
    [SerializeField] public UnLockOpenCloseObjectEventAction unLockOpenCloseObjectEventAction;
    [SerializeField] public ChangeCameraCurrentIndexEventAction changeCameraCurrentIndexEventAction;
    [SerializeField] public EndingSuccessEventAction endingSuccessEventAction;
    [SerializeField] public PlayChangeBGMEventAction playChangeBGMEventAction;
    [SerializeField] public StopBGMEventAction stopBgmEventAction;
    [SerializeField] public StoreReviewEventAction storeReviewEventAction;
    [SerializeField] public ShakeCameraEventAction shakeCameraEventAction;
    [SerializeField] public ObjectMoveEventAction objectMoveEventAction;
    [SerializeField] public PlayTimelineEventAction playTimelineEventAction;
    [SerializeField] public EndingFailEventAction endingFailEventAction;
    [SerializeField] public CounterPlusEventAction counterPlusEventAction;
    [SerializeField] public DismissItemWindowEventAction dismissItemWindowEventAction;
    [SerializeField] public CollectionItemGetEventAction collectionItemGetEvent;
    [SerializeField] public EventActionEvent eventActionEvent;
    [SerializeField] public ObjectRotationEventAction objectRotationEventAction;
    [SerializeField] public PresentCloseUpPanel presentCloseUpPanel;
    [SerializeField] public ObjectScaleEventAction objectScaleEventAction;
    [SerializeField] public OnClickBackButtonEventAction OnClickBackButtonEventAction;
    [SerializeField] public StopSEActionEvent stopSeEventAction;
    [SerializeField] public ProgressFalse progressFalse;
    [SerializeField] public PresentGimmickEventAction presentGimmickEventAction;
    [SerializeField] public ChangeWhetherAndTimeEventAction changeWhetherAndTimeEventAction;
    [SerializeField] public ChangeEpisodeEventAction changeEpisodeEventAction;
    [SerializeField] public NonSelectItemEventAction nonSelectItemEventAction;
    [SerializeField] public EndStageEventAction endStageEventAction;
    [SerializeField] public PresentGimmickByName presentGimmickByName;
    [SerializeField] public changeItemSpriteEventAction changeItemSpriteEventAction;

    public IEventAction selectAction;

    public void Initialize(EventActionConfig config)
    {
        switch (eventActionType)
        {
            case EventActionType.debug:
                selectAction = debugEventAction;
                break;

            case EventActionType.DoneProgress:
                break;

            case EventActionType.fadeIn:
                selectAction = fadeIn;
                break;

            case EventActionType.fadeout:
                selectAction = fadeOut;
                break;

            case EventActionType.waitTime:
                selectAction = waitTime;
                break;

            case EventActionType.message:
                selectAction = messageEventAction;
                break;

            case EventActionType.zoom:
                selectAction = zoomEventAction;
                break;

            case EventActionType.setActive:
                selectAction = setActiveEventAction;
                setActiveEventAction.attachedObjectName = config.gameObjectName;
                break;

            case EventActionType.changeMap:
                selectAction = changeMapEventAction;
                break;

            case EventActionType.itemGet:
                selectAction = itemGetEventAction;
                break;

            case EventActionType.PlaySE:
                selectAction = playSeEventAction;
                break;

            case EventActionType.itemRemove:
                selectAction = itemRemoveEventAction;
                break;

            case EventActionType.ColorChange:
                selectAction = colorChangeAction;
                break;

            case EventActionType.ColorTwinkle:
                selectAction = colorTwinkleAction;
                break;

            case EventActionType.ShowAd:
                selectAction = showAdEventAction;
                showAdEventAction.progressKey = config.progressKey;
                break;


            case EventActionType.MapCustom:
                selectAction = mapCustomEventAction;
                break;


            case EventActionType.AutoZoomBack:
                selectAction = autoZoomBackEventAction;
                break;

            case EventActionType.ObjectChangeAnimation:
                selectAction = objectChangeAnimationEventAction;
                break;

            case EventActionType.CameraMove:
                selectAction = cameraMoveEventAction;
                break;

            case EventActionType.UnLockOpenCloseObject:
                selectAction = unLockOpenCloseObjectEventAction;
                break;

            case EventActionType.ChangeCameraCurrentIndex:
                selectAction = changeCameraCurrentIndexEventAction;
                break;

            case EventActionType.EndingSuccess:
                selectAction = endingSuccessEventAction;
                break;

            case EventActionType.PlayChangeBGM:
                selectAction = playChangeBGMEventAction;
                break;

            case EventActionType.StopBGM:
                selectAction = stopBgmEventAction;
                break;

            case EventActionType.StoreReview:
                selectAction = storeReviewEventAction;
                break;

            case EventActionType.ShakeCamera:
                selectAction = shakeCameraEventAction;
                break;

            case EventActionType.objectMove:
                selectAction = objectMoveEventAction;
                break;

            case EventActionType.PlayTimeline:
                selectAction = playTimelineEventAction;
                break;

            case EventActionType.EndingFail:
                selectAction = endingFailEventAction;
                break;

            case EventActionType.CounterPlus:
                selectAction = counterPlusEventAction;
                break;

            case EventActionType.DismissItemWindow:
                selectAction = dismissItemWindowEventAction;
                break;

            case EventActionType.CollectionItemGet:
                selectAction = collectionItemGetEvent;
                break;

            case EventActionType.EventActionEvent:
                selectAction = eventActionEvent;
                break;

            case EventActionType.ObjectRotation:
                selectAction = objectRotationEventAction;
                break;

            case EventActionType.PresentCloseUpPanel:
                selectAction = presentCloseUpPanel;
                break;

            case EventActionType.objectScale:
                selectAction = objectScaleEventAction;
                break;

            case EventActionType.OnClickBackButtonAction:
                selectAction = OnClickBackButtonEventAction;
                break;

            case EventActionType.StopSE:
                selectAction = stopSeEventAction;
                break;

            case EventActionType.ProgressFalse:
                selectAction = progressFalse;
                break;

            case EventActionType.PresentGimmick:
                selectAction = presentGimmickEventAction;
                break;

            case EventActionType.ChangeWhetherAndTime:
                selectAction = changeWhetherAndTimeEventAction;
                break;

            case EventActionType.ChangeEpisode:
                selectAction = changeEpisodeEventAction;
                break;

            case EventActionType.NonSelectItem:
                selectAction = nonSelectItemEventAction;
                break;

            case EventActionType.endStage:
                selectAction = endStageEventAction;
                break;

            case EventActionType.PresentGimmickByName:
                selectAction = presentGimmickByName;
                break;

            case EventActionType.changeItemSprite:
                selectAction = changeItemSpriteEventAction;
                break;
        }

        selectAction?.OnStart();
    }

    public bool IsInitialized()
    {
        if (eventActionType == EventActionType.none || eventActionType == EventActionType.DoneProgress)
        {
            return true;
        }
        else
        {
            if(selectAction != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}

[Serializable]
public class DebugEventAction : IEventAction
{
    public string debugText;

    public void OnStart() { }

    public void OnUpdate() { }


    public async UniTask Act()
    {
        Debug.Log(debugText);

        await UniTask.Delay(0);
    }

    public void Load(){ }
}

[Serializable]
public class FadeIn : IEventAction
{
    public float fadeInTime;
    public Color fadeInColor;

    public void OnStart() { }

    public void OnUpdate() { }


    public async UniTask Act()
    {
        FadeManager.I.FadeIn(fadeInTime,fadeInColor);
        await UniTask.Delay(Mathf.FloorToInt(fadeInTime * 1000));
    }

    public void Load() { }
}

[Serializable]
public class FadeOut : IEventAction
{
    public float fadeOutTime;
    public Color fadeOutColor = Color.black;

    public void OnStart() { }

    public void OnUpdate() { }


    public async UniTask Act()
    {
        FadeManager.I.FadeOut(fadeOutTime, fadeOutColor);
        await UniTask.Delay(Mathf.FloorToInt(fadeOutTime * 1000));
    }

    public void Load() { }
}


[Serializable]
public class WaitTime : IEventAction
{
    public float waitTime;

    public void OnStart() { }

    public void OnUpdate() { }


    public async UniTask Act()
    {
        await UniTask.Delay(Mathf.FloorToInt(waitTime * 1000));
    }

    public void Load() { }
}


[Serializable]
public class MessageEventAction : IEventAction
{

    public bool IsCommonMessage;
    public ProgressJudge progressJudge;

    public Message[] messages;


    public void OnStart() { }

    public void OnUpdate() { }


    public async UniTask Act()
    {
        if(!progressJudge.EnableAction()) return;

        if (!IsCommonMessage)
        {
            await MessageWindow.I.PresentMessageWindowAsync("Message", messages);
        }
        else
        {
            await MessageWindow.I.PresentMessageWindowAsync("CommonMessage", messages);
        }
    }

    public void Load() { }


}

[Serializable]
public class SetActiveEventAction : IEventAction
{
    [HideInInspector]public string attachedObjectName;

    public bool NoWait;
    public SetActiveSetting[] setActiveObjects;


    public void OnStart() { }

    public void OnUpdate() { }


    public async UniTask Act()
    {
        for (int i = 0; i < setActiveObjects.Length; i++)
        {
            if (setActiveObjects[i].gameObject == null)
            {
                //Debug.LogError(setActiveObjects.Count() +  "ヌル");

            }

            if (!NoWait)
            {
                await setActiveObjects[i].seEventAction.Act();
                setActiveObjects[i].gameObject.SetActive(setActiveObjects[i].setActive);
                //Debug.Log(setActiveObjects[i].gameObject.name);

            }
            else
            {
                setActiveObjects[i].gameObject.SetActive(setActiveObjects[i].setActive);
                setActiveObjects[i].seEventAction.Act().Forget();
                //Debug.Log(setActiveObjects[i].gameObject.name);
            }

        }

        if (!NoWait)
        await UniTask.Yield();

    }

    public void Load()
    {
        for (int i = 0; i < setActiveObjects.Length; i++)
        { 
            try
            {
                setActiveObjects[i].gameObject.SetActive(setActiveObjects[i].setActive);
                
            }
            catch (System.Exception)
            {
                
                Debug.LogError(attachedObjectName + " null");
            }       
        }
    }

    [Serializable]
    public class SetActiveSetting
    {
        public PlaySeEventAction seEventAction;
        public GameObject gameObject;
        public bool setActive;
    }

}

[Serializable]
public class ZoomEventAction : IEventAction
{ 
    public GameObject target;
    public Vector2 offset;
    public float zoomValue;
    public float duration;

    public bool Nowait;
    public bool force;
    public bool dontDisableInZoomCollier2d;

    public Collider2D collider;

    public void OnStart() 
    {
        try
        {
            collider = target.GetComponent<Collider2D>();

        }
        catch
        {
            Debug.Log(target == null);
        }
    }

    public void OnUpdate() 
    {
        if(collider == null)
        {
            collider = target.GetComponent<Collider2D>();
            return;
        }

        if(!dontDisableInZoomCollier2d)
        collider.enabled = !StageSwitcher.I.mapManager.IsZoom;
    }


    public async UniTask Act()
    {
        var targetPos = new Vector2(target.transform.position.x + offset.x, target.transform.position.y + offset.y);

        if(Nowait)
        {
            StageSwitcher.I.mapManager.Zoom(targetPos, zoomValue, duration, force).Forget();
        }
        else
        {
            await StageSwitcher.I.mapManager.Zoom(targetPos, zoomValue, duration, force);

        }
    }

    public void Load() { }
}

[Serializable]
public class ChangeMapEventAction : IEventAction
{
    public Map targetMap;
    public int indexX;
    public int indexY;
    public bool dontSave;
    public void OnStart() { }
    public void OnUpdate() { }


    public async UniTask Act()
    {
        Debug.Log("チェンジマップ " + StageSwitcher.I.mapManager.gameObject.name);

        targetMap.currentIndexX = indexX;
        targetMap.currentIndexY = indexY;
        await StageSwitcher.I.mapManager.ChangeMap(targetMap.myMapInfo.mapName, dontSave);
    }

    public void Load() { }
}

[Serializable]
public class ItemGetEventAction : IEventAction
{
    public string itemType;
    public bool animation;
    public bool presentItemWindow;
    public Message ItemGetMessageTerm;

    public void OnStart(){ }
    public void OnUpdate() { }

    public async UniTask Act()
    {
        ItemManager.I.GetItem(itemType,animation,presentItemWindow,true);

        if(ItemGetMessageTerm.messageTerms != null && ItemGetMessageTerm.messageTerms.Length != 0)
        {
            await MessageWindow.I.PresentMessageWindowAsync("Message", ItemGetMessageTerm);
        }

        await UniTask.Delay(1500);

        ItemWindow.I.DismissItemWindow();

    }

    public void Load() { }

}

[Serializable]
public class CollectionItemGetEventAction : IEventAction
{
    public string collectionItemType;
    public bool animation;
    public Message ItemGetMessageTerm;

    public void OnStart() { }
    public void OnUpdate() { }

    public async UniTask Act()
    {
        ItemManager.I.GetCollectionItem(collectionItemType, animation);

        if (ItemGetMessageTerm.messageTerms != null && ItemGetMessageTerm.messageTerms.Length != 0)
        {
            await MessageWindow.I.PresentMessageWindowAsync("Message", ItemGetMessageTerm);
        }

        await UniTask.Yield();

    }

    public void Load() { }

}



[Serializable]
public class ItemRemoveEventAction : IEventAction
{
    public ItemType[] itemTypes;

    public bool OnlyVisualHide;

    public void OnStart() { }

    public void OnUpdate() { }

    public async UniTask Act()
    {
        /*
        for (int i = 0; i < itemTypes.Length; i++)
        {
            if(OnlyVisualHide)
            {
                ItemManager.I.HideItem(itemTypes[i]);

            }
            else
            {
                ItemManager.I.RemoveItem(itemTypes[i]);
                await UniTask.Yield();
            }

        }
        */

        await UniTask.Yield();

    }

    public void Load() { }

}

[Serializable]
public class PlaySeEventAction : IEventAction
{
    public AudioDirector.SeType seType;
    public AudioClip customAudioClip;

    public float startTime;

    public void OnStart() { }

    public void OnUpdate() { }

    public async UniTask Act()
    {
        AudioDirector.I.PlaySE(seType, customAudioClip,1,startTime);
        await UniTask.Yield();
    }

    public void Load() { }

}


[Serializable]
public class ColorChangeActionEvent : IEventAction
{
    public SpriteRenderer[] targetSprites;

    public ColorChangeInfo[] colorChangeInfos;

    public async UniTask Act()
    {
        for (int i = 0; i < colorChangeInfos.Length; i++)
        {
            for (int s = 0; s < targetSprites.Length; s++)
            {
                targetSprites[s].DOColor(colorChangeInfos[i].color, colorChangeInfos[i].wait);
            }

            await UniTask.Delay(Mathf.FloorToInt(colorChangeInfos[i].wait * 1000));
        }
    }

    public void Load(){ }

    public void OnStart(){ }

    public void OnUpdate(){ }


}


[Serializable]
public class ColorTwinkleActionEvent : IEventAction
{
    public SpriteRenderer[] targetSprites;

    public Color baseColor;

    public float startTime;
    public float endTime;

    public float waitTime;
    public float changeTime;

    public float baseWaitTime;

    public Color[] colors;

    public async UniTask Act()
    {
        Debug.Log("からー");
        for (int s = 0; s < targetSprites.Length; s++)
        {
            targetSprites[s].DOColor(baseColor,0);
        }

        await UniTask.Delay(Mathf.FloorToInt(startTime * 1000));

        for (int i = 0; i < colors.Length; i++)
        {
            for (int s = 0; s < targetSprites.Length; s++)
            {
                targetSprites[s].DOColor(colors[i], changeTime);
            }

            await UniTask.Delay(Mathf.FloorToInt(waitTime * 1000));

            for (int s = 0; s < targetSprites.Length; s++)
            {
                targetSprites[s].DOColor(baseColor, changeTime);
            }

            await UniTask.Delay(Mathf.FloorToInt(baseWaitTime * 1000));
        }

        await UniTask.Delay(Mathf.FloorToInt(endTime * 1000));
    }

    public void Load() { }

    public void OnStart() { }

    public void OnUpdate() { }
}

[Serializable]
public class ShowAdEventAction : IEventAction
{
    public AdType adType;
    [HideInInspector]public TargetKey progressKey;
    //public EventActionCreator eventActionCreator;

    public async UniTask Act()
    {

        switch (adType)
        {
            case AdType.Inter:



                try
                {
                    if (ProgressDirector.I.GetProgressSaveData(progressKey).AdFinish) return;
                    GoogleAdManager.I.googleAD_Inter.ShowAd();
                    await GoogleAdManager.I.googleAD_Inter.WaitForAdToFinish();
                    ProgressDirector.I.GetProgressSaveData(progressKey).AdFinish = true;
                }
                catch
                {

                }

                break;

            case AdType.Reward:

                /*
                if (ProgressDirector.I.GetProgressSaveData(progressKey).AdFinish) return;

                Message message = new Message();
                message.presentChoose = true;
                message.messageTerms = new messageTerm[1];
                message.messageTerms[0] = new messageTerm();
                message.messageTerms[0].term = "StageAdMessage";

                message.yesAction = null;
                message.noAction = null;

                await MessageWindow.I.PresentMessageStageAdvanceAd(eventActionCreator,"CommonMessage", message);
                */
                break;
        }
    }

    public void Load(){ }

    public void OnStart(){ }

    public void OnUpdate(){ }

    public enum AdType
    {
        none,Inter,Reward,AppOpen
    }
}

[Serializable]
public class MapCustomEventAction : IEventAction
{
    public Map targetMap;
    public int cameraPosIndexX_max;
    public int cameraPosIndexX_min;
    public int cameraPosIndexY_max;
    public int cameraPosIndexY_min;

    public bool NoWait;

    public async UniTask Act()
    {
        targetMap.cameraPosXIndex_max = cameraPosIndexX_max;
        targetMap.cameraPosXIndex_min = cameraPosIndexX_min;
        targetMap.cameraPosYIndex_max = cameraPosIndexY_max;
        targetMap.cameraPosYIndex_min = cameraPosIndexY_min;

        if(!NoWait) await UniTask.Yield();
    }

    public void Load()
    {
        targetMap.cameraPosXIndex_max = cameraPosIndexX_max;
        targetMap.cameraPosXIndex_min = cameraPosIndexX_min;
        targetMap.cameraPosYIndex_max = cameraPosIndexY_max;
        targetMap.cameraPosYIndex_min = cameraPosIndexY_min;
    }

    public void OnStart(){}
    public void OnUpdate(){}
}

[Serializable]
public class AutoZoomBackEventAction : IEventAction
{

    public async UniTask Act()
    {
        await StageSwitcher.I.mapManager.BackZoom();
    }

    public void Load(){}

    public void OnStart() { }
    public void OnUpdate() { }
}

[Serializable]
public class ObjectChangeEventAction : IEventAction
{
    public bool UseCommonWait;
    public float CommonWait;
    public float startWait;
    public float endWait;
    public targetGameObject[] targets;

    public async UniTask Act()
    {
        if(startWait > 0) await UniTask.Delay(Mathf.FloorToInt(startWait * 1000));

        for (int i = 0; i < targets.Length; i++)
        {
            SetActiveTarget(i);

            if(UseCommonWait)
            {
                await UniTask.Delay(Mathf.FloorToInt(CommonWait * 1000));

            }
            else
            {
                await UniTask.Delay(Mathf.FloorToInt(targets[i].wait * 1000));
            }

        }

        SetActiveFailsAll();

        if (endWait > 0) await UniTask.Delay(Mathf.FloorToInt(endWait * 1000));
    }

    public void SetActiveTarget(int index)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].target.SetActive(i == index);
            if(i == index)targets[i].SeEvent.Act().Forget();
        }
    }

    public void SetActiveFailsAll()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].target.SetActive(false);
        }
    }

    public void Load()
    {

    }

    public void OnStart()
    {
        SetActiveFailsAll();
        targets[0].target.SetActive(true);
    }
    public void OnUpdate() { }

    [Serializable]
    public class targetGameObject
    {
        public GameObject target;
        public PlaySeEventAction SeEvent;
        public float wait;
    }

}

[Serializable]
public class CameraMoveEventAction : IEventAction
{
    
    public Vector2 targetPos;
    public float time;

    public bool NoWait;

    public async UniTask Act()
    {
        if(NoWait)
        {
            VCamManager.I.MoveMainVcam(targetPos, time).Forget();
        }
        else
        {
            await VCamManager.I.MoveMainVcam(targetPos, time);

        }

    }

    public void Load() { }

    public void OnStart() { }

    public void OnUpdate() { }

}


[Serializable]
public class UnLockOpenCloseObjectEventAction : IEventAction
{
    public OpenCloseObject target;

    public async UniTask Act()
    {
        await target.UnLockAction();
    }

    public void Load() 
    {
        target.IsLock = false;
        target.LoadOpen();
    }

    public void OnStart() { }

    public void OnUpdate() { }

}

[Serializable]
public class ChangeCameraCurrentIndexEventAction : IEventAction
{
    public Map target;
    public int indexX;
    public int indexY;

    public async UniTask Act()
    {
        target.currentIndexX = indexX;
        target.currentIndexY = indexY;
        StageSwitcher.I.mapManager.LoadMap();
        await UniTask.Yield();
    }

    public void Load()
    {
    }

    public void OnStart() { }

    public void OnUpdate() { }

}

[Serializable]
public class EndingSuccessEventAction : IEventAction
{
    public async UniTask Act()
    {
        EndingDirector.I.EndingSuccess();
        GameDirector.I.IsEndGame = true;
        await UniTask.Yield();
    }

    public void Load()
    {
    }

    public void OnStart() { }

    public void OnUpdate() { }

}

[Serializable]
public class EndingFailEventAction : IEventAction
{
    public async UniTask Act()
    {
        EndingDirector.I.EndingFail();
        GameDirector.I.IsEndGame = true;
        await UniTask.Yield();
    }

    public void Load()
    {
    }

    public void OnStart() { }

    public void OnUpdate() { }

}

[Serializable]
public class PlayChangeBGMEventAction : IEventAction
{
    public AudioDirector.BGMType bgmType;

    public TargetKey endKey;
    public bool playBgmOnLoad;
    public bool dontStopOtherBGM;

    public async UniTask Act()
    {
        AudioDirector.I.PlayBGM(bgmType,0,dontStopOtherBGM);
        await UniTask.Yield();
    }

    public void Load()
    {
        if(playBgmOnLoad && !ProgressDirector.I.IsDoneProgress(endKey))
        {
            AudioDirector.I.PlayBGM(bgmType,0,dontStopOtherBGM);
        }
    }

    public void OnStart() { }

    public void OnUpdate() { }

}

[Serializable]
public class StopBGMEventAction : IEventAction
{
    public float duration;
    public bool waitSameduration;

    public async UniTask Act()
    {
        AudioDirector.I.FadeoutBGM(duration);

        if(waitSameduration)
        {
            await UniTask.Delay(Mathf.FloorToInt(duration * 1000));
        }
        else
        {
            await UniTask.Yield();
        }
    }

    public void Load()
    {

    }

    public void OnStart() { }

    public void OnUpdate() { }

}

[Serializable]
public class StoreReviewEventAction : IEventAction
{
    public async UniTask Act()
    {
        StoreReview.I.PresentStoreReview();
        await UniTask.Yield();
    }

    public void Load()
    {
       
    }

    public void OnStart()
    {
        
    }

    public void OnUpdate()
    {
        
    }
}


[Serializable]
public class ShakeCameraEventAction : IEventAction
{
    public float duration;
    public float strength;
    public int vibrato;
    public float waitTime;
    public bool fadeOut;
    public bool infinity;

    public PlaySeEventAction seEventAction;

    public async UniTask Act()
    {
        await seEventAction.Act();

        await VCamManager.I.ShakeCamera(duration, strength, vibrato, fadeOut,waitTime,infinity);
    }

    public void Load()
    {

    }

    public void OnStart()
    {

    }

    public void OnUpdate()
    {

    }
}

[Serializable]
public class PlayTimelineEventAction : IEventAction
{
    public PlayableDirector timeline;
    public bool NoWait;
    public bool PlayOnLoad;

    public async UniTask Act()
    {
        timeline.Play();
        TimelineAsset asset = timeline.playableAsset as TimelineAsset;
        float waitTime = (float)asset.duration;

        if(NoWait)
        {

        }
        else
        {
            await UniTask.Delay(Mathf.FloorToInt(waitTime * 1000));
        }

    }

    public void Load()
    {
        if(PlayOnLoad)
            timeline.Play();
    }

    public void OnStart()
    {

    }

    public void OnUpdate()
    {

    }
}

[Serializable]
public class ObjectMoveEventAction : IEventAction
{
    public GameObject target;
    public Ease easeType;
    public float duration;
    public bool offset;
    public Vector3 value;

    public bool NoWait;
    public bool MoveOnLoad;

    public async UniTask Act()
    {
        var targetPos = value;

        if(offset)
        {
            targetPos += target.transform.localPosition;
        }

        target.transform.DOLocalMove(targetPos, duration).SetEase(easeType);

        if(!NoWait)
        await UniTask.Delay(Mathf.FloorToInt(duration * 1000));
    }

    public void Load()
    {
        var targetPos = value;

        if (offset)
        {
            targetPos += target.transform.localPosition;
        }

        if (MoveOnLoad) target.transform.localPosition = targetPos;
    }

    public void OnStart()
    {

    }

    public void OnUpdate()
    {

    }
}


[Serializable]
public class ObjectScaleEventAction : IEventAction
{
    public GameObject target;
    public Ease easeType;
    public float duration;
    public bool offset;
    public Vector3 value;

    public bool NoWait;
    public bool MoveOnLoad;

    public async UniTask Act()
    {
        var targetScale = value;

        if (offset)
        {
            targetScale += target.transform.localScale;
        }

        target.transform.DOScale(targetScale, duration).SetEase(easeType);

        if (!NoWait)
            await UniTask.Delay(Mathf.FloorToInt(duration * 1000));
    }

    public void Load()
    {
        var targetScale = value;

        if (offset)
        {
            targetScale += target.transform.localScale;
        }

        if (MoveOnLoad) target.transform.localScale = targetScale;
    }

    public void OnStart()
    {

    }

    public void OnUpdate()
    {

    }
}

[Serializable]
public class CounterPlusEventAction : IEventAction
{
    public NumCounter numCounter;
    public int value;

    public async UniTask Act()
    {
        numCounter.counter += value;
        await numCounter.EventCheck();
        await UniTask.Yield();
    }

    public void Load()
    {

    }

    public void OnStart()
    {

    }

    public void OnUpdate()
    {

    }
}

[Serializable]
public class DismissItemWindowEventAction : IEventAction
{

    public async UniTask Act()
    {
        ItemWindow.I.DismissItemWindow();
        await UniTask.Yield();
    }

    public void Load()
    {

    }

    public void OnStart()
    {

    }

    public void OnUpdate()
    {

    }
}

[Serializable]
public class ObjectRotationEventAction : IEventAction
{
    public GameObject target;
    public Ease easeType;
    public float duration;
    public bool offset;
    public bool reverse;
    public Vector3 value;

    public PlaySeEventAction playSe;

    public bool NoWait;
    public bool RotateOnLoad;

    public async UniTask Act()
    {
        var targetRot = value;

        if (offset)
        {
            targetRot += target.transform.localEulerAngles;
        }

        playSe.Act().Forget();

        if(reverse)
        {
            target.transform.DOLocalRotate(targetRot, duration, RotateMode.FastBeyond360).SetEase(easeType).SetLoops(2, LoopType.Yoyo); ;
        }
        else
        {
            target.transform.DOLocalRotate(targetRot, duration, RotateMode.FastBeyond360).SetEase(easeType);
        }


        if (!NoWait)
        {
            if (reverse)
            {
                await UniTask.Delay(Mathf.FloorToInt(duration * 2 * 1000));

            }
            else
            {
                await UniTask.Delay(Mathf.FloorToInt(duration * 1000));

            }
        }



    }

    public void Load()
    {
        var targetRot = value;

        if (offset)
        {
            targetRot += target.transform.localEulerAngles;
        }

        if (RotateOnLoad) target.transform.localEulerAngles= targetRot;
    }

    public void OnStart()
    {

    }

    public void OnUpdate()
    {

    }
}


[Serializable]
public class PresentCloseUpPanel : IEventAction
{
    public string type;

    public async UniTask Act()
    {
        await CloseUpPanel.I.PresentCloseUpPanel(type);  
    }

    public void Load()
    {

    }

    public void OnStart()
    {

    }

    public void OnUpdate()
    {

    }
}

[Serializable]
public class OnClickBackButtonEventAction : IEventAction
{
    public bool Nowait;

    public async UniTask Act()
    {
        GameObject.FindObjectOfType<MoveBtn>().OnClickBackBtn();
        if (!Nowait) await UniTask.Yield();
    }

    public void Load()
    {

    }

    public void OnStart()
    {

    }

    public void OnUpdate()
    {

    }
}

[Serializable]
public class EventActionEvent : IEventAction
{
    public EventActionCreator eventActionCreator;

    public async UniTask Act()
    {
        await eventActionCreator.EventAction();
    }

    public void Load()
    {
        eventActionCreator.Load();
    }

    public void OnStart()
    {

    }

    public void OnUpdate()
    {

    }
}

[Serializable]
public class StopSEActionEvent : IEventAction
{
    public async UniTask Act()
    {
        AudioDirector.I.seAudioSource.Stop();
        await UniTask.Yield();
    }

    public void Load()
    {

    }

    public void OnStart()
    {

    }

    public void OnUpdate()
    {

    }
}

[Serializable]
public class ProgressFalse : IEventAction
{
    public TargetKey targetKey;

    public async UniTask Act()
    {
        ProgressDirector.I.DoneProgressFalse(targetKey);
        await UniTask.Yield();
    }

    public void Load()
    {

    }

    public void OnStart()
    {

    }

    public void OnUpdate()
    {

    }
}

[Serializable]
public class PresentGimmickEventAction : IEventAction
{
    public Gimmick gimmick;

    public async UniTask Act()
    {
        gimmick.PresentGimmick();
        await UniTask.Yield();
    }

    public void Load()
    {

    }

    public void OnStart()
    {

    }

    public void OnUpdate()
    {

    }
}

[Serializable]
public class PresentGimmickByName : IEventAction
{
    public string gimmickName;
    [HideInInspector] public Gimmick gimmick;

    public async UniTask Act()
    {
        gimmick = GameObject.Find(gimmickName).GetComponent<Gimmick>();

        gimmick.PresentGimmick();
        await UniTask.Yield();
    }


    public void Load()
    {
        
    }

    public void OnStart()
    {

    }

    public void OnUpdate()
    {

    }
}

[Serializable]
public class ChangeWhetherAndTimeEventAction : IEventAction
{
    public weatherType weatherType;
    public timeType timeType;

    public async UniTask Act()
    {
        EnvironmentalManager.I.ChangeWhetherAndTime(weatherType, timeType);
        await UniTask.Yield();
    }

    public void Load()
    {

    }

    public void OnStart()
    {

    }

    public void OnUpdate()
    {

    }
}

[Serializable]
public class ChangeEpisodeEventAction : IEventAction
{
    public StageContext.StageType episodeType;

    public async UniTask Act()
    {
        StageContext.currentStageType = episodeType;
        ProgressDirector.I.SetProgressData();

        await UniTask.Yield();
    }

    public void Load()
    {

    }

    public void OnStart()
    {

    }

    public void OnUpdate()
    {

    }
}

[Serializable]
public class NonSelectItemEventAction : IEventAction
{

    public async UniTask Act()
    {
        ItemManager.I.currentItem = null;
        await UniTask.Yield();
    }

    public void Load()
    {

    }

    public void OnStart()
    {

    }

    public void OnUpdate()
    {

    }
}

[Serializable]
public class EndStageEventAction : IEventAction
{

    public async UniTask Act()
    {
        ProgressSaveManger.EndStage();

        if (ProgressSaveManger.GetClearCount() >= 3 && !DataPersistenceManager.I.gameData.IsReviewRequired)
        {
            StoreReview.I.PresentStoreReview();
            DataPersistenceManager.I.gameData.IsReviewRequired = true;
            Debug.Log(ProgressSaveManger.GetClearCount());
        }

        await UniTask.Yield();
    }

    public void Load()
    {

    }

    public void OnStart()
    {

    }

    public void OnUpdate()
    {

    }
}

[Serializable]
public class changeItemSpriteEventAction : IEventAction
{
    public Sprite targetSprite;

    public async UniTask Act()
    {
        ItemWindow.I.itemImage.sprite = targetSprite;
        await UniTask.Yield();
    }

    public void Load()
    {

    }

    public void OnStart()
    {

    }

    public void OnUpdate()
    {

    }
}



[Serializable]
public class ColorChangeInfo
{
    public Color color = Color.white;
    public float wait;
}


public interface IEventAction
{
    public void OnStart();//スタート時のアクション
    public void OnUpdate();//Update時のアクション

    public UniTask Act();//実行時のアクション
    public void Load();//ロード時のアクション

}

public enum EventActionType
{
    none, debug, fadeIn, fadeout, waitTime, message, zoom, setActive, changeMap, itemGet, itemRemove,
    ColorChange, ColorTwinkle, DoneProgress, PlaySE, ShowAd, MapCustom, AutoZoomBack, ObjectChangeAnimation,
    CameraMove, UnLockOpenCloseObject, ChangeCameraCurrentIndex, EndingSuccess, PlayChangeBGM, StopBGM, StoreReview,
    ShakeCamera, objectMove, PlayTimeline, EndingFail, CounterPlus, DismissItemWindow, CollectionItemGet, EventActionEvent, ObjectRotation,
    PresentCloseUpPanel, objectScale, OnClickBackButtonAction, StopSE, ProgressFalse, PresentGimmick, ChangeWhetherAndTime,
    ChangeEpisode,NonSelectItem,endStage,PresentGimmickByName,changeItemSprite
}