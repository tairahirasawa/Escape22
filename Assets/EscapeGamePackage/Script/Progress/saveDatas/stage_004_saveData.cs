using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class stage_004_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_004;
    public List<string> saveKeys => new List<string>()
    {
        "opening",
        "openDoor",
        "box_zoukin",
        "get_zoukin",
        "use_zoukin",
        "open_krageRoom",
        "box_mushimegane",
        "get_mushimegane",
        "box_rakko",
        "box_gunte",
        "get_gnte",
        "drag_nokogirizame",
        "drag_stone",
        "box_ei",
        "box_hasami",
        "get_hasami",
        "use_hasami",
        "box_shachi",
        "box_uketuke",
        "ending",
    };
}
