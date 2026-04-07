using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_038_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_038;
    public List<string> saveKeys => new List<string>()
    {
        "opening",
        "on_radicase",
        "open_cartein",
        "box_sougankyou",
        "get_sougankyou",
        "box_hotPlate",
        "get_hotPlate",
        "box_hotPlate_Tumami",
        "get_hotPlate_Tumami",
        "use_hotPlate_Tumami",
        "box_houchou",
        "get_houchou",
        "box_kyabetu",
        "use_houcyou",
        "box_butaniku",
        "get_butaniku",
        "loast_butaniku",
        "box_hera",
        "get_hera",
        "collectionItem_conro",
        "collectionItem_hotplate",
        "collectionItem_calender",
        "collectionItem_danbowl",
        "collectionItem_sara",
        "ending",
    };
}
