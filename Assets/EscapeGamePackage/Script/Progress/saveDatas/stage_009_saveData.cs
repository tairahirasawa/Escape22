using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_009_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_009;
    public List<string> saveKeys => new List<string>()
    {
        "opening",
        "open_villa",
        "box_shunokel",
        "get_shunokel",
        "box_sougankyou",
        "get_sougankyou",
        "box_cyoushin",
        "get_cyoushin",
        "use_cyoushin",
        "open_wallPanel",
        "get_eda",
        "use_eda",
        "open_iwa",
        "box_sennuki",
        "get_sennuki",
        "box_cola",
        "get_cola",
        "use_sennuki",
        "use_cola",
        "ending",
    };
}
