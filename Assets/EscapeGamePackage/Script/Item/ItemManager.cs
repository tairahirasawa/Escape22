using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RotaryHeart.Lib.SerializableDictionary;
using TMPro;
using Unity.VisualScripting;
using System.Data;

public partial class GameData
{
    //public List<ItemConfig> S_itemList;
    public List<int> S_collectionItemList;

    public int S_CurrentCollectionItemCount;
}

[Serializable]
public class ItemConfig
{
    public string itemType;
    public ProgressSwitch enalbleSwitch;
    public ProgressSwitch disableSwitch;

    public ItemConfig(string itemType)
    {
        this.itemType = itemType;
    }
}

public class ItemManager : SingletonMonoBehaviour<ItemManager>
{
    public string currentItem;
    public Item itemPrefab;

    public List<Item> itemObjectList = new List<Item>();
    public List<ItemConfig> itemConfigList;// { get => DataPersistenceManager.I.gameData.S_itemList; set => DataPersistenceManager.I.gameData.S_itemList = value; }

    public Transform ItemEria;
    public ItemWindow itemWindow;

    public TextMeshProUGUI collectionCountText;
    private int collectionItemCount;
    //public List<int> collectionItemList { get => DataPersistenceManager.I.gameData.S_collectionItemList; set => DataPersistenceManager.I.gameData.S_collectionItemList= value; }
    public int currentCollectionItemCount;

    public float escapeParsent => EscapeParsent();

    //public SerializableDictionaryBase<ItemType, Sprite> itemSprites;
    //public SerializableDictionaryBase<CollectionItemtype, Sprite> collectionitemSprites;


    protected override void OnStart()
    {
        //currentCollectionItemCount = 0;

        base.OnStart();

        if(StageContext.currentStageType == StageContext.StageType.none) return;

        itemConfigList = ProgressSaveManger.GetItems(StageContext.GetCurrentPrefix());

        foreach (var item in DataBase.I.itemDatas.ItemDataList)
        {
            if (itemConfigList == null)
            {
                itemConfigList = new List<ItemConfig>();

            }

            var prefab = Instantiate(itemPrefab, ItemEria.transform);
            prefab.type = item.Key;

            prefab.itemImage.sprite = item.Value.ItemSprite;
            itemObjectList.Add(prefab);


            if (!IsConfigListExist(item.Key))
            {
                itemConfigList.Add(new ItemConfig(item.Key));
            }

            GetItemConfig(item.Key).enalbleSwitch = item.Value.enableSwitch;
            GetItemConfig(item.Key).disableSwitch = item.Value.disableSwitch;

        }

        if (DataBase.I.itemDatas.CollectionItemDataList != null)
        {
            collectionItemCount = DataBase.I.itemDatas.CollectionItemDataList.Count;
        }

        UpdateCurrentCollectionItemCount();
    }

    public bool IsConfigListExist(string type)
    {
        if (itemConfigList == null) return false;

        for (int i = 0; i < itemConfigList.Count; i++)
        {
            if (type == itemConfigList[i].itemType)
            {
                return true;
            }
        }

        return false;
    }


    public float EscapeParsent()
    {
        return ((float)currentCollectionItemCount / (float)collectionItemCount) * 100;
    }


    public void GetItem(string type, bool animation = true, bool presentItemWindow = true, bool onlyPresent = false)
    {
        currentItem = null;

        if (presentItemWindow)
        {
            if (animation) AudioDirector.I.PlaySE(AudioDirector.SeType.itemGet);

            if (onlyPresent)
            {
                ItemWindow.I.OnlyPresentItemWindow(type, animation);
            }
            else
            {
                ItemWindow.I.PresentItemWindow(type, animation);
            }
        }

    }

    public void GetCollectionItem(string type, bool animation = true)
    {

        ItemWindow.I.PresentCollectionItemWindow(type, animation);
        UpdateCurrentCollectionItemCount();
    }

    public void GetFindItem(string type, bool animation = true)
    {

        ItemWindow.I.PresentFindItemWindow(type, animation);
        //UpdateCurrentCollectionItemCount();
    }

    public void UpdateCurrentCollectionItemCount()
    {
        if (DataBase.I.itemDatas.CollectionItemDataList == null) return;

        var collectionItems = DataBase.I.itemDatas.CollectionItemDataList;
        var count = 0;

        foreach (var collectioItem in collectionItems)
        {
            if (collectioItem.Value.enableSwitch.JudgeSwitch() == true)
            {
                count++;
            }
        }

        currentCollectionItemCount = count;
    }

