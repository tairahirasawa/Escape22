using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_032_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_032;
    public List<string> saveKeys => new List<string>()
    {
        "opning",
        "open_shutter",
        "box_uekibachi",
        "grow_tougarashi",
        "get_tougarashi",
        "use_tougarashi",
        "set_fire",
        "open_door",
        "box_happa",
        "get_happa",
        "use_happa",
        "box_mimizuku",
        "get_battery",
        "use_battery",
        "box_bouenkyou",
        "get_bouenkyou",
        "use_bouenkyou",
        "box_pikkel",
        "get_pikkel",
        "use_pikkel",
        "collectionItem_tree",
        "collectionItem_door",
        "collectionItem_nightSky",
        "collectionItem_clock",
        "collectionItem_kirikabu",
        "ending",
    };
}
