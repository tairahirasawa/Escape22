using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

interface IOnClickBackBtn
{
    void OnClickBackBtn();
}

[RequireComponent(typeof(Button))]
public class MoveBtn : MonoBehaviour
{
    public ButtonType type;
    public CanvasScaler StageCanvas;
    public GameObject MoveObject;

    Button button;

    private IOnClickBackBtn[] IOnClickBackBtns;

    public static Action OnClickBackBtnAction;

    public static Action OnPushBtn;

    public static bool INEventAction;

    public static bool DisableBackBtn;
    public static bool DisableArrowBtn;

    public static bool DisableMove; //ボタンの連打防止に使用する。

    public GameObject MainVcam;
    public bool debug;

    public enum ButtonType
    {
        Right,Left,Back,Down,Up
    }


    private void Start()
    {
        OnClickBackBtnAction = null;
        OnPushBtn = null;
        INEventAction = false;

        MainVcam = GameObject.Find("MainVcam");

        button = GetComponent<Button>();

        DisableBackBtn = false;
        DisableArrowBtn = false;
        DisableMove = false;
        
        switch(type)
        {
            case ButtonType.Right:
                button.onClick.AddListener(OnClickRightBtn);
                break;

            case ButtonType.Left:
                button.onClick.AddListener(OnClickLeftBtn);
                break;

            case ButtonType.Up:
                button.onClick.AddListener(OnClickUpBtn);
                break;

            case ButtonType.Down:
                button.onClick.AddListener(OnClickDownBtn);
                break;

            case ButtonType.Back:
                button.onClick.AddListener(OnClickBackBtn);
                break;

        }

        IOnClickBackBtns = UsefulMethod.FindObjectOfInterfaces<IOnClickBackBtn>();
        DisableBackBtn = true;
    }

    private bool JudgeFreeMove()
    {
        for (int i = 0; i < EventActionCreator.NowEventList.Count; i++)
        {
            if (EventActionCreator.NowEventList[i].FreeMove) return true;
        }

        return false;
    }

