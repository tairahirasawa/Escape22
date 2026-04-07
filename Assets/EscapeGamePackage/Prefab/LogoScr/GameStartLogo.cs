using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class GameStartLogo : SingletonMonoBehaviour<GameStartLogo>
{
    public CanvasGroup canvasGroup;
    public Image logo;

    public static bool IsEndLogo;

    protected override void OnAwake()
    {
        base.OnAwake();
        UsefulMethod.Hide(canvasGroup);

    }

    public async void PresentLogo(int starWait = 1, int stayTime = 2)
    {
        if (IsEndLogo) return;

        logo.DOFade(0, 0);
        UsefulMethod.Present(canvasGroup);

        await UniTask.Delay(TimeSpan.FromSeconds(starWait));

        logo.DOFade(1, 1);

        await UniTask.Delay(TimeSpan.FromSeconds(stayTime));

        canvasGroup.DOFade(0, 1).OnComplete(() =>
            {
                IsEndLogo = true;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            });
    }

    public async UniTask WaitForEndLogoAsync()
    {
        while (!IsEndLogo)
        {
            await UniTask.Delay(100); // 適切な待機時間を設定してください
        }
    }
}
