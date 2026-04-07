using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_035_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_035;
    public List<string> saveKeys => new List<string>()
    {
        "opning",
        "cartain_open",
        "door_magicRoom",
        "box_watorMelon",
        "cut_watorMelon",
        "box_magicWand",
        "get_magicWand",
        "meet_magicGirl",
        "On_electric",
        "door_bukiko",
        "box_lighter",
        "get_lighter",
        "box_taihou",
        "box_houdan",
        "set_houdan",
        "fire_taihou",
        "open_nabe",
        "get_truthDrink",
        "use_truthDrink",
        "collectionItem_bookShelf",
        "collectionItem_bookMajicHat",
        "collectionItem_Shield",
        "collectionItem_behindFuda",
        "collectionItem_taihou",
        "ending",
    };
}
