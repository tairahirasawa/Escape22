using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadTextureFromURL : MonoBehaviour
{
    public string imageUrl = "http://tsgamedev.com/wp-content/uploads/2024/05/IconRoll.png"; // ここに取得したい画像のURLを設定します
    public RawImage targetRawImage; // テクスチャを適用する対象のRawImageを設定します

    void Start()
    {
        // Coroutineを開始して画像をダウンロード
        StartCoroutine(DownloadImage(imageUrl));
    }

    IEnumerator DownloadImage(string url)
    {
        // UnityWebRequestを使用して画像をダウンロード
        using (UnityEngine.Networking.UnityWebRequest uwr = UnityEngine.Networking.UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityEngine.Networking.UnityWebRequest.Result.ConnectionError || uwr.result == UnityEngine.Networking.UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Failed to download image: " + uwr.error);
            }
            else
            {
                // 取得したテクスチャをRawImageのテクスチャに設定
                Texture2D downloadedTexture = UnityEngine.Networking.DownloadHandlerTexture.GetContent(uwr);
                downloadedTexture.wrapMode = TextureWrapMode.Repeat;
                targetRawImage.texture = downloadedTexture;

                // もし必要なら、RectTransformのサイズをテクスチャサイズに合わせる
                RectTransform rectTransform = targetRawImage.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(downloadedTexture.width, downloadedTexture.height);
            }
        }
    }
}