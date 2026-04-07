using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_025_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_025;
    public List<string> saveKeys => new List<string>()
    {
        "opning",
        "box_block",
        "open_jihanki",
        "get_juice",
        "open_door",
        "event_meetBoy",
        "rotate_hericopter",
        "box_cardPack",
        "event_getSuperWanko",
        "event_lose",
        "open_batteryBox",
        "box_sissors",
        "get_sissors",
        "box_saikoro",
        "box_vihecles",
        "box_battery",
        "get_battery",
        "use_battery",
        "box_100yen",
        "get_100yen",
        "use_100yen",
        "collectionItem_racer",
        "collectionItem_kozeni",
        "collectionItem_toilet",
        "collectionItem_gacya",
        "collectionItem_shobomonTana",
        "ending",
    };
}
