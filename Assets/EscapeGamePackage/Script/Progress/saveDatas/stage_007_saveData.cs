using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_007_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_007;
    public List<string> saveKeys => new List<string>()
    {
        "opening",
        "get_coffeeBeans_01",
        "get_coffeeBeans_02",
        "get_coffeeBeans_03",
        "open_door",
        "box_coffeeBeans_04",
        "get_coffeeBeans_04",
        "open_tana_left",
        "get_coffeeBeans_05",
        "get_coffeeBeans_06",
        "open_tana_right",
        "get_tonkachi",
        "use_tonkachi",
        "box_sissors",
        "get_sissors",
        "get_cheese",
        "get_coffeeBeans_07",
        "get_coffeeBeans_08",
        "get_coffeeBeans_09",
        "box_coffeeBeans_10",
        "get_coffeeBeans_10",
        "ending",
    };
}
