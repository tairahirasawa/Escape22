using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_022_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_022;
    public List<string> saveKeys => new List<string>()
    {
        "opning",
        "box_magnet",
        "get_magnet",
        "open_door",
        "box_paper",
        "use_magnet",
        "open_freezer",
        "box_ladder",
        "get_ladder",
        "open_tana_left",
        "box_cheezePaint",
        "open_manhole",
        "use_ladder",
        "box_light",
        "get_light",
        "get_battery",
        "use_battery",
        "box_necklace",
        "get_necklace",
        "collectionItem_sara",
        "collectionItem_behindMeet",
        "collectionItem_Inhat",
        "collectionItem_wator",
        "collectionItem_kugi",
        "ending",
    };
}
