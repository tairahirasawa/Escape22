using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_017_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_017;
    public List<string> saveKeys => new List<string>()
    {
        "opening",
        "open_uraguchi",
        "box_cutter",
        "get_cutter",
        "switch_bill",
        "box_tonkachi",
        "get_tonkachi",
        "get_kugi",
        "repair_ladder",
        "meet_oni",
        "box_bachi",
        "get_bachi",
        "open_shouji",
        "panel_kugimuki",
        "get_kuginuki",
        "use_kuginuki",
        "repair_amamori",
        "open_houseWindow",
        "get_oniPants",
        "ending",
    };
}
