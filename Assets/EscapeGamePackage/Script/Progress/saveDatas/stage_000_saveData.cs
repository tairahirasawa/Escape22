using System.Collections.Generic;

public class stage_000_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_000;
    public List<string> saveKeys => new List<string>()
    {
        "opening",
        "open_door",
        "box_watorserver",
        "fill_wator",
        "box_renge",
        "get_renge",
        "on_denki",
        "open_nabe",
        "box_menu",
        "box_tong",
        "get_tong",
        "use_tong",
        "on_fire",
        "box_tonkachi",
        "get_tonkachi",
        "use_tonkachi",
        "get_waribashi",
        "use_waribashi",
        "get_500Yen",
        "ending",
    };
}
