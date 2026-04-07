using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_040_saveData : IProgressSaveData
{
    public StageContext.StageType prefix => StageContext.StageType.stage_040;
    public List<string> saveKeys => new List<string>()
    {
        "opening",
        "fall_paper",
        "box_cord",
        "box_sissors",
        "get_sissors",
        "use_sissors",
        "open_sideDisplay",
        "open_topDisplay",
        "box_driverSentan",
        "get_dirverSentan",
        "box_driverMochite",
        "get_driverMochite",
        "create_driver",
        "box_smartphone",
        "box_lightSwitch",
        "box_usbCable",
        "get_usbCable",
        "use_usbCable",
        "box_phoneButton",
        "ending",
    };
}
