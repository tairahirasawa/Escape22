using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_036_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_036;
    public List<string> saveKeys => new List<string>()
    {
        "opening",
        "open_tisshu",
        "open_window",
        "box_mushimegane",
        "get_mushimegane",
        "box_gameMachine",
        "get_gameMachine",
        "open_casetSlot",
        "box_controller",
        "get_controller",
        "use_controller",
        "open_freezer",
        "on_stove",
        "get_yakan_before",
        "get_yakan_after",
        "get_cutter",
        "use_cutter",
        "box_gameSoft",
        "get_gameSoft",
        "collectionItem_denki",
        "collectionItem_conro",
        "collectionItem_cloud",
        "collectionItem_danbowl",
        "collectionItem_sink",
        "ending",
    };
}
