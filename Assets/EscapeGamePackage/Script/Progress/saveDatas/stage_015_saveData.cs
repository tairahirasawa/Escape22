using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_015_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_015;
    public List<string> saveKeys => new List<string>()
    {
        "opening",
        "box_flashLight",
        "get_flashLight",
        "use_flashLight",
        "get_scop",
        "use_scop",
        "get_battery",
        "open_batteryBox",
        "use_battery",
        "box_carrot",
        "get_carrot",
        "use_carrot",
        "get_uchiwa",
        "box_furin",
        "get_furin",
        "use_furin",
        "use_uchiwa",
        "box_kiduchi",
        "get_kiduchi",
        "get_dango",
        "ending",
    };
}
