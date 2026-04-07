using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_029_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_029;
    public List<string> saveKeys => new List<string>()
    {
        "opning",
        "open_door",
        "open_cartein",
        "open_okama_under",
        "open_tableCartein",
        "box_lighter",
        "get_lighter",
        "use_lighter",
        "box_konpeitou",
        "open_okama_top",
        "open_uraguchi",
        "event_find_kurinoki",
        "change_evening",
        "box_scop",
        "get_scop",
        "use_scop",
        "box_tong",
        "get_tong",
        "use_tong",
        "collectionItem_konpeitou",
        "collectionItem_minomushi",
        "collectionItem_ochiba",
        "collectionItem_moguraHole",
        "collectionItem_cloud",
        "ending",
    };
}
