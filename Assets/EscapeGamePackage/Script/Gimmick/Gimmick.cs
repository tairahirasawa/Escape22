using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gimmick : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] public GimmickType gimmickType;
    [SerializeField] public TargetKey progressKey;
    [SerializeField] public TargetKey lockProgressKey;

    [SerializeField] private bool needZoom;
    [SerializeField] private bool always;
    [SerializeField] private bool hideBG;
    [SerializeField] private bool DisableClick;
    //アイテム使用ギミック
    [SerializeField] private UseItem[] NeedItems;

    [SerializeField] public GimickButtonType gimmickButtonType;

    //ソートボタン、オーダーボタン共通
    [SerializeField] private GimmickButtonLayout gimmickButtonlayout;
    [SerializeField] private int[] answer;

    //ソートボタン
    [SerializeField] private GimmickButtonString[] gimmickButtonText;
    [SerializeField] private GimmickButtonSprite[] gimmickButtonSprite;

    [SerializeField] private EventAction[] eventActions;
    [SerializeField] private EventAction[] scrCloseButtonActions;

    public Func<UniTask> ClearAction;
    public bool IsDoneProgress => ProgressDirector.I.IsDoneProgress(progressKey);

    private Collider2D col;



    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if(IsDoneProgress && !GetComponent<OpenCloseObject>() && col != null)
        {
            col.enabled = false;
        }
    }

    public void ItemUse()
    {
        if (NeedItems.Length >= 1)
        {
            for (int i = 0; i < NeedItems.Length; i++)
            {
                if (ItemManager.I.currentItem == NeedItems[i].needItem)
                {
                    if (progressKey == null) ProgressDirector.I.CurrentProgressKey = progressKey;

                    ProgressDirector.I.DoneProgress(progressKey);
                    ClearAction();

                    if (!NeedItems[i].DontDeleteItem)
                    {
                        //ItemManager.I.RemoveItem(NeedItems[i].needItem);
                    }
                }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsDoneProgress) return;
        if (DisableClick) return;
        if(!always)
        {
            if (needZoom != StageSwitcher.I.mapManager.IsZoom) return;
        }
        if (!GameDirector.I.SetUpEnd) return; 
        if (GimmickManager.I.IsPresentGimmickScr) return;

        PresentGimmick();
    }

    public void PresentGimmick()
    {

        switch (gimmickType)
        {
            case GimmickType.itemUse:

                ItemUse();

                break;

            case GimmickType.sort:
                GimmickManager.I.bg.SetActive(!hideBG);
                PresentSortButtonWindow();

                break;

            case GimmickType.order:
                GimmickManager.I.bg.SetActive(!hideBG);
                PresentOrderButtonWindow();
                break;
        }
    }



    public void PresentSortButtonWindow()
    {
        if(progressKey == null) ProgressDirector.I.CurrentProgressKey = progressKey;

        switch (gimmickButtonType)
        {
            case GimickButtonType.textMesh:

                GimmickManager.I.PresentSortButtonGimmick(gimmickButtonlayout, gimmickButtonText,answer,progressKey,ClearAction,eventActions,scrCloseButtonActions,lockProgressKey);

                break;

            case GimickButtonType.sprite:

                GimmickManager.I.PresentSortButtonGimmick(gimmickButtonlayout, gimmickButtonSprite, answer, progressKey, ClearAction, eventActions, scrCloseButtonActions,lockProgressKey);

                break;
        }
    }

    public void PresentOrderButtonWindow()
    {

        if (progressKey == null) ProgressDirector.I.CurrentProgressKey = progressKey;

        switch (gimmickButtonType)
        {
            case GimickButtonType.textMesh:

                GimmickManager.I.PresentOrderButtonGimmick(gimmickButtonlayout, gimmickButtonText, answer,progressKey, ClearAction, eventActions, scrCloseButtonActions,lockProgressKey);

                break;

            case GimickButtonType.sprite:

                GimmickManager.I.PresentOrderButtonGimmick(gimmickButtonlayout, gimmickButtonSprite, answer, progressKey, ClearAction, eventActions, scrCloseButtonActions,lockProgressKey);

                break;
        }
    }

}

public enum GimmickType
{
    sort,order,itemUse
}

public interface IGimmickButtonContent
{
    // 共通のメソッドやプロパティを定義
}


[Serializable]
public class UseItem
{
    public string needItem;
    public bool DontDeleteItem;
}

[Serializable]
public class GimmickButtonLayout
{
    [SerializeField] public Sprite titleImage;
    [SerializeField] public string titleText;
    [SerializeField] public string titleTerm;
    [SerializeField] public Color titleTextColor;
    [SerializeField] public int[] NumX;
    [SerializeField] public bool hideArrowButton;

    [SerializeField] public bool CustomPadding;
    [SerializeField] public Vector2 padding;

    [SerializeField] public bool IsUseCustomGimmickButton;
    [SerializeField] public GimmickButtonStyle[] gimmickButtonStyles;




    public readonly float defaultSizeX = 150;//140;
    public readonly float defaultSizeY = 150;// 180;
}

[Serializable]
public class GimmickButtonSprite : IGimmickButtonContent
{
    public Sprite sprite;
    public Color color;
    public CustomRectTransForm customRectTransForm;
}


[Serializable]
public class GimmickButtonString : IGimmickButtonContent
{
    public string textString;
    public Color color;
    public CustomRectTransForm customRectTransForm;
}

[Serializable]
public class CustomRectTransForm
{
    public bool ApplyCustomRectTransForm;
    public float left;
    public float top;
    public float right;
    public float bottom;

    public Vector3 eulerAngles;

    public bool DisablePreserveAspect;
}

[Serializable]
public class GimmickButtonStyle
{
    [SerializeField] public float sizeX;
    [SerializeField] public float sizeY;
    [SerializeField] public Vector3 eulerAngles;
    [SerializeField] public Sprite customShape;
    [SerializeField] public Color buttonColor;
}

