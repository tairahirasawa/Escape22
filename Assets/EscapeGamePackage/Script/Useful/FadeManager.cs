using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class FadeManager: SingletonMonoBehaviour<FadeManager>
{
    private CanvasGroup canvasGroup;
    private Image image;

    protected override void OnAwake()
    {
        base.OnAwake();
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();

        UsefulMethod.Hide(canvasGroup);
    }

    public void FadeReset()
    {
        canvasGroup.DOFade(0, 0);
    }


    public void FadeIn(float time,Color color)
    {
        image.color = color;
        canvasGroup.DOFade(1, time);
    }

    public void FadeOut(float time, Color color)
    {
        image.color = color;
        canvasGroup.DOFade(0, time);
    }

    public void FadeInOut(float time, Color color)
    {
        StartCoroutine(IeFadeInFadeOut(time, color));
    }

    IEnumerator IeFadeInFadeOut(float time,Color color)
    {
        FadeIn(time, color);

        yield return  new WaitForSeconds(time);

        FadeOut(time, color);
    }

}
