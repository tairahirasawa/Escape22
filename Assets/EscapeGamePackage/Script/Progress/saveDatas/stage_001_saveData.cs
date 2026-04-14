using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_001_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_001;
    public List<string> saveKeys => new List<string>()
    {
        "opning",
        "open_toiletDoor",
        "box_renge",
        "get_renge",
        "use_renge",
        "box_cyoumiryou",
        "get_waribashi",
        "open_door",
        "get_wator",
        "use_wator",
        "box_menma",
        "get_menma",
        "box_flypan",
        "open_freazer_left",
        "use_menma",
        "control_time",
        "open_freazer_right",
        "get_freaze_chashu",
        "get_chashu",
        "ending",
    };
}
