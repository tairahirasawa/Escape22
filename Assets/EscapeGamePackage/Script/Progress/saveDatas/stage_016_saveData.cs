using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_016_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_016;
    public List<string> saveKeys => new List<string>()
    {
        "opening",
        "open_numberPanel",
        "open_colorPanel",
        "box_sissors",
        "get_sissors",
        "use_sissors",
        "box_nokogiri",
        "get_nokogiri",
        "use_nokogiri",
        "open_inugoya",
        "open_uraguchi",
        "get_sunglass",
        "box_magicHand",
        "get_magicHand",
        "get_shose",
        "box_kama",
        "get_kama",
        "get_susuki",
        "ending",
    };
}
