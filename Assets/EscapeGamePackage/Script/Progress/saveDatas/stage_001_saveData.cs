using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_001_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_001;
    public List<string> saveKeys => new List<string>()
    {
        "opning",
        "box_encyoCord",
        "get_encyoCord",
        "use_encyoCord",
        "open_araibaTana_right",
        "open_freezer",
        "get_milk",
        "use_milk",
        "open_eggBox",
        "get_egg",
        "open_araibaTana_left",
        "get_flyingPan",
        "use_flyingPan",
        "get_conroTumami",
        "use_conroTumami",
        "set_fire",
        "use_egg",
        "ending",
    };
}
