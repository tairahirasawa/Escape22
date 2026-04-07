using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_014_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_014;
    public List<string> saveKeys => new List<string>()
    {
        "box_fude",
        "get_fude",
        "use_fude",
        "box_dreamKey",
        "get_dreamKey",
        "use_dreamKey",
        "open_window",
        "get_degitalPanel",
        "use_degitalPanel",
        "box_fish",
        "get_fish",
        "get_stone",
        "use_stone",
        "use_fish",
        "box_bansoukou",
        "get_bansoukou",
        "use_bansoukou",
        "box_onigiri",
        "get_onigiri",
        "ending",
    };
}
