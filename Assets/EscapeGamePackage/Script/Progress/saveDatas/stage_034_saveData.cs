using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_034_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_034;
    public List<string> saveKeys => new List<string>()
    {
        "opning",
        "get_lighter",
        "use_lighter",
        "door_shoukan",
        "box_penchi",
        "get_penchi",
        "use_penchi",
        "box_suishou",
        "get_suishou",
        "use_suishou",
        "box_chork",
        "get_chork",
        "use_chork",
        "door_toshokan",
        "box_book",
        "box_yumiya",
        "get_yumiya",
        "use_yumiya",
        "door_gobrin",
        "collectionItem_chair",
        "collectionItem_magicCercle",
        "collectionItem_gaikotu",
        "collectionItem_sword",
        "collectionItem_bookShelf",
        "ending",
    };
}
