using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_042_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_042;
    public List<string> saveKeys => new List<string>()
    {
        "opening",
        "box_megaphone",
        "get_megaphone",
        "use_megaphone",
        "box_sougankyou",
        "get_sougankyou",
        "on_trainLight",
        "open_shutter",
        "open_freazer",
        "open_trainDoor",
        "get_hankachi",
        "use_hankachi",
        "open_trainWindow",
        "get_himo5yen",
        "use_himo5yen",
        "box_sissors",
        "get_sissors",
        "use_sissors_ekiinn",
        "use_sissors_jentlman",
        "box_openButton",
        "ending",
    };
}
