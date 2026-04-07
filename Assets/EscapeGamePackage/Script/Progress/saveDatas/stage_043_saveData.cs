using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_043_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_043;
    public List<string> saveKeys => new List<string>()
    {
        "opning",
        "box_hamigakiko",
        "get_hamigakiko",
        "use_hamigakiko",
        "box_pinset",
        "get_pinset",
        "use_pinset",
        "open_shutter",
        "open_window",
        "box_cutter",
        "get_cutter",
        "use_cutter",
        "box_icepick",
        "get_icepick",
        "use_icepick",
        "box_fude_before",
        "get_fude_before",
        "use_fude_before",
        "use_fude_after",
        "box_jyougi",
        "get_jyougi",
        "ending",
    };
}
