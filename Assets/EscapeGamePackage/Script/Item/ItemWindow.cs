using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using RotaryHeart.Lib.SerializableDictionary;

public class ItemWindow : SingletonMonoBehaviour<ItemWindow>,IPointerClickHandler
{
    [HideInInspector]public CanvasGroup canvasGroup;

    public GameObject ItemBase;
    public List<EventActionCreator> eventActionCreators = new List<EventActionCreator>();

    public Button closeBtn;

    public string ScrItemType;
    public Image itemImage;

    public bool changed;

    public SerializableDictionaryBase<ItemType, PlayableDirector> timelines;

    Coroutine coroutine;

    public bool IsPresent;

    protected override void OnAwake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        closeBtn.onClick.AddListener(DismissItemWindow);

        UsefulMethod.Hide(canvasGroup);
    }

    public async void AddEventActionCreator(string type)
    {
        RemoveEventActionCreator();

        if (ItemBase.GetComponent<EventActionCreator>())
        {
            Destroy(ItemBase.GetComponent<EventActionCreator>());
        }

        var creater = ItemBase.AddComponent<EventActionCreator>();
        creater.eventActionConfig = DataBase.I.itemDatas.ItemDataList[type].eventActionConfig;

        await creater.InitializeGame();
        await creater.LoadGame();

        eventActionCreators.Add(creater);
    }

    public void RemoveEventActionCreator()
    {
        for (int i = 0; i < eventActionCreators.Count; i++)
        {
            Destroy(eventActionCreators[i]);
        }

        eventActionCreators.Clear();
    }

    public void OnlyPresentItemWindow(string type, bool animation = true)
    {
        closeBtn.gameObject.SetActive(false);

        itemImage.sprite = DataBase.I.itemDatas.ItemDataList[type].ItemSprite;
        ScrItemType = type;
        UsefulMethod.Present(canvasGroup);

        if (animation) itemImage.transform.DOPunchScale(Vector3.one * 0.1f, 0.2f, 5, 10);
        if (!AudioDirector.I.seAudioSource.isPlaying) AudioDirector.I.PlaySE(AudioDirector.SeType.Window);
    }


    public void PresentItemWindow(string type,bool animation = true)
    {
        closeBtn.gameObject.SetActive(true);
        MoveBtn.INEventAction = true;

        IsPresent = true;
        itemImage.sprite = DataBase.I.itemDatas.ItemDataList[type].ItemSprite;
        AddEventActionCreator(type);

        ScrItemType = type;
        UsefulMethod.Present(canvasGroup);

        if (animation) itemImage.transform.DOPunchScale(Vector3.one * 0.1f, 0.2f,5,10);
        if(!AudioDirector.I.seAudioSource.isPlaying) AudioDirector.I.PlaySE(AudioDirector.SeType.Window);
    }

    public void PresentCollectionItemWindow(string type, bool animation = true)
    {
        closeBtn.gameObject.SetActive(true);
        itemImage.sprite = DataBase.I.itemDatas.CollectionItemDataList[type].ItemSprite;
        //ItemImageDictionary.I.PresentWindowItem(type);
        ScrItemType = null;
        UsefulMethod.Present(canvasGroup);

        if (animation) transform.DOPunchScale(Vector3.one * 0.01f, 0.2f, 5, 10);
        //if(!AudioDirector.I.seAudioSource.isPlaying) 
            AudioDirector.I.PlaySE(AudioDirector.SeType.Window);

    }

    public void PresentFindItemWindow(string type, bool animation = true)
    {
        closeBtn.gameObject.SetActive(true);
        itemImage.sprite = FindItemManager.I.GetFindItemSprite(type);
        //ItemImageDictionary.I.PresentWindowItem(type);
        ScrItemType = null;
        UsefulMethod.Present(canvasGroup);

        if (animation) transform.DOPunchScale(Vector3.one * 0.01f, 0.2f, 5, 10);
        //if(!AudioDirector.I.seAudioSource.isPlaying) 
            AudioDirector.I.PlaySE(AudioDirector.SeType.Window);

    }


    private void Update()
    {
        //IsPresent = canvasGroup.alpha == 1;
    }

    public void DismissItemWindow()
    {
        MoveBtn.INEventAction = false;
        IsPresent = false;
        RemoveEventActionCreator();
        UsefulMethod.Hide(canvasGroup);
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        changed = false;

        //ChangeItemCheck();

        //GoExScr(ItemManager.ItemType.beaker_water, "mijinko");

        if(changed == false)
        {
            //ここにコレクションアイテム

            /*
            if (ScrItemType == ItemManager.ItemType.baketu_hitode)
            {
                AudioDirector.I.PlaySE(AudioDirector.SeType.Window);
                ItemManager.I.GetCollectionItem(ItemManager.CollectionItemtype.hitode, 108);
                ItemManager.I.RemoveItem(ItemManager.ItemType.baketu_hitode);
            }
            changed = true;
            */
        }

    }

    public void GoExScr(string scrItem,string exScrName)
    {
        if(ScrItemType == scrItem)
        {
            UsefulMethod.Hide(canvasGroup);
            //ExtraScrManager.I.PresentExtrakScr(exScrName);
            MoveBtn.OnClickBackBtnAction = BackExScr;

        }
    }

    public void BackExScr()
    {
        UsefulMethod.Present(canvasGroup);
        //ExtraScrManager.I.HideExtraScr();
    }


    //変更先のアイテムがある場合は、ここに追加
    public void ChangeItemCheck()
    {

    }


    //一度使ったアイテムを分解する。
    /*
    public void ReChangeItem(int triggerNum, string scrItem, string changeItem, int progressNum = 1000, bool animation = true, PlayableDirector timeline = null)
    {
        if (ProgressDirector.I.IsDoneProgress(triggerNum))
            ItemChange(scrItem, changeItem, progressNum, animation, timeline);
    }
    */

    //一度使ったアイテムを自動的に切り替える
    public void AutoGetItem(string changeItem, int progressNum = 1000, bool animation = true, PlayableDirector timeline = null)
    {
        ItemManager.I.GetItem(changeItem, false, false);
    }
    
    /*
    private void ItemChange(string scrItem, string changeItem, int progressNum = 1000, bool animation = true, PlayableDirector timeline = null, Action act = null)
    {
        if (coroutine != null) return;
        coroutine = StartCoroutine(IeItemChange(scrItem, changeItem, progressNum, animation, timeline,act));
    }


    IEnumerator IeItemChange(string scrItem, string changeItem,int progressNum = 1000,bool animation = true,PlayableDirector timeline = null,Action act = null)
    {
        if(changed == false)
        {
            if (ScrItemType == scrItem)
            {
                changed = true;
                if (timeline != null)
                {
                    timeline.Play();
  
                    yield return new WaitForSeconds((float)timeline.duration);
                }


                ItemManager.I.GetItem(changeItem,animation);
                //ItemManager.I.RemoveItem(scrItem);


                if (progressNum != 1000)
                {
                    ProgressDirector.I.DoneProgress(progressNum);
                }

                act?.Invoke();
                changed = true;
            }
            coroutine = null;
        }

    }

    private void ItemMix(string scrItem, string selectItem, string getItem, int progressNum = 1000, bool removeScrItem = true, bool removeSelectItem = true, ItemType extraRecmoveItem = ItemType.none, PlayableDirector timeline = null, Action act = null)
    {
        if (coroutine != null) return;
        coroutine = StartCoroutine(IeItemMix(scrItem, selectItem, getItem, progressNum, removeScrItem, removeSelectItem, extraRecmoveItem, timeline, act));
    }


    IEnumerator IeItemMix(string scrItem, string selectItem, string getItem , int progressNum = 1000, bool removeScrItem = true, bool removeSelectItem = true,ItemType extraRecmoveItem = ItemType.none,PlayableDirector timeline = null,Action act = null)
    {
        if (ScrItemType == scrItem && ItemManager.I.currentItem == selectItem)
        {
            if(timeline != null)
            {
                timeline.Play();
                yield return new WaitForSeconds((float)timeline.duration);
            }

            ItemManager.I.GetItem(getItem);

           //if(removeScrItem)ItemManager.I.RemoveItem(scrItem);
            //if(removeSelectItem) ItemManager.I.RemoveItem(selectItem);

            //if (extraRecmoveItem != ItemType.none) ItemManager.I.RemoveItem(extraRecmoveItem);

            if(progressNum != 1000)
            {
                ProgressDirector.I.DoneProgress(progressNum);
            }

            act?.Invoke();
            changed = true;
        }
        coroutine = null;
    }
*/
}
