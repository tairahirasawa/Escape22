using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_020_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_020;
    public List<string> saveKeys => new List<string>()
    {
        "opning",
        "get_lighter",
        "use_lighter",
        "open_freezer_right",
        "get_ice",
        "get_remokon",
        "use_remokon",
        "open_suihanki",
        "box_shamoji",
        "get_shamoji",
        "use_shamoji",
        "open_freezer_left",
        "open_door",
        "box_ladder",
        "get_ladder",
        "use_ladder",
        "open_shatter_fishMarket",
        "box_chopStick",
        "get_chopStick",
        "ending",
    };
}
