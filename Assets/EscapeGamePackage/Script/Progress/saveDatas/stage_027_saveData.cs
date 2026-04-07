using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_027_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_027;
    public List<string> saveKeys => new List<string>()
    {
        "opning",
        "box_okane",
        "box_tana_senbei",
        "open_door",
        "box_houki",
        "get_houki",
        "use_houki",
        "box_scop",
        "get_scop",
        "use_scop",
        "box_umeboshi",
        "get_umeboshi",
        "use_umeboshi",
        "open_uraguchi",
        "event_find_kakinoki",
        "box_nata",
        "get_nata",
        "box_fundoshi",
        "get_fundoshi",
        "use_fundoshi",
        "collectionItem_tukue",
        "collectionItem_tubo",
        "collectionItem_ochiba",
        "collectionItem_fundoshi",
        "collectionItem_kakinoki",
        "ending",
    };
}
