using UnityEngine;
using UnityEngine.UI;

public class OpenDeveloperPage : MonoBehaviour
{
    private void Awake()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OpenPage);
    }

    public void OpenPage()
    {
#if UNITY_ANDROID
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Tennpa+Games");
#elif UNITY_IOS
            Application.OpenURL("https://apps.apple.com/developer/your-developer-page/id1494664402");
#else
            Debug.Log("Unsupported platform");
#endif
    }
}