using System.Collections;
using UnityEngine;

public partial class GameData
{
    public bool IsReviewRequired;
}

public class StoreReview : SingletonMonoBehaviour<StoreReview>
{
    public bool IsPresented;

    public static IEnumerator Request()
    {
#if UNITY_IOS
        UnityEngine.iOS.Device.RequestStoreReview();

#elif UNITY_ANDROID
        var reviewManager = new Google.Play.Review.ReviewManager();
        var requestFlowOperation = reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;

        if (requestFlowOperation.Error != Google.Play.Review.ReviewErrorCode.NoError)
        {
            // エラー処理が必要な場合ここに追加
            Debug.LogError(requestFlowOperation.Error);
            yield break;
        }

        var playReviewInfo = requestFlowOperation.GetResult();
        var launchFlowOperation = reviewManager.LaunchReviewFlow(playReviewInfo);
        yield return launchFlowOperation;

        if (launchFlowOperation.Error != Google.Play.Review.ReviewErrorCode.NoError)
        {
            // エラー処理が必要な場合ここに追加
            Debug.LogError(launchFlowOperation.Error);
            yield break;
        }
#else
        Debug.Log("RequestReview Not supported.");
#endif
        yield break;
    }

    public void PresentStoreReview()
    {
        StartCoroutine(Request());
        IsPresented = true;
    }

}
