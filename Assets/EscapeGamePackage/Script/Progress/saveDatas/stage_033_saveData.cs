using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_033_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_033;
    public List<string> saveKeys => new List<string>()
    {
        "opning",
        "switch_tapestory",
        "open_cartein",
        "open_kajibaDoor",
        "box_kamisori",
        "get_kamisori",
        "use_kamisori",
        "open_shokudouDoor",
        "box_breadBox",
        "box_kinka",
        "get_kinka",
        "box_hummer",
        "use_hummer",
        "box_tamahagane",
        "get_tamahagane",
        "get_sword",
        "event_slime",
        "box_colorNumber",
        "open_basementDoor",
        "collectionItem_kanadoko",
        "collectionItem_danro",
        "collectionItem_cook",
        "collectionItem_shield",
        "collectionItem_tapestory",
        "ending",
    };
}
