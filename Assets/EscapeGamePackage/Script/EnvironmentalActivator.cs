using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalActivator : MonoBehaviour
{
    public List<EnvironmentalActivatorSetting> environmentalActivatorSettings;

    void Update()
    {
        for (int i = 0; i < environmentalActivatorSettings.Count; i++)
        {
            environmentalActivatorSettings[i].ObjectJudge();
        }

    }
}

[Serializable]
public class EnvironmentalActivatorSetting
{
    public JudgeType judgeType;
    public List<weatherType> weatherTypes;
    public List<timeType> timeTypes;
    public List<GameObject> targetObjects;

    public void ObjectJudge()
    {
        if (judgeType == JudgeType.both)
        {
            for (int i = 0; i < targetObjects.Count; i++)
            {
                targetObjects[i].SetActive(TimeJude() && WeatherJudge());
            }
        }
        else
        {
            for (int i = 0; i < targetObjects.Count; i++)
            {
                targetObjects[i].SetActive(TimeJude() || WeatherJudge());
            }
        }

    }

    public bool TimeJude()
    {
        if (timeTypes[0] == timeType.none) return true;

        if (timeTypes.Contains(EnvironmentalManager.I.currentTimeType)) return true;
        else return false;
    }

    public bool WeatherJudge()
    {
        if (weatherTypes[0] == weatherType.none) return true;

        if (weatherTypes.Contains(EnvironmentalManager.I.currentWeatherType)) return true;
        else return false;
    }

    public enum JudgeType
    {
        both,any
    }
}