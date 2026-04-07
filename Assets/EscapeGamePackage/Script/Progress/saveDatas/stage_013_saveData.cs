using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_013_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_013;
    public List<string> saveKeys => new List<string>()
    {
        "get_houcyou",
        "get_smartphone",
        "open_cartain",
        "unlock_smartphone",
        "open_closet",
        "box_dreamkey",
        "get_dreamkey",
        "use_dreamkey",
        "open_usagigoya",
        "open_bookShelf",
        "box_jyouro",
        "get_jyouro",
        "wator_appear",
        "get_jyouroWator",
        "use_jyouroWator",
        "get_ninjin",
        "use_ninjin",
        "get_rabitPapet",
        "ending",
    };
}