    public void LoadItem()
    {

        if (itemConfigList == null) return;

        if (itemConfigList != null && itemConfigList.Count > 0)
        {


            for (int i = 0; i < itemConfigList.Count; i++)
            {
                var item = Instantiate(itemPrefab, ItemEria.transform);
                item.type = itemConfigList[i].itemType;

                item.itemImage.sprite = DataBase.I.itemDatas.ItemDataList[itemConfigList[i].itemType].ItemSprite;
                itemObjectList.Add(item);
            }
        }

    }

    private Item FindItem(string type)
    {
        for (int i = 0; i < itemObjectList.Count; i++)
        {
            if (itemObjectList[i].type == type)
            {
                return itemObjectList[i];
            }
        }

        return null;
    }

    public void HideItem(string type)
    {
        if (GetItemConfig(type) == null) return;

        for (int i = 0; i < itemObjectList.Count; i++)
        {
            if (itemObjectList[i].type == type)
            {
                UsefulMethod.Hide(itemObjectList[i].gameObject.GetComponent<CanvasGroup>());
                currentItem = null;

                break;
            }
        }
    }



    public void RemoveItemAll()
    {
        for (int i = 0; i < itemObjectList.Count; i++)
        {
            Destroy(itemObjectList[i].gameObject);

            itemConfigList.Remove(GetItemConfig(itemObjectList[i].type));
            itemObjectList.Remove(itemObjectList[i]);

        }

        currentItem = null;
    }


    public ItemConfig GetItemConfig(string type)
    {
        if (itemConfigList == null) return null;

        for (int i = 0; i < itemConfigList.Count; i++)
        {
            if (itemConfigList[i].itemType == type)
            {
                return itemConfigList[i];
            }
        }

        return null;
    }
    /*
        public void MultiDoneRemove(ItemType itemType,int progressNum1 = 96, int progressNum2 = 96,int progressNum3 = 96)
        {
            if(ProgressDirector.I.IsDoneProgress(progressNum1) && ProgressDirector.I.IsDoneProgress(progressNum2) && ProgressDirector.I.IsDoneProgress(progressNum3))
            {
                //RemoveItem(itemType);
            }


        }
    */
    private void Update()
    {
        if (collectionCountText != null) collectionCountText.text = string.Format("{0}/{1}", currentCollectionItemCount, collectionItemCount);
        if (currentCollectionItemCount > collectionItemCount) currentCollectionItemCount = collectionItemCount;

        //MultiDoneRemove(ItemType.scop, 7, 87);
            //MultiDoneRemove(ItemType.scop, 30, 59);
            //MultiDoneRemove(ItemType.fishbone, 74, 94);
            //MultiDoneRemove(ItemType.hummer, 103, 35, 44);

            if (itemObjectList != null && itemObjectList.Count != 0)
            {
                for (int i = 0; i < itemObjectList.Count; i++)
                {
                    if (itemObjectList[i].type == currentItem)
                    {
                        itemObjectList[i].GetComponent<Image>().color = Color.white;
                    }
                    else
                    {
                        itemObjectList[i].GetComponent<Image>().color = Color.gray;
                    }
                }

            }


        for (int i = 0; i < itemConfigList.Count; i++)
        {
            var item = FindItem(itemConfigList[i].itemType);
            item.gameObject.SetActive(itemConfigList[i].enalbleSwitch.JudgeSwitch() && !itemConfigList[i].disableSwitch.JudgeSwitch());

            if (!item.gameObject.activeSelf && currentItem == itemConfigList[i].itemType)
            {
                currentItem = null;
            }
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            //ProgressDirector.I.GetProgressData(ProgressKey.useTonkachi).IsDone = false;
            //ProgressDirector.I.GetProgressData(ProgressKey.GetMagicLamp).IsDone = false;
            //GetItem(ItemType.SoccerBallBefore);

            //GetItem(ItemType.tonkachi);
            //RemoveItem(ItemType.magicHand);
            //GetItem(ItemType.Penchi);
            //GetItem(ItemType.Hake);

        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            currentCollectionItemCount--;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            currentCollectionItemCount++;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            //GetItem(ItemType.OnsenKey3);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            //RemoveItemAll();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {


        }

    }
}