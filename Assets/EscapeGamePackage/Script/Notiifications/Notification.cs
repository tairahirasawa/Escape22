using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Messaging;


#if UNITY_ANDROID
using Unity.Notifications.Android;
using UnityEngine.Android;
#endif

#if UNITY_IOS 
using Unity.Notifications.iOS;
using UnityEngine.iOS;
#endif

public class Notification : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        #if UNITY_IOS //iOS用の実装
        iOSNotificationCenter.RemoveAllScheduledNotifications();
        iOSNotificationCenter.RemoveAllDeliveredNotifications();
        iOSNotificationCenter.ApplicationBadge = 0;

#elif UNITY_ANDROID //Android用の実装
        AndroidNotificationCenter.CancelAllScheduledNotifications();
        AndroidNotificationCenter.CancelAllNotifications();
#endif

        // 登録トークンの登録 & 受信処理
        Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
        // プッシュ通知の受信処理
        Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;

#if UNITY_ANDROID
        // サーバープッシュの受信権限がない場合、リクエストする
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }
#endif
    }

    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
    }

    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        UnityEngine.Debug.Log("Received a new message from: " + e.Message.From);
    }


}


