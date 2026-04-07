using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpScr : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public Button closeButton;

    public bool IsPresetnScr;

    private void Awake()
    {
        closeButton.onClick.AddListener(HideHelpScr);
        canvasGroup = GetComponent<CanvasGroup>();
        HideHelpScr();
    }

    public void PresentHelpScr()
    {
        UsefulMethod.Present(canvasGroup);
        IsPresetnScr = true;
    }
    public void HideHelpScr()
    {
        UsefulMethod.Hide(canvasGroup);
        IsPresetnScr = false;
    }

}
