using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_010_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_010;
    public List<string> saveKeys => new List<string>()
    {
        "opening",
        "open_fune",
        "get_hoshinosuna_01",
        "get_hoshinosuna_02",
        "open_windowShutter",
        "box_kumade",
        "get_kumade",
        "use_kumade",
        "open_villaDoor",
        "get_hoshinosuna_03",
        "box_ami",
        "get_ami",
        "use_ami",
        "use_mendako",
        "box_oysterKnife",
        "get_oysterKnife",
        "get_hoshinosuna_04",
        "box_mushimegane",
        "get_mushimegane",
        "get_hoshinosuna_05",
        "ending",
    };
}
