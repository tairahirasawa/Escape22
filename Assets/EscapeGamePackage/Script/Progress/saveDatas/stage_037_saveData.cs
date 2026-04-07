using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_037_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_037;
    public List<string> saveKeys => new List<string>()
    {
        "opening",
        "box_waribashi",
        "get_waribashi",
        "use_waribashi",
        "box_denkiSwitch",
        "open_toilet",
        "box_hokori",
        "open_cartain",
        "box_nannsui",
        "get_nannsui",
        "open_washingMachine",
        "box_encyouCord",
        "get_encyouCord",
        "use_encyouCord",
        "box_kousui",
        "get_kousui",
        "get_suidousui",
        "search_eyeMask",
        "box_eyeMask",
        "get_eyeMask",
        "collectionItem_remokon",
        "collectionItem_washingMachine",
        "collectionItem_toiletPaper",
        "collectionItem_tape",
        "collectionItem_denki",
        "ending",
    };
}
