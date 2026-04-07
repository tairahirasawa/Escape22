using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine.UI;  // DOTweenを使うために必要

public class GachGachController : MonoBehaviour
{
    public Button gachaButton;
    public GameObject gachadama_appear;
    public GameObject handle;

    public GameObject result;
    public GameObject gachadama_before;
    public GameObject gachadama_after;
    public GameObject resultSeel;

    private void Awake()
    {
        Reset();
        gachaButton.onClick.AddListener(async () => await OnClickGachabutton());
    }

    public async Task OnClickGachabutton()
    {
        await PlayGacha();
    }

    public async Task PlayGacha()
    {
        Reset();
        gachaButton.gameObject.SetActive(false);

        // gachadamaを非表示にしておく
        handle.transform.DORotate(new Vector3(0, 0, -360f), 1.0f, RotateMode.FastBeyond360)
        .SetEase(Ease.Linear);

        await UniTask.Delay(1500);

        gachadama_appear.SetActive(true);

        await UniTask.Delay(1000);

        result.SetActive(true);
        gachadama_before.SetActive(true);

        await UniTask.Delay(1000);

        gachadama_before.SetActive(false);
        gachadama_after.SetActive(true);

        AppearRouletResult_Pop();
        gachaButton.gameObject.SetActive(true);
        gachadama_appear.SetActive(false);


    }

    public void AppearRouletResult_Pop()
    {
        resultSeel.SetActive(true);

        resultSeel.transform.localScale = Vector3.zero;

        // Sequenceで2段階の動きを滑らかに連結
        Sequence seq = DOTween.Sequence();
        seq.Append(resultSeel.transform.DOScale(Vector3.one * 1.4f, 0.25f).SetEase(Ease.OutCubic)); // 膨らむ
        seq.Append(resultSeel.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBack));         // 戻る
    }

    public void Reset()
    {
        result.SetActive(false);
        resultSeel.SetActive(false);
        gachadama_before.SetActive(false);
        gachadama_after.SetActive(false);
        gachadama_appear.SetActive(false);
    }
}