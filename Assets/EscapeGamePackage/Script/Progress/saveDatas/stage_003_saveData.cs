using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class stage_003_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_003;
    public List<string> saveKeys => new List<string>()
    {
        "opening",
        "open_shutter",
        "box_haburashi",
        "get_haburashi",
        "use_haburashi",
        "use_sissors",
        "use_hari_01",
        "use_hari_02",
        "use_500yen",
        "open_sikuinnDoor",
        "box_baketu",
        "get_baketu_before",
        "drag_okiami",
        "use_baketu_before",
        "use_baketu_after",
        "open_kurageRoom",
        "box_tokei",
        "gimmick_time",
        "box_switch",
        "ending",
    };
}
