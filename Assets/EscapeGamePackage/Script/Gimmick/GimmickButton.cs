using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;

public class GimmickButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public GimmickType gimmickType;

    public int DebugIndex;
    public int index { get; set; }
    public int orderIndex;
    public List<GameObject> ButtonContents = new List<GameObject>();

    public Button IndexUpButton;
    public Button IndexDownButton;

    public bool Disable;

    public List<ButtonSprite> NumberSprites;

    public Transform ContentsContainer;

    public Image ButtonImage;
    public TextMeshProUGUI ButtonTextMesh;

    public LayoutElement layoutElement;

    private Vector3 originScale;

    public EventAction extraAction;

    private void Awake()
    {
        IndexUpButton.onClick.AddListener(OnClickIndexUpButton);
        IndexDownButton.onClick.AddListener(OnClickIndexDownButton);

        GetIndex();
    }

    private void Update()
    {
        DebugIndex = index;

        if(gimmickType == GimmickType.sort)
        {
            for (int i = 0; i < ButtonContents.Count; i++)
            {
                if (i == index)
                {
                    ButtonContents[i].SetActive(true);

                }
                else
                {
                    ButtonContents[i].SetActive(false);
                }
            }
        }

    }


    public void SetSortButtonTextMesh(GimmickButtonString[] texts,GimmickButtonLayout layout,EventAction extraAction)
    {
        IndexUpButton.gameObject.SetActive(!layout.hideArrowButton);
        IndexDownButton.gameObject.SetActive(!layout.hideArrowButton);

        ButtonContents.Clear();

        for (int i = 0; i < texts.Length; i++)
        {
            var buttonText = Instantiate(ButtonTextMesh, ContentsContainer);
            buttonText.text = texts[i].textString;
            buttonText.color = texts[i].color;

            this.extraAction = extraAction;
            if (extraAction != null) extraAction.Initialize(null);

            SetRectTransform(buttonText.GetComponent<RectTransform>(), texts[i].customRectTransForm);
            ButtonContents.Add(buttonText.gameObject);
        }
    }

    public void SetSortButtonSprite(GimmickButtonSprite[] sprites, GimmickButtonLayout layout, EventAction extraAction)
    {
        IndexUpButton.gameObject.SetActive(!layout.hideArrowButton);
        IndexDownButton.gameObject.SetActive(!layout.hideArrowButton);

        ButtonContents.Clear();

        for (int i = 0; i < sprites.Length; i++)
        {
            var buttonSprite = Instantiate(ButtonImage, ContentsContainer);
            buttonSprite.sprite = sprites[i].sprite;
            buttonSprite.color = sprites[i].color;

            this.extraAction = extraAction;
            if (extraAction != null) extraAction.Initialize(null);

            SetPreserveAspect(buttonSprite, sprites[i].customRectTransForm);
            SetRectTransform(buttonSprite.GetComponent<RectTransform>(), sprites[i].customRectTransForm);

            ButtonContents.Add(buttonSprite.gameObject);
        }
    }

    public void SetOrderButtonTextMesh(GimmickButtonString text,int index, EventAction extraAction)
    {

        IndexUpButton.gameObject.SetActive(false);
        IndexDownButton.gameObject.SetActive(false);

        ButtonContents.Clear();

        var buttonText = Instantiate(ButtonTextMesh, ContentsContainer);
        buttonText.text = text.textString;
        buttonText.color = text.color;

        this.extraAction = extraAction;
        if (extraAction != null) extraAction.Initialize(null);

        SetRectTransform(buttonText.GetComponent<RectTransform>(), text.customRectTransForm);


        orderIndex = index;

        ButtonContents.Add(buttonText.gameObject);
    }

    public void SetOrderButtonSprite(GimmickButtonSprite spriteAndColor, int index, EventAction extraAction)
    {
        IndexUpButton.gameObject.SetActive(false);
        IndexDownButton.gameObject.SetActive(false);

        ButtonContents.Clear();

        var buttonSprite = Instantiate(ButtonImage, ContentsContainer);
        buttonSprite.sprite = spriteAndColor.sprite;
        buttonSprite.color = spriteAndColor.color;

        this.extraAction = extraAction;
        if (extraAction != null) extraAction.Initialize(null);

        SetPreserveAspect(buttonSprite, spriteAndColor.customRectTransForm);
        SetRectTransform(buttonSprite.GetComponent<RectTransform>(),spriteAndColor.customRectTransForm);

        orderIndex = index;

        ButtonContents.Add(buttonSprite.gameObject);
    }

    public void SetRectTransform(RectTransform target,CustomRectTransForm custom)
    {
        if (!custom.ApplyCustomRectTransForm) return;

        // LeftとBottomのオフセットを設定
        target.offsetMin = new Vector2(custom.left , custom.bottom);

        // RightとTopのオフセットを設定 (負の値に注意)
        target.offsetMax = new Vector2(-custom.right, -custom.top);

        target.localEulerAngles = custom.eulerAngles;
    }

    public void SetPreserveAspect(Image image, CustomRectTransForm custom)
    {
        if (!custom.ApplyCustomRectTransForm)
        {
            image.preserveAspect = true;
        }
        else
        {
            image.preserveAspect = !custom.DisablePreserveAspect;
        }
    }


    private void GetIndex()
    {
        for (int i = 0; i < ButtonContents.Count; i++)
        {
            if (ButtonContents[i].activeSelf)
            {
                index = i;
            }
        }
    }

    public async void OnPointerDown(PointerEventData eventData)
    {

        if (Disable) return;

        DisablePanel.I.ForceDisable();

        switch (gimmickType)
        {
            case GimmickType.sort:

                index++;

                if (index >= ButtonContents.Count)
                {
                    index = 0;
                }

                break;

            case GimmickType.order:

                GimmickManager.I.orderButtonIndex.Add(orderIndex);

                originScale = transform.localScale;
                transform.localScale = transform.localScale * 0.9f;

                break;
        }


        AudioDirector.I.PlaySE(AudioDirector.SeType.gimmick);
        if (extraAction != null) await extraAction.selectAction.Act();
        DisablePanel.I.ExitForce();
        DisablePanel.I.Enable();
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        if (Disable) return;

        switch (gimmickType)
        {
            case GimmickType.order:

                transform.localScale = originScale;

                break;
        }


    }

    public void OnClickIndexUpButton()
    {
        index++;
        AudioDirector.I.PlaySE(AudioDirector.SeType.gimmick);
        if (index >= ButtonContents.Count)
        {
            index = 0;
        }

    }

    public void OnClickIndexDownButton()
    {
        index--;
        AudioDirector.I.PlaySE(AudioDirector.SeType.gimmick);
        if (index < 0)
        {
            index = ButtonContents.Count -1;
        }

    }
}

