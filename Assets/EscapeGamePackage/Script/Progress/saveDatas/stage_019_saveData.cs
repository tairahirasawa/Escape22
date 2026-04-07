using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_019_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_019;
    public List<string> saveKeys => new List<string>()
    {
        "opning",
        "open_freezer_left",
        "box_jyouro_before",
        "get_jyouro_before",
        "use_jyouro_before",
        "use_jyouro_after",
        "box_mushimegane",
        "get_mushimegane",
        "open_door",
        "box_barl",
        "get_barl",
        "box_hankachi",
        "get_hankachi",
        "use_hankachi",
        "open_shutter_fishmarket",
        "box_sissors",
        "get_sissors",
        "get_pen",
        "use_pen",
        "open_freezer_right",
        "get_daikon",
        "ending",
    };
}
