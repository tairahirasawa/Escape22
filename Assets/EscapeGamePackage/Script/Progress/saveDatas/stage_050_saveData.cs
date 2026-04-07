using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_050_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_050;
    public List<string> saveKeys => new List<string>()
    {
        "opening",
        "open_karaokeroom_01",
        "box_wallPanel",
        "on_denmoku",
        "fill_juice",
        "open_toilet",
        "on_coffeeMaker",
        "box_coupon",
        "fill_coffee_01",
        "fill_coffee_02",
        "box_zoukin",
        "get_zoukin",
        "use_zoukin",
        "open_karaokeroom_02",
        "on_monitar",
        "box_houcyou",
        "get_houcyou",
        "use_houcyou",
        "get_mike",
        "ending", 
    };
}
