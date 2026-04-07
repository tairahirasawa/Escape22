using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_012_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_012;
    public List<string> saveKeys => new List<string>()
    {
        "box_remokon",
        "get_remokon",
        "use_remokon",
        "open_toilet",
        "open_window",
        "box_shamoji",
        "get_shamoji",
        "open_suihanki",
        "use_shamoji",
        "get_dreamKey",
        "use_dreamKey",
        "open_saku",
        "box_hari",
        "get_hari",
        "use_hari",
        "box_senbei",
        "get_senbei",
        "use_senbei",
        "get_satutaba",
        "ending",
    };
}
