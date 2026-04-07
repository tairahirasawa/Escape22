using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_039_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_039;
    public List<string> saveKeys => new List<string>()
    {
        "opening",
        "get_mushimegane",
        "box_magnet",
        "get_magnet",
        "use_magnet",
        "on_denki",
        "box_hummer",
        "get_hummer",
        "use_hummer",
        "get_pinset",
        "get_cardkey",
        "use_cardkey",
        "on_elevatorPanel",
        "open_notePC",
        "box_battery",
        "get_battery",
        "box_batteryBox",
        "use_battery",
        "open_openButtonBox",
        "ending",
    };
}
