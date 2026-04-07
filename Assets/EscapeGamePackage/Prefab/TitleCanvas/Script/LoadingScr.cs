using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScr : SingletonMonoBehaviour<LoadingScr>
{
    private CanvasGroup canvasGroup;

    protected override void OnAwake()
    {
        base.OnAwake();

        canvasGroup = GetComponent<CanvasGroup>();

    }

    public void PresentLoadingScr() => UsefulMethod.Present(canvasGroup);
    public void HideLoadingScr() => UsefulMethod.Hide(canvasGroup);

}
