using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_031_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_031;
    public List<string> saveKeys => new List<string>()
    {
        "box_chiken",
        "opning",
        "open_cartein",
        "box_lighter",
        "get_lighter",
        "use_lighter",
        "box_pen",
        "get_pen",
        "use_pen",
        "open_door",
        "event_seachYukikaki",
        "box_yukikaki",
        "get_yukikaki",
        "box_yukidaruma",
        "box_rousoku",
        "get_rousoku_before",
        "get_rousoku_after",
        "use_rousoku_after",
        "box_juice",
        "get_juice",
        "collectionItem_freezer",
        "collectionItem_cloud",
        "collectionItem_roof",
        "collectionItem_chiken",
        "collectionItem_mountain",
        "ending",
    };
}
