using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_026_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_026;
    public List<string> saveKeys => new List<string>()
    {
        "opning",
        "box_cars",
        "box_hairPin",
        "get_hairPin",
        "use_hairPin",
        "open_door",
        "box_arrowButton",
        "box_ufo",
        "box_daruma",
        "box_hummer",
        "get_hummer",
        "box_gacyadama",
        "box_hitofudegaki",
        "box_airPlane",
        "get_airPlane",
        "awake_hook",
        "use_airPlane",
        "get_kujibikiTicket",
        "get_yo-yo-",
        "collectionItem_Star",
        "collectionItem_jidouhanbaiki",
        "collectionItem_pocket",
        "collectionItem_toyshop",
        "collectionItem_kujibiki",
        "ending",
    };
}
