using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_002_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_002;
    public List<string> saveKeys => new List<string>()
    {
        "opening",
        "box_shouyusashi",
        "box_driver",
        "get_driver",
        "box_waribashi",
        "open_door",
        "use_driver",
        "box_batteryBox",
        "get_battery",
        "on_kenbaiki",
        "box_shouyu",
        "get_shouyu",
        "use_shouyu",
        "open_freezer",
        "box_houcyou",
        "get_houcyou",
        "use_houcyou",
        "box_barl",
        "get_barl",
        "ending",
    };
}
