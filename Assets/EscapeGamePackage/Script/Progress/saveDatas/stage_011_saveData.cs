using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_011_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_011;
    public List<string> saveKeys => new List<string>()
    {
        "opening",
        "box_turizao",
        "get_turizao",
        "use_turizao",
        "open_villa",
        "get_encyoCord",
        "use_encyoCord",
        "open_funeDoor",
        "box_palasol",
        "get_parasol",
        "use_parasol",
        "open_drinkShop",
        "box_apple",
        "get_apple",
        "open_funeWindow",
        "box_scop",
        "get_scop",
        "use_scop",
        "get_melon",
        "box_peach",
        "get_peach",
        "einding",
    };
}
