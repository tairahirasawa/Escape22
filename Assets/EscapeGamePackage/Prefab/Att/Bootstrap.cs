#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif
using UnityEngine;
using UnityEngine.UI;
using System.Collections; // コルーチンを使用するために追加

public class Bootstrap : MonoBehaviour
{
    public Button PresentAttBtn;
    private CanvasGroup canvasGroup;

#if UNITY_IOS
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        PresentAttBtn.onClick.AddListener(PresentAtt);
        CheckAttStatusAndUpdateUI();
    }

    private void Update()
    {
        if (!DataPersistenceManager.I.gameData.IsAtt)
        {
            CheckAttStatusAndUpdateUI();
        }
    }

    private void CheckAttStatusAndUpdateUI()
    {
        var status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

        if (status != ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            DataPersistenceManager.I.gameData.IsAtt = true;
            UsefulMethod.Hide(canvasGroup);
        }
    }

    public void PresentAtt()
    {
        var status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

        if (status == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED && !DataPersistenceManager.I.gameData.IsAtt)
        {
            ATTrackingStatusBinding.RequestAuthorizationTracking();
            StartCoroutine(CheckAttStatusAfterRequest());
        }
        else
        {
            UsefulMethod.Hide(canvasGroup);
        }
    }

    private IEnumerator CheckAttStatusAfterRequest()
    {
        yield return new WaitForSeconds(1); // リクエスト後に1秒間待機

        var status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
        if (status == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
        {
            // ここでGDRPメッセージを表示
            GDRPMessage.I.StartGdpr();
        }

        if (status != ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            DataPersistenceManager.I.gameData.IsAtt = true;
            UsefulMethod.Hide(canvasGroup);
        }
    }
#endif
}
