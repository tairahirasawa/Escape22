using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_028_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_028;
    public List<string> saveKeys => new List<string>()
    {
        "opning",
        "open_door",
        "box_kakinotane",
        "appear_kakejiku",
        "box_fude",
        "get_fude",
        "box_goishi",
        "use_fude",
        "box_houki",
        "get_houki",
        "use_houki",
        "open_uraguchi",
        "box_gunte",
        "get_gunte",
        "use_gunte",
        "use_satumaimo",
        "box_takuwan",
        "box_lighter",
        "get_lighter",
        "collectionItem_kakejiku",
        "collectionItem_goke",
        "collectionItem_ochiba",
        "collectionItem_hatake",
        "collectionItem_houki",
        "ending",
    };
}
