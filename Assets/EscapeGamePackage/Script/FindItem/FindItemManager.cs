using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public partial class GameData
{
    public List<FindItemSaveRoot> S_findItemSaveRoots;
}


public class FindItemManager : SingletonMonoBehaviour<FindItemManager>
{
    public FindItemUIManager findItemUIManager;

    public List<FindItemSaveRoot> findItemSaveRoots { get => DataPersistenceManager.I.gameData.S_findItemSaveRoots; set => DataPersistenceManager.I.gameData.S_findItemSaveRoots = value; }
    [SerializeField] private FindItemDatas _findItemdatas;
    public FindItemSaveRoot savedata;

    public GameObject completeScreen;

    protected override void OnAwake()
    {
        
        if (findItemSaveRoots == null)
                findItemSaveRoots = new List<FindItemSaveRoot>();

            bool found = false;

            // 既存のデータを検索
            foreach (var roots in findItemSaveRoots)
            {
                if (roots.FindItemKeysName == _findItemdatas.FindItemKeysName)
                {
                    savedata = roots;
                    found = true;
                    break;
                }
            }

            // 見つからなかった場合のみ新規追加
            if (!found)
            {
                findItemSaveRoots.Add(new FindItemSaveRoot(_findItemdatas));

                foreach (var roots in findItemSaveRoots)
                {
                    if (roots.FindItemKeysName == _findItemdatas.FindItemKeysName)
                    {
                        savedata = roots;
                        break;
                    }
                }

            }

        findItemUIManager.GenerateUI(_findItemdatas);

        completeScreen.SetActive(savedata.IsAllCollect);
    }

    public bool IsSetFindItemList(string findObjectItemKey)
    {
        for (int l = 0; l < savedata.types.Count; l++)
        {
            for (int i = 0; i < savedata.types[l].findItems.Count; i++)
            {
                if(savedata.types[l].findItems[i].findItemKey == findObjectItemKey)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void DoneProgress(string findObjectItemKey)
    {
        var findItem = GetFindItemListByFindObjectItemKey(findObjectItemKey);
        findItem.IsCollected = true;
    }

    public bool IsDoneProgress(string findObjectItemKey)
    {
        var findItem = GetFindItemListByFindObjectItemKey(findObjectItemKey);
        return findItem.IsCollected;
    }

    public FindItemTypeSaveData GetFindItemListByFindItemType(string type)
    {
        for (int i = 0; i < savedata.types.Count; i++)
        {
            if(savedata.types[i].findItemType == type)
            {
                return savedata.types[i];
            }
        }

        return null;
    }

    public FindItemState GetFindItemListByFindObjectItemKey(string findObjectItemKey)
    {
        for (int l = 0; l < savedata.types.Count; l++)
        {
            for (int i = 0; i < savedata.types[l].findItems.Count; i++)
            {
                
                if(savedata.types[l].findItems[i].findItemKey == findObjectItemKey)
                {
                    return savedata.types[l].findItems[i];
                }
            }
        }

        return null;
    }

    public string GetNoCollectFindItemKey()
    {
        for (int l = 0; l < savedata.types.Count; l++)
        {
            for (int i = 0; i < savedata.types[l].findItems.Count; i++)
            {
                
                if(savedata.types[l].findItems[i].IsCollected == false)
                {
                    return savedata.types[l].findItems[i].findItemKey;
                }
            }
        }

        return null;
    }

    public Sprite GetFindItemSprite(string findItemType)
    {
        for (int i = 0; i < _findItemdatas.findItemKeylist.Count; i++)
        {
            if(_findItemdatas.findItemKeylist[i].findItemType == findItemType)
            {
                return _findItemdatas.findItemKeylist[i].findItemSprite;
            }
        }

        return null;
    }

    public bool IsAllCollect()
    {
        for (int l = 0; l < savedata.types.Count; l++)
        {
            for (int i = 0; i < savedata.types[l].findItems.Count; i++)
            {
                
                if(savedata.types[l].findItems[i].IsCollected == false)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void AllCollect()
    {
        savedata.IsAllCollect = true;
        completeScreen.SetActive(savedata.IsAllCollect);
    }
}

[Serializable]
public class FindItemSaveRoot
{
    public string FindItemKeysName;
    public bool IsAllCollect;
    public List<FindItemTypeSaveData> types;

    public FindItemSaveRoot(FindItemDatas findItemDatas)
    {
        FindItemKeysName = findItemDatas.FindItemKeysName;
        types = new List<FindItemTypeSaveData>();

        for (int i = 0; i < findItemDatas.findItemKeylist.Count; i++)
        {
            types.Add(new FindItemTypeSaveData(findItemDatas.findItemKeylist[i]));
        }

    }

    public bool IsAlreadyPlay()
    {

        for (int t = 0; t < types.Count; t++)
        {
            for (int i = 0; i < types[t].findItems.Count; i++)
            {
                if(types[t].findItems[i].IsCollected == true)
                {
                    
                    return true;
                }
            }
        }

        return false;
    }

    public void ResetData()
    {

        for (int t = 0; t < types.Count; t++)
        {
            for (int i = 0; i < types[t].findItems.Count; i++)
            {
                types[t].findItems[i].IsCollected = false;
            }
        }

        IsAllCollect = false;
    }
}

[Serializable]
public class FindItemTypeSaveData
{
    public string findItemType;
    public List<FindItemState> findItems;

    public FindItemTypeSaveData(FindItemKeylist findItemkeylist)
    {
        findItemType = findItemkeylist.findItemType;

        findItems = new List<FindItemState>();
        for (int i = 0; i < findItemkeylist.findItemKeylist.Count; i++)
        {
            findItems.Add(new FindItemState(findItemkeylist.findItemKeylist[i]));
        }
    }
}

[Serializable]
public class FindItemState
{
    public bool IsCollected;
    public string findItemKey;

    public FindItemState(string key)
    {
        findItemKey = key;
    }
}
