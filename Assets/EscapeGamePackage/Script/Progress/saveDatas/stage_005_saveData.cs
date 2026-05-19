using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class stage_005_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_005;
    public List<string> saveKeys => new List<string>()
    {
        "opening",
        "box_gowgle",
        "get_gowgle",
        "box_takotubo",
        "box_tako",
        "open_door",
        "box_fude",
        "get_fude",
        "use_fude",
        "box_ashika",
        "open_azarashiRoom",
        "box_scop",
        "get_scop",
        "use_scop",
        "box_barl",
        "get_barl",
        "use_barl",
        "box_iwashi_baketu",
        "get_iwashi_baketu",
        "ending",
    };
}
