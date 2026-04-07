using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

[CreateAssetMenu(menuName = "ScriptableObjects/ItemDatas")]
public class ItemDatas : ScriptableObject
{
    public SerializableDictionaryBase<string, ItemData> ItemDataList;
    public SerializableDictionaryBase<string, ItemData> CollectionItemDataList;
}

[Serializable]
public class ItemData
{
    public Sprite ItemSprite;
    public ProgressSwitch enableSwitch;
    public ProgressSwitch disableSwitch;
    public EventActionConfig eventActionConfig;
}

public enum ItemType
{
    none,
    Yen100,
    Bread,
    IcCard,
    SmartPhone,
    megaPhone,
    driverSentan,
    driverMochite,
    driver,
    neji,
    mirror,
    jyouro_before,
    jyaguchiMochite,
    jyouro_after,
    etcCard,
    parkingKanban,
    ketchap,
    rebar,
    lantern,
    sennuki,
    carKey,
    OnsenTicket,
    gacyagacyaHandle,
    roomKey102,
    Tonkachi,
    pinponBall,
    towel,
    roomKey101,
    yukata,
    KouishituKey,
    turuhashi,
    Ice_Kamisori,
    Kamisori,
    hari_WeightScale,
    zoukin,
    sissors,
    oshokujiken

}

public enum CollectionItemtype
{
    none,matutake
}