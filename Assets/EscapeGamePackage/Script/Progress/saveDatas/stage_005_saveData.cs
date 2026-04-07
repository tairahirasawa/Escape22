using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class stage_005_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_005;
    public List<string> saveKeys => new List<string>()
    {
        "opening",
        "open_car_front",
        "get_bouenkyou",
        "use_bouenkyou",
        "open_tent",
        "open_ryukku_red",
        "get_scop",
        "use_scop",
        "get_bungalowKey",
        "use_bungalowKey",
        "open_ryukku_green",
        "get_mashumaro01",
        "open_car_back",
        "get_trump",
        "use_trump",
        "open_ryukku_blue",
        "get_mushimegane",
        "box_mashumaro02",
        "get_mashumaro02",
        "ending",
    };
}
