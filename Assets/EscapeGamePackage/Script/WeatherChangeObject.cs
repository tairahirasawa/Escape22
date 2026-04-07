using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeatherChangeObject : MonoBehaviour, IPointerClickHandler
{
    public GameObject SunnySymbol;
    public GameObject rainySymbol;

    public bool needZoom;

    private void Start()
    {
        SunnySymbol.SetActive(EnvironmentalManager.I.currentWeatherType == weatherType.sunny);
        rainySymbol.SetActive(EnvironmentalManager.I.currentWeatherType == weatherType.rainy);
    }

    private void Update()
    {
        
    }

    public void ChangeWeather(weatherType weatherType)
    {
        SunnySymbol.SetActive(weatherType == weatherType.sunny);
        rainySymbol.SetActive(weatherType == weatherType.rainy);

        EnvironmentalManager.I.ChangeWhetherAndTime(weatherType,EnvironmentalManager.I.currentTimeType);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (needZoom)
        {
            if (!StageSwitcher.I.mapManager.IsZoom) return;
        }

        AudioDirector.I.PlaySE(AudioDirector.SeType.gimmick);
        
        if (EnvironmentalManager.I.currentWeatherType == weatherType.sunny)
        {
            ChangeWeather(weatherType.rainy);
        }
        else if (EnvironmentalManager.I.currentWeatherType == weatherType.rainy)
        {
            ChangeWeather(weatherType.sunny);
        }

    }
}
