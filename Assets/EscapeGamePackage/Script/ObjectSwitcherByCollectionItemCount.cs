using System.Collections.Generic;
using UnityEngine;

public class ObjectSwitcherByCollectionItemCount : MonoBehaviour
{
    public List<ObjectSwitcherByCollectionItemCountContent> Contents;
    
    private void Update()
    {
        if(!ItemManager.I) return;

        for (int i = 0; i < Contents.Count; i++)
            {
            var content = Contents[i];
            bool inRange = (ItemManager.I.currentCollectionItemCount >= content.minNum && ItemManager.I.currentCollectionItemCount <= content.maxNum);
            content.target.SetActive(inRange);
            }
    }
}

[System.Serializable]
public class ObjectSwitcherByCollectionItemCountContent
{
    public int minNum;
    public int maxNum;
    public GameObject target;
}