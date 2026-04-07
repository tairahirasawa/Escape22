using UnityEngine;
using UnityEngine.UI;

public class StoreLinkOpener : MonoBehaviour
{
    // ストアURLをInspectorで指定できるようにする
    [SerializeField] private string androidUrl = "https://play.google.com/store/apps/details?id=com.example.app";
    [SerializeField] private string iosUrl = "https://apps.apple.com/app/id1234567890";

    public bool DisableIOS;
    public bool DisableAndroid;

    private void Awake()
    {
#if UNITY_ANDROID
        if (DisableAndroid) gameObject.SetActive(false);
#elif UNITY_IOS
        if (DisableIOS) gameObject.SetActive(false);
#else
        // エディタなど
        Debug.Log("現在のプラットフォームではストアを開けません。");
#endif
    }

    public void OpenStore()
    {
#if UNITY_ANDROID
        Application.OpenURL(androidUrl);
#elif UNITY_IOS
        Application.OpenURL(iosUrl);
#else
        // エディタなど
        Debug.Log("現在のプラットフォームではストアを開けません。");
#endif
    }
}
