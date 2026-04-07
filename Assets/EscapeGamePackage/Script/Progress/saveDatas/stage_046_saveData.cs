using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_046_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_046;
    public List<string> saveKeys => new List<string>()
    {
        "opning",
        "on_patocarlamp",
        "box_title",
        "open_policeBox",
        "open_window",
        "box_sissorss",
        "get_sissorss",
        "use_sissorss",
        "box_locker",
        "box_magnet",
        "get_magnet",
        "use_magnet",
        "get_100yen",
        "use_100yen",
        "box_kama",
        "get_kama",
        "use_kama",
        "get_pistol",
        "use_pistol",
        "ending",
    };
}