    private bool MapChangeOrZoom()
    {
        for (int i = 0; i < EventActionCreator.NowEventList.Count; i++)
        {
            for (int e = 0; e< EventActionCreator.NowEventList[i].eventActionConfig.actionList.Count; e++)
            {
                if (
                    EventActionCreator.NowEventList[i].eventActionConfig.actionList[e].selectAction is ChangeMapEventAction 
                    || EventActionCreator.NowEventList[i].eventActionConfig.actionList[e].selectAction is ZoomEventAction 
                    || EventActionCreator.NowEventList[i].eventActionConfig.actionList[e].selectAction is AutoZoomBackEventAction
                    )
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void Update()
    {
        debug = DisableArrowBtn;

        if(INEventAction || ItemWindow.I.IsPresent || GimmickManager.I.IsPresentGimmickScr || EventActionCreator.NowEventList.Count > 0 && !JudgeFreeMove() || GameDirector.I.IsEndGame)// && !MapManager.I.IsZoom)
        {
            EnableBtn(false);
            return;
        }
        else
        {
            switch (type)
            {
                case ButtonType.Right:
                    EnableBtn(!DisableArrowBtn);
                    if (!DisableArrowBtn) EnableBtn(StageSwitcher.I.mapManager.EnableRightMove());

                    break;

                case ButtonType.Left:

                    EnableBtn(!DisableArrowBtn);
                    if (!DisableArrowBtn) EnableBtn(StageSwitcher.I.mapManager.EnableLeftMove());

                    break;

                case ButtonType.Down:

                    EnableBtn(!DisableArrowBtn);
                    if (!DisableArrowBtn) EnableBtn(StageSwitcher.I.mapManager.EnableDownMove());

                    break;

                case ButtonType.Up:

                    EnableBtn(!DisableArrowBtn);
                    if (!DisableArrowBtn) EnableBtn(StageSwitcher.I.mapManager.EnableUpMove());

                    break;

                case ButtonType.Back:


                    if (StageSwitcher.I.mapManager.currentMap != null && !StageSwitcher.I.mapManager.IsZoom)
                    {
                        if (StageSwitcher.I.mapManager.currentMap.DiasbleBackButton)
                        {
                            EnableBtn(false);
                            return;
                        }
                    }

                    EnableBtn(StageSwitcher.I.mapManager.CanBackPreviousMap || StageSwitcher.I.mapManager.IsZoom || GimmickManager.I.IsPresentGimmickScr);

                    break;

            }
        }
    }

    public static void DisableAllButton()
    {
        DisableArrowBtn = true;
        DisableBackBtn = true;
    }

    public static void EnableAllButton()
    {
        DisableArrowBtn = false;
        DisableBackBtn = false;
    }

    public void EnableBtn(bool value)
    {
        if(value == true)
        {
            UsefulMethod.Present(GetComponent<CanvasGroup>());
        }
        else
        {

            UsefulMethod.Hide(GetComponent<CanvasGroup>());
        }

        GetComponent<Button>().enabled = value;
    }

    public void OnClickRightBtn()
    {
        if(DisableMove) return;

        if(StageSwitcher.I.mapManager.currentMap.EnableRightMove())
        {
            AudioDirector.I.PlaySE(AudioDirector.SeType.button,null,0.5f);
            StageSwitcher.I.mapManager.CameraMoveX(StageSwitcher.I.mapManager.MoveAmountX());
            OnPushBtn?.Invoke();
        }

        StageSwitcher.I.mapManager.currentMap.AddCameraPosXIndex(1);

    }

    public void OnClickLeftBtn()
    {
        if (DisableMove) return;

        if (StageSwitcher.I.mapManager.currentMap.EnableLeftMove())
        {
            AudioDirector.I.PlaySE(AudioDirector.SeType.button,null, 0.5f);
            StageSwitcher.I.mapManager.CameraMoveX(StageSwitcher.I.mapManager.MoveAmountX() * -1);
            OnPushBtn?.Invoke();
        }

        StageSwitcher.I.mapManager.currentMap.AddCameraPosXIndex(-1);

    }

    public void OnClickDownBtn()
    {
        if (DisableMove) return;

        if (StageSwitcher.I.mapManager.currentMap.EnableDownMove())
        {
            AudioDirector.I.PlaySE(AudioDirector.SeType.button, null, 0.5f);
            StageSwitcher.I.mapManager.CameraMoveY(StageSwitcher.I.mapManager.MoveAmountY() * -1);
            OnPushBtn?.Invoke();
        }

        StageSwitcher.I.mapManager.currentMap.AddCameraPosYIndex(-1);
    }

    public void OnClickUpBtn()
    {
        if (DisableMove) return;

        if (StageSwitcher.I.mapManager.currentMap.EnableUpMove())
        {
            AudioDirector.I.PlaySE(AudioDirector.SeType.button, null, 0.5f);
            StageSwitcher.I.mapManager.CameraMoveY(StageSwitcher.I.mapManager.MoveAmountY());
            OnPushBtn?.Invoke();
        }

       StageSwitcher.I.mapManager.currentMap.AddCameraPosYIndex(1);
    }



    public async void OnClickBackBtn()
    {
        if (DisableMove) return;

        if (StageSwitcher.I.mapManager.IsZoom)
        {
            await StageSwitcher.I.mapManager.BackZoom();
            return;
        }

        OnPushBtn?.Invoke();
        OnClickBackBtnAction?.Invoke();

        StageSwitcher.I.mapManager.ChangePreviousMap(StageSwitcher.I.mapManager.currentMap.previousMapInfo);

        for (int i = 0; i < IOnClickBackBtns.Length; i++)
        {
            IOnClickBackBtns[i].OnClickBackBtn();
        }

    }

}
