using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class RouletController : MonoBehaviour
{
    public GameObject roulet;
    public RouletResult rouletResult;
    private Tween rotateTween;
    private bool isSpinning = false;
    public Button spinButton;

    private void Awake()
    {
        spinButton.onClick.AddListener(SpinAndStopRoulet);
    }

    public async void SpinAndStopRoulet()
    {
        spinButton.gameObject.SetActive(false);
        StartRoulet();
        await UniTask.Delay(2000);
        StopRoulet();
    }

    // ルーレットを回し始める
    public void StartRoulet()
    {
        if (isSpinning) return;
        isSpinning = true;

        // 速めに回す（0.3秒で1回転）
        rotateTween = roulet.transform
            .DORotate(new Vector3(0, 0, -360), 0.3f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1);
    }

    // ルーレットをランダムな位置で止める
    public void StopRoulet()
    {
        if (!isSpinning) return;
        isSpinning = false;

        // ランダムで止まる角度（0〜360度）
        float randomAngle = Random.Range(0f, 360f);

        // 数回転分を足した「絶対角度」
        float targetZ = -(360f * Random.Range(3, 6) + randomAngle);

        rotateTween.OnKill(() =>
        {
            roulet.transform
                .DORotate(new Vector3(0, 0, targetZ), 3f, RotateMode.FastBeyond360)
                .SetEase(Ease.OutCubic)
                .OnComplete(() =>
                {
                    CheckResult();
                    spinButton.gameObject.SetActive(true);
                });
        });

        rotateTween.Kill();
    }

    // ルーレットの結果判定（8分割）
    private void CheckResult()
    {
        float z = roulet.transform.eulerAngles.z % 360f;
        if (z < 0) z += 360f;

        // 時計回りに回してるので、角度を反転
        float normalized = (360f - z) % 360f;

        // 1区間 = 45度（8分割）
        int index = Mathf.FloorToInt(normalized / 45f);

        Debug.Log($"ルーレット結果: {index}番 (角度={normalized:F1}°)");

        // switch文で分岐
        switch (index)
        {
            case 0:
                ShowRouletResult_Coin(10);
                break;
            case 1:
                ShowRouletResult_Coin(10);
                break;
            case 2:
                ShowRouletResult_Coin(20);
                break;
            case 3:
                ShowRouletResult_Coin(10);
                break;
            case 4:
                ShowRouletResult_Coin(30);
                break;
            case 5:
                ShowRouletResult_Coin(10);
                break;
            case 6:
                ShowRouletResult_Coin(20);
                break;
            case 7:
                ShowRouletResult_Coin(10);
                break;
        }
    }

    public void ShowRouletResult_Coin(int coinNum)
    {
        rouletResult.AppearRouletResult_Pop("x" + coinNum.ToString());
        Coin.AddCoin(coinNum);
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartRoulet();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            StopRoulet();
        }
        */
    }
}
