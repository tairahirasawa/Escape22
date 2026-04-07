using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class stage_003_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_003;
    public List<string> saveKeys => new List<string>()
    {
        "opening",
        "get_maki_01",
        "get_maki_02",
        "open_tent",
        "get_maki_03",
        "open_red_ryukku",
        "get_maki_04",
        "open_blue_ryukku",
        "get_maki_05",
        "get_maki_06",
        "get_tennisBall",
        "get_maki_07",
        "get_bungalowKey",
        "use_bungalowKey",
        "get_maki_08",
        "open_green_ryukku",
        "get_maki_09",
        "open_makiBox",
        "get_maki_10",
        "ending",
    };
}
