using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TimeChangeButton : MonoBehaviour,IPointerClickHandler
{
    public int Index;

    public List<GameObject> timeTexts;


    void Start()
    {
        if (EnvironmentalManager.I.currentTimeType == timeType.day) Index = 0;
        if (EnvironmentalManager.I.currentTimeType == timeType.evening) Index = 1;
        if (EnvironmentalManager.I.currentTimeType == timeType.night) Index = 2;

        for (int i = 0; i < timeTexts.Count; i++)
        {
            timeTexts[i].SetActive(i == Index);
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Index++;

        if (Index > 2) Index = 0;

        if (Index == 0)
        {
            EnvironmentalManager.I.ChangeWhetherAndTime(EnvironmentalManager.I.currentWeatherType, timeType.day);
        }

        if (Index == 1)
        {
            EnvironmentalManager.I.ChangeWhetherAndTime(EnvironmentalManager.I.currentWeatherType, timeType.evening);
        }

        if (Index == 2)
        {
            EnvironmentalManager.I.ChangeWhetherAndTime(EnvironmentalManager.I.currentWeatherType, timeType.night);
        }

        for (int i = 0; i < timeTexts.Count; i++)
        {
            timeTexts[i].SetActive(i == Index);
        }
    }

}
