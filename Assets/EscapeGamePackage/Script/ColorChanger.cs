using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public colorType type;
    private SpriteRenderer sp;

    void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        switch (type)
        {
            case colorType.window:
            
                    if (EnvironmentalManager.I.currentTimeType == timeType.night)
                    {
                        sp.color = DataBase.I.windowColorData.nightWindowColor;
                    }
                    else
                    {
                        sp.color = DataBase.I.windowColorData.dayWindowColor;
                    }

                break;

            case colorType.doorOpen:
 
                    if (EnvironmentalManager.I.currentTimeType == timeType.night)
                    {
                        sp.color = DataBase.I.windowColorData.nightDoorOpenColor;
                    }
                    else
                    {
                        sp.color = DataBase.I.windowColorData.dayDoorOpenColor;
                    }
                break;
        }


    }

    public enum colorType
    {
        window,doorOpen
    }
}
