using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_023_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_023;
    public List<string> saveKeys => new List<string>()
    {
        "opning",
        "open_wallPanel",
        "open_bookShelf",
        "open_door",
        "box_blackBottle",
        "get_tumami",
        "use_tumami",
        "open_cartain",
        "box_tape",
        "get_tape",
        "box_suidoukan",
        "use_tape",
        "box_smallBottle",
        "box_chawan",
        "open_freezer",
        "box_handMirror",
        "get_handMirror",
        "use_handMirror",
        "box_nekoKan_before",
        "get_nekoKan_before",
        "open_nekoKan_before",
        "collectionItem_mushimegane",
        "collectionItem_cloud",
        "collectionItem_donburi",
        "collectionItem_obasan",
        "collectionItem_kobin",
        "ending",
    };
}
