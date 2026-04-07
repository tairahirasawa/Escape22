using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FindItemObject : MonoBehaviour, IPointerClickHandler
{
    public string type;
    public string findItemObjectKey;

    public bool ApplyAnotherSprite;
    public bool IsManyItem;
    public bool IsNeedZoom;

    public SpriteRenderer spriteRenderer;
    

    private void Start()
    {
        if (!string.IsNullOrEmpty(type) && !ApplyAnotherSprite)
        {
            spriteRenderer.sprite = FindItemManager.I.GetFindItemSprite(type);
        }

        if (FindItemManager.I.IsDoneProgress(findItemObjectKey))
        {
            spriteRenderer.gameObject.SetActive(false);
        }
    }

    public void FindItemCheck()
    {
        if(!FindItemManager.I.IsSetFindItemList(findItemObjectKey))
        {
            Debug.LogError("アイテムがFindItemDataにセットされていません");
        }
    }


    public void GetFindItem()
    {
        ItemManager.I.GetFindItem(type);
        FindItemManager.I.DoneProgress(findItemObjectKey);
        spriteRenderer.gameObject.SetActive(false);

        FindItemManager.I.findItemUIManager.UpdateCount();
        
        if(FindItemManager.I.IsAllCollect())
        {
            FindItemManager.I.AllCollect();
        }

        FindItemHintManager.I.hideHint();

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameDirector.I.IsEndGame) return;
        if (!spriteRenderer.gameObject.activeSelf) return;
        if (IsNeedZoom && !StageSwitcher.I.mapManager.IsZoom) return;
        GetFindItem();
    }
}
