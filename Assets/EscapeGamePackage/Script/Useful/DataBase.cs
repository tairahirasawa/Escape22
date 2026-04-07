using System.Collections;
using System.Collections.Generic;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

public class DataBase : SingletonMonoBehaviour<DataBase>
{
    public SerializableDictionaryBase<string, ItemDatas> itemDatalist;
    [HideInInspector]public ItemDatas itemDatas;

    public WindowColorData windowColorData;

    protected override void OnAwake()
    {
        Debug.Log(StageContext.GetCurrentPrefix());
        itemDatas = itemDatalist[StageContext.GetCurrentPrefix()];
    }
}
