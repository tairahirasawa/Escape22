using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemObject : MonoBehaviour , IPointerClickHandler
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
        if (progressKey.episodeType != StageContext.currentStageType) return;
        if (!string.IsNullOrEmpty(type) && !ApplyAnotherSprite)
        {
            spriteRenderer.sprite = DataBase.I.itemDatas.ItemDataList[type].ItemSprite;
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

    private void Update()
    {
        //spriteRenderer.gameObject.SetActive(!ProgressDirector.I.IsDoneProgress(progressKey));
    }

    public void GetItem()
    {
        
        if (progressKey.episodeType == StageContext.StageType.none) ProgressDirector.I.CurrentProgressKey = progressKey;

        ItemManager.I.GetItem(type);

        for (int i = 0; i < chaneObject.Count; i++)
        {
            chaneObject[i].SetActive(false);
        }


        ProgressDirector.I.DoneProgress(progressKey);
        
        spriteRenderer.gameObject.SetActive(false);

        //広告
        if(!DataPersistenceManager.I.gameData.blockAd)
        {
            if(ProgressDirector.I.GetProgressKeyIndex(progressKey) >= 10 && ProgressSaveManger.GetProgressDatas(StageContext.currentStageType.ToString()).interAdShowed == false)
            {
                GoogleAdManager.I.googleAD_Inter.ShowAd(1000);
                ProgressSaveManger.GetProgressDatas(StageContext.currentStageType.ToString()).interAdShowed = true;
            }

        }
        //

    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (!spriteRenderer.gameObject.activeSelf) return;
        GetItem();
    }
}
