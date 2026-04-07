using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_024_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_024;
    public List<string> saveKeys => new List<string>()
    {
        "opning",
        "open_toyshop",
        "open_cartein",
        "box_500yen",
        "get_500yen",
        "box_pointCard",
        "get_pointCard",
        "box_stamp",
        "get_stamp",
        "use_stamp",
        "buy_miniracer",
        "talk_tanin",
        "get_reciept",
        "get_100yen",
        "use_100yen",
        "open_zenmaiUsePoint",
        "use_zenmai",
        "box_battery",
        "get_battery",
        "collectionItem_dog",
        "collectionItem_gacya",
        "collectionItem_nefuda",
        "collectionItem_star",
        "collectionItem_underBox",
        "ending",
    };
}
