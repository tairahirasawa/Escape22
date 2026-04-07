using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class stage_004_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_004;
    public List<string> saveKeys => new List<string>()
    {
        "opening",
        "get_sissors",
        "use_sissors",
        "open_tent",
        "open_ryukku_red",
        "get_soap",
        "get_truribari",
        "get_nokogiri",
        "get_eda",
        "open_ryukku_blue",
        "get_ito",
        "make_turizao",
        "open_car",
        "get_bungalowKey",
        "use_bungalowKey",
        "get_sunglass",
        "get_yamame",
        "open_ryukku_green",
        "get_lighter",
        "ending",
    };
}
