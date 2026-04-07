using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_048_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_048;
    public List<string> saveKeys => new List<string>()
    {
        "box_remokon_eakon",
        "on_light",
        "on_remokon_eakon",
        "box_guitar",
        "box_mike",
        "box_kaicyudentou",
        "get_kaicyudentou",
        "box_battery",
        "get_battery",
        "use_battery",
        "on_monitar",
        "box_pick",
        "get_pick",
        "on_denmoku",
        "box_guitar_string",
        "get_guitar_string",
        "use_guitar_string",
        "box_marakasu",
        "unlock_door",
        "ending",
    };
}
