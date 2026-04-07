using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrCloseBtn : MonoBehaviour
{
    public CanvasGroup canvas;
    private Button button;

    public Action OnCloseScr;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickBtn);

    }

    private void OnClickBtn()
    {
        canvas.alpha = 0;
        canvas.blocksRaycasts = false;
        canvas.interactable = false;

        OnCloseScr?.Invoke();
    }


}
