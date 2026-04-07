using UnityEngine;
using UnityEngine.UI;

public class StageMoveButton : MonoBehaviour
{
    public StageContext.StageType stageType;
    public string sceneName;

    public Button mainButton;
    public Button UnlockButton;
    public Button newGameButton;
    public Button loadGameButton;

    public bool Lock;
    public GameObject lockScreen;
    public GameObject clearMark;

    private FindItemSaveRoot findItemSaveRoot;

    private void Awake()
    {
        mainButton.onClick.AddListener(OnClickButton);
        UnlockButton.onClick.AddListener(OnClickUnLockButton);
        //newGameButton.onClick.AddListener(OnClickNewGameBtn);
        //loadGameButton.onClick.AddListener(OnClickLoadGameButton);

        if(stageType == StageContext.StageType.none)
        {
            if(DataPersistenceManager.I.gameData.S_findItemSaveRoots != null)
            {
                for (int i = 0; i < DataPersistenceManager.I.gameData.S_findItemSaveRoots.Count; i++)
                {
                    if(DataPersistenceManager.I.gameData.S_findItemSaveRoots[i].FindItemKeysName == sceneName)
                    {
                        findItemSaveRoot = DataPersistenceManager.I.gameData.S_findItemSaveRoots[i];          
                    }
                }
            }
        }
    }

    public void Update()
    {        
        if(stageType == StageContext.StageType.none)
        {
            if(findItemSaveRoot != null)
            {
                clearMark.SetActive(findItemSaveRoot.IsAllCollect);
            }
            else
            {
                clearMark.SetActive(false);
            }
        }
        else
        {
            clearMark.SetActive(ProgressSaveManger.IsClearStage(stageType.ToString()));
            lockScreen.SetActive(Lock && !ProgressSaveManger.IsClearStage(stageType.ToString()) && !ProgressSaveManger.IsAlreadyPlay(stageType.ToString()) && !ProgressSaveManger.IsUnlockStage(stageType.ToString()));
        }      
    }

    private void OnClickButton()
    {
        StageMoveScreen.I.PresentStageMoveScreen(this);
    }

    private void OnClickUnLockButton()
    {
        CautionScr.I.PresentCautionScrLocalize("UnLockStageAd",()=> GoogleAdManager.I.googleAD_RewardSubEvent.ShowRewardedAd(UnlockStage));
    }

    public void UnlockStage()
    {
        ProgressSaveManger.GetProgressDatas(stageType.ToString()).IsUnLockStage = true;
        CautionScr.I.HideCautionScr();
    }

    public bool IsNullFindItemSaveRoot()
    {
        return findItemSaveRoot == null;
    }

    public bool IsAlreadyPlayFindItemSaveRoot()
    {
        if(findItemSaveRoot == null) return false;
        return findItemSaveRoot.IsAlreadyPlay();
    }

    public void ResetfindItemSaveRoot()
    {
        findItemSaveRoot.ResetData();
    }
}
