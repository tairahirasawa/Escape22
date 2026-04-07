using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresentScrBtn : MonoBehaviour
{
    public CanvasGroup canvas;
    private Button button;

    public bool showTimerAd;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickBtn);

    }

    private void OnClickBtn()
    {
        canvas.alpha = 1;
        canvas.blocksRaycasts = true;
        canvas.interactable = true;

        if(showTimerAd)
        {
            GoogleAdManager.I.ShowTimerAd();
        }

    }


}
