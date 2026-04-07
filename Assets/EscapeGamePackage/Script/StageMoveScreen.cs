using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageMoveScreen : SingletonMonoBehaviour<StageMoveScreen>
{
    private StageMoveButton stageMoveButton;
    private CanvasGroup canvasGroup;

    public Button newGameButton;
    public Button loadGameButton;

    protected override void OnAwake()
    {
        base.OnAwake();
        canvasGroup = GetComponent<CanvasGroup>();
        UsefulMethod.Hide(canvasGroup);

        newGameButton.onClick.AddListener(OnClickNewGameBtn);
        loadGameButton.onClick.AddListener(OnClickLoadGameButton);
    }

    public void PresentStageMoveScreen(StageMoveButton stageMoveButton)
    {
        this.stageMoveButton = stageMoveButton;
        UsefulMethod.Present(canvasGroup);
    }

    public void OnClickNewGameBtn()
    {

        if(stageMoveButton.stageType == StageContext.StageType.none)
        {
            if(!stageMoveButton.IsAlreadyPlayFindItemSaveRoot())
            {
                OnClickYesButton();
            }
            else
            {
                CautionScr.I.PresentCautionScrLocalize("DeleteSaveData", OnClickYesButton);
            }

        }
        else
        {
           
            if (!ProgressSaveManger.IsAlreadyPlay(stageMoveButton.stageType.ToString()))
            {
                OnClickYesButton();
            }
            else
            {
                CautionScr.I.PresentCautionScrLocalize("DeleteSaveData", OnClickYesButton);
            } 
        }
    }

    public void OnClickYesButton()
    {
        LoadingScr.I.PresentLoadingScr();
        StageContext.currentStageType = stageMoveButton.stageType;
        ProgressSaveManger.GetProgressDatas(stageMoveButton.stageType.ToString()).ResetData();
        DeleteFindItemStageSave();
        SceneManager.LoadScene(stageMoveButton.sceneName);
    }

    public void OnClickLoadGameButton()
    {
        LoadingScr.I.PresentLoadingScr();
        StageContext.currentStageType = stageMoveButton.stageType;
        SceneManager.LoadScene(stageMoveButton.sceneName);
    }

    public void DeleteFindItemStageSave()
    {
        if(stageMoveButton.stageType == StageContext.StageType.none && !stageMoveButton.IsNullFindItemSaveRoot())
        {
            stageMoveButton.ResetfindItemSaveRoot();
        }
    }


}
