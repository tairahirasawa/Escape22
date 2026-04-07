using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using I2.Loc;
using TMPro;

public class HintManager : SingletonMonoBehaviour<HintManager>
{
    public Button hint1Btn,AnswerBtn;
    public TextMeshProUGUI hit1Text,AnswerText;
    private CanvasGroup hintBtn2Canvas, AnswerBtnCanvas;

    public TextMeshProUGUI hintText;

    public ScrCloseBtn scrCloseBtn;

    public Localize HintTextLocalize, hintBtnLocalize, answerBtnLocalize,debugText,answerDebugText;



    private void Awake()
    {
        HideHint();
        AnswerBtnCanvas = AnswerBtn.GetComponent<CanvasGroup>();

        hint1Btn.onClick.AddListener(() => OnClickHintBtn(1));
        AnswerBtn.onClick.AddListener(() => OnClickHintBtn(2));

        scrCloseBtn.OnCloseScr = HideHint;

    }

    private void Start()
    {

        //Debug.Log(LocalizationManager.GetTermData("hint/h00").GetTranslation(0));
        

    }

    private void Update()
    {
        SetBtnText();
        BtnPresent();


        debugText.Term = GetHintID();
        answerDebugText.Term = GetAnswerTerm();
    }

    public void OnClickHintBtn(int hintnum)
    {
        if (!ProgressDirector.I.ShowedHintAnswer(hintnum) && !DataPersistenceManager.I.gameData.blockAd)
        {
            GoogleAdManager.I.googleAD_Rewerd.ShowRewardedAd(()=>ShowHint(hintnum));
        }
        else
        {
            ShowHint(hintnum);
        }
    }

    public string GetHintID()
    {
        if (ProgressDirector.I.NowProgressKey == null) return null;
        return StageContext.GetCurrentPrefixWithSeparator() + "hint/" + ProgressDirector.I.NowProgressKey.saveKey;
    }

    public string GetAnswerTerm()
    {
        if (ProgressDirector.I.NowProgressKey == null) return null;
        return StageContext.GetCurrentPrefixWithSeparator() + "answer/" + ProgressDirector.I.NowProgressKey.saveKey;
    }

    public bool IsHintExist()
    {
        if (LocalizationManager.GetTermData(GetHintID()) != null)
        {
            if (LocalizationManager.GetTermData(GetHintID()).GetTranslation(1).Length <= 5)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }

    }

    public void ShowHint(int hintnum)
    {
        if (hintnum == 1)
        {
            HintTextLocalize.Term = GetHintID();
        }
        else
        {
            HintTextLocalize.Term = GetAnswerTerm();
        }


        hintText.gameObject.SetActive(true);


        ProgressDirector.I.DoneHint(hintnum);
    }


    public void HideHint()
    {
        hintText.gameObject.SetActive(false);
    }

    public void SetBtnText()
    {
        if (!ProgressDirector.I.ShowedHintAnswer(1) && !DataPersistenceManager.I.gameData.blockAd)
        {
            hintBtnLocalize.Term = "hintBtn";
        }
        else
        {
            hintBtnLocalize.Term = "hintBtn2";
        }

        if (!ProgressDirector.I.ShowedHintAnswer(2) && !DataPersistenceManager.I.gameData.blockAd)
        {
            answerBtnLocalize.Term = "answerBtn";
        }
        else
        {
            answerBtnLocalize.Term = "answerBtn2";
        }

    }


    public void BtnPresent()
    {
        if(!IsHintExist())
        {
            hint1Btn.interactable = false;
        }
        else
        {
            hint1Btn.interactable = true;
        }

        if (ProgressDirector.I.ShowedHintAnswer(1) || !IsHintExist())
        {
            UsefulMethod.Present(AnswerBtnCanvas);
        }
        else
        {
            UsefulMethod.Hide(AnswerBtnCanvas);
        }
    }


}
