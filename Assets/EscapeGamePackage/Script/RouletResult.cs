using TMPro;
using UnityEngine;
using DG.Tweening;

public class RouletResult : MonoBehaviour
{
    public GameObject contents;
    public GameObject panel;
    public TextMeshProUGUI textMesh;

    private void Start()
    {
        contents.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AppearRouletResult_Pop("test");
        }
    }

    public void AppearRouletResult_Bouncy(string text)
    {
        textMesh.text = text;
        contents.SetActive(true);

        // 初期状態をスケール0に
        panel.transform.localScale = Vector3.zero;

        // ぷるんぷるん弾むアニメーション
        panel.transform
            .DOScale(Vector3.one, 0.6f)   // 0.6秒かけて通常サイズへ
            .SetEase(Ease.OutElastic);    // 弾むイージング
    }

    public void AppearRouletResult_Pop(string text)
    {
        textMesh.text = text;
        contents.SetActive(true);

        panel.transform.localScale = Vector3.zero;

        // Sequenceで2段階の動きを滑らかに連結
        Sequence seq = DOTween.Sequence();
        seq.Append(panel.transform.DOScale(Vector3.one * 1.4f, 0.25f).SetEase(Ease.OutCubic)); // 膨らむ
        seq.Append(panel.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBack));         // 戻る
    }
}
