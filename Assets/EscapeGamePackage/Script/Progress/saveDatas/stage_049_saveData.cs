using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_049_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_049;
    public List<string> saveKeys => new List<string>()
    {
        "opening",
        "on_monitar",
        "box_potato",
        "box_fork",
        "get_fork",
        "box_marakas",
        "use_marakas",
        "open_door",
        "box_batteryBox",
        "get_battery",
        "use_battery",
        "box_drinkbar_juice",
        "open_toilet",
        "get_toiletHandle",
        "use_toiletHandle",
        "box_drinkbar_coffee",
        "on_drinkbar_coffee",
        "box_map",
        "get_juice",
        "ending",
    };
}
