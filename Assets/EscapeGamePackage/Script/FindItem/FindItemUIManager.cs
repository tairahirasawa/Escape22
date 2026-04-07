using System.Collections.Generic;
using UnityEngine;

public class FindItemUIManager : MonoBehaviour
{
    public FindItemCounterUI findItemCounterUI;

    public List<FindItemCounterUI> uilist;

    public void GenerateUI(FindItemDatas findItemDatas)
    {
        for (int i = 0; i < findItemDatas.findItemKeylist.Count; i++)
        {
            var prefab = Instantiate(findItemCounterUI,transform);
            Debug.Log(FindItemManager.I.GetFindItemListByFindItemType(findItemDatas.findItemKeylist[i].findItemType) == null);
            prefab.findItemTypeSaveData = FindItemManager.I.GetFindItemListByFindItemType(findItemDatas.findItemKeylist[i].findItemType);
            prefab.icon.sprite = findItemDatas.findItemKeylist[i].findItemSprite;

            uilist.Add(prefab);
        }

        UpdateCount();
    }

    public void UpdateCount()
    {
        for (int i = 0; i < uilist.Count; i++)
        {
            uilist[i].UpdateCount();
        }
    }
    

}
