using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseUpPanel : SingletonMonoBehaviour<CloseUpPanel>
{
    private CanvasGroup _canvasGroup;
    public Image ItemImage;

    protected override void OnAwake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        UsefulMethod.Hide(_canvasGroup);
    }

    public async UniTask PresentCloseUpPanel(string itemType)
    {
        ItemImage.sprite = DataBase.I.itemDatas.ItemDataList[itemType].ItemSprite;

        UsefulMethod.Present(_canvasGroup);

        await UniTask.Yield();
    }

}
