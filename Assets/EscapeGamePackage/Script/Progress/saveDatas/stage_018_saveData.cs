using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_018_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_018;
    public List<string> saveKeys => new List<string>()
    {
        "open_freezer_left",
        "box_fude",
        "get_fude_before",
        "get_fude_after",
        "get_10yen",
        "use_fude_after",
        "get_manekineko",
        "event_come",
        "use_10yen",
        "open_door",
        "open_freezer_right",
        "box_match",
        "get_match",
        "use_match",
        "box_tong",
        "get_tong",
        "get_500yen",
        "open_sakanayaShatter",
        "use_500yen",
        "einding",
    };
}
