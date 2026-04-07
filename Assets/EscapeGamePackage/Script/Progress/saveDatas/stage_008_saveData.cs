using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_008_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_008;
    public List<string> saveKeys => new List<string>()
    {
        "opening",
        "box_yobirin_right",
        "come_shortCake",
        "turnOn_denki",
        "open_tana_left",
        "get_teaBag",
        "use_teaBag",
        "box_yobirin_left",
        "come_donuts",
        "open_tana_right",
        "get_catFood",
        "use_catFood",
        "open_door",
        "open_manhole",
        "box_ladder",
        "get_ladder",
        "get_tape",
        "use_tape",
        "use_ladder_after",
        "get_koneko",
        "ending",
    };
}
