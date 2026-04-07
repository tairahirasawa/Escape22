using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using I2.Loc;

public class CautionScr : SingletonMonoBehaviour<CautionScr>
{
    private CanvasGroup canvasGroup;

    public TextMeshProUGUI cautionText;
    public Localize caustionLocalize;

    public Button yesBtn;
    public Button noBtn;

    protected override void OnAwake()
    {
        base.OnAwake();
        canvasGroup = GetComponent<CanvasGroup>();


        HideCautionScr();
        noBtn.onClick.AddListener(OnClickNoBtn);

    }

    public void PresentCautionScrLocalize(string term, Action action)
    {
        caustionLocalize.Term = term;

        yesBtn.gameObject.SetActive(true);
        noBtn.gameObject.SetActive(true);


        UsefulMethod.Present(canvasGroup);
        yesBtn.onClick.AddListener(() => action());
    }

    public void PresentCautionScrLocalizeForReward(string term)
    {
        caustionLocalize.Term = term;

        yesBtn.gameObject.SetActive(false);
        noBtn.gameObject.SetActive(false);

        UsefulMethod.Present(canvasGroup);

    }


    public void PresentCautionScr(string text , Action action)
    {
        cautionText.text = text;


        yesBtn.gameObject.SetActive(true);
        noBtn.gameObject.SetActive(true);


        UsefulMethod.Present(canvasGroup);
        yesBtn.onClick.AddListener(()=>action());
    }

    public void PresentCautionScr2(string text)
    {
        cautionText.text = text;

        yesBtn.gameObject.SetActive(false);
        noBtn.gameObject.SetActive(false);

        UsefulMethod.Present(canvasGroup);
    }


    public void HideCautionScr() => UsefulMethod.Hide(canvasGroup);

    public void OnClickNoBtn()
    {
        HideCautionScr();
    }

}
