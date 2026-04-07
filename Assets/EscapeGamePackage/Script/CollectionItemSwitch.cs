using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CollectionItemSwitch
{
    [SerializeField] public bool UseCollectionItemSwitch;
    [SerializeField] public int collectionItemMinNum;
    [SerializeField] public int collectionItemMaxNum;


    public bool CollectionItemJudge()
    {
        if (!UseCollectionItemSwitch) return true;

        if(ItemManager.I.currentCollectionItemCount >= collectionItemMinNum && ItemManager.I.currentCollectionItemCount <= collectionItemMaxNum)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
