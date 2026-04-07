using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CollectionItemObject : MonoBehaviour, IPointerClickHandler
{
    public string type;
    public TargetKey progressKey;

    public bool ApplyAnotherSprite;
    public bool IsManyItem;
    public bool IsNeedZoom;

    public SpriteRenderer spriteRenderer;
    public List<GameObject> chaneObject;

    private void Start()
    {
        if (!string.IsNullOrEmpty(type) && !ApplyAnotherSprite)
        {
            spriteRenderer.sprite = DataBase.I.itemDatas.CollectionItemDataList[type].ItemSprite;
        }

        if (ProgressDirector.I.IsDoneProgress(progressKey))
        {
            for (int i = 0; i < chaneObject.Count; i++)
            {
                chaneObject[i].SetActive(false);
            }

            spriteRenderer.gameObject.SetActive(false);

        }

    }

    public void GetCollectionItem()
    {
        if (progressKey.episodeType == StageContext.StageType.none) ProgressDirector.I.CurrentProgressKey = progressKey;

        ItemManager.I.GetCollectionItem(type);

        for (int i = 0; i < chaneObject.Count; i++)
        {
            chaneObject[i].SetActive(false);
        }

        ProgressDirector.I.DoneProgress(progressKey);
        spriteRenderer.gameObject.SetActive(false);

        ItemManager.I.UpdateCurrentCollectionItemCount();

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameDirector.I.IsEndGame) return;
        if (!spriteRenderer.gameObject.activeSelf) return;
        if (IsNeedZoom && !StageSwitcher.I.mapManager.IsZoom) return;
        GetCollectionItem();
    }
}
