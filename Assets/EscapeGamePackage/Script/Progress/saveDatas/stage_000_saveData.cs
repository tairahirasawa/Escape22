using System.Collections.Generic;

public class stage_000_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_000;
    public List<string> saveKeys => new List<string>()
    {
        "open_window",
        "open_sugarCubeBox",
        "get_sugarCube",
        "use_sugerCube",
        "open_suitou",
        "get_suidouHandle",
        "use_suidouHandle",
        "open_toilet",
        "get_cutter",
        "use_cutter",
        "open_suihanki",
        "open_furoba",
        "open_conroTana",
        "open_araibaTana_left",
        "get_pinset",
        "get_emptyCan",
        "get_battery",
        "use_battery",
        "einding",
    };
}
