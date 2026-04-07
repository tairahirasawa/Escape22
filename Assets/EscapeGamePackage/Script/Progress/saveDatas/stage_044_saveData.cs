using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_044_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_044;
    public List<string> saveKeys => new List<string>()
    {
        "opning",
        "open_shutter",
        "box_ICcard",
        "get_ICcard",
        "box_cardReader",
        "use_ICcard",
        "box_pinset",
        "get_pinset",
        "use_pinset",
        "use_coin",
        "box_doorGimmick",
        "open_trainDoor_01",
        "get_kasa",
        "use_kasa",
        "box_sissors",
        "get_sissors",
        "use_sissors",
        "get_magic",
        "use_magic",
        "open_trainDoor_02",
        "ending",
    };
}
