using Cysharp.Threading.Tasks;
using I2.Loc;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GimmickManager : SingletonMonoBehaviour<GimmickManager>
{
    public GimmickType gimmickType;
    public CanvasGroup canvasGroup;
    public GameObject bg;
    public GimmickButton changeBtnPrefab;
    public Transform virticalLayout;

    public Dictionary<TargetKey, List<int>> sortgimmickSaves = new Dictionary<TargetKey, List<int>>();
    public List<GameObject> holizeontalGroups = new List<GameObject>();
    public List<GimmickButton> gimmickButtons = new List<GimmickButton>();

    public int[] answerIndex;
    public List<int> orderButtonIndex = new List<int>();

    public Button scrCloseButton;
    public EventAction[] scrCloseButtonActions;

    public bool IsPresentGimmickScr;

    [HideInInspector] public TargetKey targetProgressKey;
    [HideInInspector] public TargetKey lockProgressKey;

    public Func<UniTask> ClearAction;

    public Image titleImage;
    public TextMeshProUGUI titleText;
    public Localize localize;

    private Vector2 defaultPadding = new Vector2(30, 0);

    public bool StopGimmick;
    private bool IsHided;

    protected override void OnAwake()
    {
        scrCloseButton.onClick.AddListener(OnClickBackBtn);
    }

    public GameObject CreateHorizontalLayoutGroup(GimmickButtonLayout gimmickButtonLayout)
    {
        GameObject newObject = new GameObject("HorizontalLayoutObject");

        // Horizontal Layout Groupコンポーネントを追加
        HorizontalLayoutGroup layoutGroup = newObject.AddComponent<HorizontalLayoutGroup>();

        // Horizontal Layout Groupの設定を行う
        layoutGroup.childControlWidth = true;
        layoutGroup.childControlHeight = true;
        layoutGroup.childForceExpandWidth = false;
        layoutGroup.childForceExpandHeight = false;

        if (gimmickButtonLayout.CustomPadding)
        {
            layoutGroup.spacing = gimmickButtonLayout.padding.x;
        }
        else
        {
            layoutGroup.spacing = defaultPadding.x;
        }

        // Paddingの設定
        layoutGroup.padding = new RectOffset(40, 40, 20, 20); // 左, 右, 上, 下

        // Child Alignmentの設定
        layoutGroup.childAlignment = TextAnchor.MiddleCenter;
        return newObject;

    }

    public void GenerateButtons<T>(GimmickButtonLayout gimmickButtonLayout, T[] contents,GimmickType gimmickType, EventAction[] eventActions)
    {
        if(gimmickButtonLayout.titleImage != null)
        {
            titleImage.sprite = gimmickButtonLayout.titleImage;
            titleImage.gameObject.SetActive(true);
        }
        else
        {
            titleImage.gameObject.SetActive(false);
        }

        if(gimmickButtonLayout.titleText != null && gimmickButtonLayout.titleText !="")
        {
            localize.enabled = false;
            titleText.gameObject.SetActive(true);
            titleText.text = gimmickButtonLayout.titleText;
            titleText.color = gimmickButtonLayout.titleTextColor;
        }
        else
        {
            titleText.gameObject.SetActive(false);
        }

        if (gimmickButtonLayout.titleTerm != null && gimmickButtonLayout.titleTerm != "")
        {
            localize.enabled = true;
            localize.gameObject.SetActive(true);
            localize.Term = gimmickButtonLayout.titleTerm;
            titleText.color = gimmickButtonLayout.titleTextColor;
        }
        else
        {
            //localize.gameObject.SetActive(false);
        }

        if (gimmickButtonLayout.CustomPadding)
        {
            virticalLayout.gameObject.GetComponent<VerticalLayoutGroup>().spacing = gimmickButtonLayout.padding.y;
        }
        else
        {
            virticalLayout.gameObject.GetComponent<VerticalLayoutGroup>().spacing = defaultPadding.y;

        }


        var index = 0;


        for (int y = 0; y < gimmickButtonLayout.NumX.Length; y++)
        {
            //3,1
            var holizeontalGroup = CreateHorizontalLayoutGroup(gimmickButtonLayout);
            holizeontalGroup.transform.SetParent(virticalLayout);
            holizeontalGroup.transform.localScale = Vector3.one;
            holizeontalGroups.Add(holizeontalGroup);



            for (int x = 0; x < gimmickButtonLayout.NumX[y]; x++)
            {
                //2,1,2

                var button = Instantiate(changeBtnPrefab, holizeontalGroup.transform);
                button.gimmickType = gimmickType;

                EventAction eventAction = null;

                if(eventActions != null && eventActions.Length >= index + 1)
                {
                    eventAction = eventActions[index];
                }

                switch (gimmickType)
                {
                    case GimmickType.sort:

                        if (typeof(T) == typeof(GimmickButtonString))
                        {
                            button.SetSortButtonTextMesh(contents as GimmickButtonString[], gimmickButtonLayout, eventAction);
                        }
                        else if (typeof(T) == typeof(GimmickButtonSprite))
                        {
                            button.SetSortButtonSprite(contents as GimmickButtonSprite[], gimmickButtonLayout, eventAction);
                        }

                        break;

                    case GimmickType.order:

                        if (typeof(T) == typeof(GimmickButtonString))
                        {
                            button.SetOrderButtonTextMesh(contents[index] as GimmickButtonString,index, eventAction);
                        }
                        else if (typeof(T) == typeof(GimmickButtonSprite))
                        {
                            button.SetOrderButtonSprite(contents[index] as GimmickButtonSprite, index, eventAction);
                        }

                        break;
                }

                if (!gimmickButtonLayout.IsUseCustomGimmickButton)
                {
                    button.layoutElement.preferredWidth = gimmickButtonLayout.defaultSizeX;
                    button.layoutElement.preferredHeight = gimmickButtonLayout.defaultSizeY;
                }
                else
                {
                    if (gimmickButtonLayout.gimmickButtonStyles[index].sizeX == 0 && gimmickButtonLayout.gimmickButtonStyles[index].sizeY == 0)
                    {
                        button.layoutElement.preferredWidth = gimmickButtonLayout.defaultSizeX;
                        button.layoutElement.preferredHeight = gimmickButtonLayout.defaultSizeY;
                    }
                    else
                    {
                        button.layoutElement.preferredWidth = gimmickButtonLayout.gimmickButtonStyles[index].sizeX;
                        button.layoutElement.preferredHeight = gimmickButtonLayout.gimmickButtonStyles[index].sizeY;
                    }

                    if (gimmickButtonLayout.gimmickButtonStyles[index].customShape != null)
                    {
                        button.ContentsContainer.GetComponent<Image>().sprite = gimmickButtonLayout.gimmickButtonStyles[index].customShape;
                    }

                    button.transform.localEulerAngles = gimmickButtonLayout.gimmickButtonStyles[index].eulerAngles;
                    button.ContentsContainer.GetComponent<Image>().color = gimmickButtonLayout.gimmickButtonStyles[index].buttonColor;
                }

                gimmickButtons.Add(button);

                index++;
            }
        }
    }

    public void InitializeButtonSetting()
    {
        for (int i = 0; i < gimmickButtons.Count; i++)
        {
            if(gimmickButtons[i] != null)
            Destroy(gimmickButtons[i]?.gameObject);
        }

        for (int i = 0; i < holizeontalGroups.Count; i++)
        {
            if (holizeontalGroups[i] != null)
            Destroy(holizeontalGroups[i]);
        }

        holizeontalGroups.Clear();
        gimmickButtons.Clear();
        orderButtonIndex.Clear();

    }


    public async void PresentSortButtonGimmick<T>(GimmickButtonLayout gimmickButtonLayout, T[] contents, int[] answer, TargetKey progressKey , Func<UniTask> clearAction, EventAction[] eventActions = null, EventAction[] scrCloseActions = null,TargetKey lockProgressKey = null)
    {
        var isSetUp = false;

        gimmickType = GimmickType.sort;
        InitializeButtonSetting();

        GenerateButtons(gimmickButtonLayout, contents,GimmickType.sort,eventActions);
        
        if (sortgimmickSaves.ContainsKey(progressKey))
        {
            for (int i = 0; i < gimmickButtons.Count; i++)
            {
                gimmickButtons[i].index = sortgimmickSaves[progressKey][i];
            }
        }
        
        answerIndex = answer;
        targetProgressKey = progressKey;
        this.lockProgressKey = lockProgressKey;
        ClearAction = clearAction;

        isSetUp = true;


        this.scrCloseButtonActions = scrCloseActions;

        if (scrCloseButtonActions != null)
        {
            for (int i = 0; i < scrCloseButtonActions.Length; i++)
            {
                scrCloseButtonActions[i].Initialize(null);
            }
        }


        await UniTask.WaitUntil(()=>isSetUp == true);
        UsefulMethod.Present(canvasGroup);

         IsHided = false;
    }

    public async void PresentOrderButtonGimmick<T>(GimmickButtonLayout gimmickButtonLayout, T[] contents, int[] answer, TargetKey progressKey, Func<UniTask> clearAction, EventAction[] eventActions = null, EventAction[] scrCloseActions = null,TargetKey lockProgressKey = null)
    {
        var isSetUp = false;

        gimmickType = GimmickType.order;

        InitializeButtonSetting();
        GenerateButtons(gimmickButtonLayout, contents, GimmickType.order,eventActions);

        answerIndex = answer;
        targetProgressKey = progressKey;
        this.lockProgressKey = lockProgressKey;
        ClearAction = clearAction;

        isSetUp = true;

        this.scrCloseButtonActions = scrCloseActions;

        if (scrCloseButtonActions != null)
        {
            for (int i = 0; i < scrCloseButtonActions.Length; i++)
            {
                scrCloseButtonActions[i].Initialize(null);
            }
        }

        await UniTask.WaitUntil(() => isSetUp == true);

        UsefulMethod.Present(canvasGroup);

        IsHided = false;
    }

    private void SaveSortGimmickData()
    {
        if (!ProgressDirector.I.IsDoneProgress(lockProgressKey) && lockProgressKey.episodeType != StageContext.StageType.none) return;

        if (gimmickType == GimmickType.sort)
        {
            var indexList = new List<int>();

            for (int i = 0; i < gimmickButtons.Count; i++)
            {
                indexList.Add(gimmickButtons[i].index);
            }

            if (!sortgimmickSaves.ContainsKey(targetProgressKey))
            {
                sortgimmickSaves.Add(targetProgressKey, indexList);
            }
            else
            {
                sortgimmickSaves[targetProgressKey] = indexList;
            }
        }

    }

    private void HideGimmickScr()
    {
        if(IsHided) return;

        UsefulMethod.Hide(canvasGroup);
        SaveSortGimmickData();

        for (int i = 0; i < gimmickButtons.Count; i++)
        {
            Destroy(gimmickButtons[i].gameObject);
        }

        for (int i = 0; i < holizeontalGroups.Count; i++)
        {
            Destroy(holizeontalGroups[i]);
        }

        IsHided = true;
    }

    private async void Update()
    {
        IsPresentGimmickScr = canvasGroup.alpha == 1;

        if (lockProgressKey.episodeType != StageContext.StageType.none && !ProgressDirector.I.IsDoneProgress(lockProgressKey)) return;

        if (IsPresentGimmickScr && !ProgressDirector.I.IsDoneProgress(targetProgressKey))
        {
            switch(gimmickType)
            {
                case GimmickType.sort:

                    if (!SortButtonJudge()) return;

                    break;

                case GimmickType.order:

                    if (!OrderButtonJudge()) return;
                    
                    break;

            }

            StopGimmick = true;
            ProgressDirector.I.DoneProgress(targetProgressKey);

            await UniTask.Delay(1000);

            HideGimmickScr();

            if(ClearAction != null)
            {

                await ClearAction();
            }

            StopGimmick = false;
        }
    }

    public bool SortButtonJudge()
    {
        for (int i = 0; i < gimmickButtons.Count; i++)
        {
            if (gimmickButtons[i].index != answerIndex[i])
            {
                return false;
            }
        }

        return true;
    }

    public bool OrderButtonJudge()
    {
        if (answerIndex.Length == orderButtonIndex.Count)
        {
            for (int i = 0; i < answerIndex.Length; i++)
            {
                if (answerIndex[i] != orderButtonIndex[i])
                {
                    //orderButtonIndex.Clear();
                    return false;
                }
            }

            return true;
        }
        else
        {
            return false;
        }
    }



    public async void OnClickBackBtn()
    {
        HideGimmickScr();

        if(scrCloseButtonActions != null)
        {
            for (int i = 0; i < scrCloseButtonActions.Length; i++)
            {
                await scrCloseButtonActions[i].selectAction.Act();
            }
        }

    }
}
