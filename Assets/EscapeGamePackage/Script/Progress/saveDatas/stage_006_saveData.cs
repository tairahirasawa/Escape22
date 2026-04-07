using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_006_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_006;
    public List<string> saveKeys => new List<string>()
    {
        "opening",
        "open_cartein",
        "box_encyocord",
        "get_encyocord",
        "use_encyocord",
        "box_coffeecup",
        "get_coffeecup",
        "use_coffeecup",
        "open_tana_left",
        "create_coffee",
        "box_piggybank",
        "get_piggyband",
        "get_denkyu",
        "use_denkyu",
        "open_tana_right",
        "get_tonkachi",
        "use_tonkachi",
        "use_10yen",
        "ending",
    };
}
