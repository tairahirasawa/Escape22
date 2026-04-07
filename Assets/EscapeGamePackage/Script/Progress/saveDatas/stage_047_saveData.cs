using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_047_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_047;
    public List<string> saveKeys => new List<string>()
    {
        "opning",
        "on_patolamp",
        "open_uraguchi",
        "box_tonkachi",
        "get_tonkachi",
        "use_tonkachi",
        "get_driver",
        "use_driver",
        "box_tape",
        "get_tape",
        "use_tape",
        "open_window",
        "get_onigiri",
        "use_onigiri",
        "box_turizao",
        "get_turizao",
        "use_turizao_fail",
        "use_turizao_success",
        "use_fish",
        "get_100yen",
    };
}
