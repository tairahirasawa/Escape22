using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_041_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_041;
    public List<string> saveKeys => new List<string>()
    {
        "opening",
        "get_onigiri",
        "use_onigiri",
        "talk_george",
        "box_driver",
        "get_driver",
        "get_woodStick_before",
        "box_knife",
        "get_knife",
        "use_knife",
        "use_woodStick_after",
        "on_denki",
        "box_haisen",
        "box_sign",
        "box_zoukin_before",
        "get_zoukin_before",
        "on_sprinkler",
        "use_zoukin_before",
        "use_zoukin_after",
        "ending",
    };
}
