using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_030_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_030;
    public List<string> saveKeys => new List<string>()
    {
        "opning",
        "box_zoukin",
        "get_zoukin",
        "use_zoukin",
        "box_sougankyou",
        "get_sougankyou",
        "box_yukidaruma",
        "box_mafuler",
        "get_mafuler",
        "use_mafuler",
        "get_tebukuro",
        "get_yakan",
        "box_turizao",
        "get_turizao",
        "box_alphabet",
        "box_scop",
        "get_scop",
        "use_scop",
        "use_turizao",
        "use_yakan",
        "collectionItem_poket",
        "collectionItem_snowTree",
        "collectionItem_window",
        "collectionItem_behindHouse",
        "collectionItem_behindRabbit",
        "ending",
    };
}
