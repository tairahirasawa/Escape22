using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_002_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_002;
    public List<string> saveKeys => new List<string>()
    {
        "opening",
        "get_driver",
        "use_driver",
        "stop_Kankisen",
        "open_araibaTana_right",
        "get_soap",
        "open_furoba",
        "use_soap",
        "open_washerMachine",
        "get_showerTotte",
        "use_showerTotte",
        "shower",
        "open_closet",
        "get_tshits",
        "opne_closet_under",
        "get_zubon",
        "open_genkan",
        "ending",
    };
}
