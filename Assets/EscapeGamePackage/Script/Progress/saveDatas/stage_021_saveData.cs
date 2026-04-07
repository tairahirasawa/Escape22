using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_021_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_021;
    public List<string> saveKeys => new List<string>()
    {
        "opning",
        "box_dounatu",
        "get_gabyou",
        "use_gabyou",
        "open_door",
        "box_sara",
        "suidou_right",
        "suidou_left",
        "box_flyingPan",
        "get_flyingPan_before",
        "box_sponge",
        "get_sponge_before",
        "get_sponge_after",
        "use_sponge_after",
        "box_houcyou",
        "get_houcyou",
        "use_houcyou",
        "box_cheeze",
        "get_cheeze",
        "collection_makura",
        "collection_bed",
        "collection_book",
        "collection_sara",
        "collection_bin",
        "ending",
    };
}
