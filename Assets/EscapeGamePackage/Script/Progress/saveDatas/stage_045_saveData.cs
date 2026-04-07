using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_045_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_045;
    public List<string> saveKeys => new List<string>()
    {
        "opning",
        "box_kaicyudentou",
        "get_kaicyudentou",
        "use_kaicyudentou",
        "get_driver",
        "use_driver",
        "open_kouban",
        "open_window",
        "open_suidou",
        "box_ramen",
        "box_hummer",
        "get_hummer",
        "use_hummer",
        "open_manhole",
        "get_recipt",
        "open_gesuidouWator",
        "box_ami",
        "get_ami",
        "get_cheeze_before",
        "get_cheeze_after",
        "ending",
    };
}
