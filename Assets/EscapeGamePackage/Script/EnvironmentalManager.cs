using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameData
{
    public weatherType currentWeatherType;
    public timeType currentTimeType;
}

public class EnvironmentalManager : SingletonMonoBehaviour<EnvironmentalManager>
{
    public GameObject allcontents;
    public GameObject rain;
    public GameObject sunnySky_day;
    public GameObject sunnySky_evening;
    public GameObject sunnySky_night;
    public GameObject rainySky_day;
    public GameObject rainySky_evening;
    public GameObject rainySky_night;

    public List<Map> applyMaps;

    public weatherType currentWeatherType { get => DataPersistenceManager.I.gameData.currentWeatherType; set => DataPersistenceManager.I.gameData.currentWeatherType = value; }
    public timeType currentTimeType { get => DataPersistenceManager.I.gameData.currentTimeType; set => DataPersistenceManager.I.gameData.currentTimeType = value; }

    protected override void OnAwake()
    {
        if (currentWeatherType == weatherType.none) currentWeatherType = weatherType.sunny;
        if (currentTimeType == timeType.none) currentTimeType = timeType.day;

        
    }

    protected override void OnStart()
    {
        ChangeWhetherAndTime(currentWeatherType, currentTimeType);
    }

    private void Update()
    {
        allcontents.SetActive(applyMaps.Contains(StageSwitcher.I.mapManager.visibleMap));
    }

    public void UpdateAllContents()
    {
        allcontents.SetActive(!applyMaps.Contains(StageSwitcher.I.mapManager.visibleMap));
    }

    public void ChangeWhetherAndTime(weatherType weatherType, timeType timeType)
    {
        rain.SetActive(false);
        sunnySky_day.SetActive(false);
        sunnySky_evening.SetActive(false);
        sunnySky_night.SetActive(false);
        rainySky_day.SetActive(false);
        rainySky_evening.SetActive(false);
        rainySky_night.SetActive(false);

        if (weatherType == weatherType.sunny)
        {
            AudioDirector.I.bgmsettings[AudioDirector.BGMType.Rain].stopAllBGM();
            
            switch (timeType)
            {
                case timeType.day:
                    sunnySky_day.SetActive(true);
                    break;

                case timeType.evening:
                    sunnySky_evening.SetActive(true);
                    break;

                case timeType.night:
                    sunnySky_night.SetActive(true);
                    break;

            }
        }

        if (weatherType == weatherType.rainy)
        {
            AudioDirector.I.bgmsettings[AudioDirector.BGMType.Rain].playAllBGM();
            switch (timeType)
            {
                case timeType.day:
                    rainySky_day.SetActive(true);
                    break;

                case timeType.evening:
                    rainySky_evening.SetActive(true);
                    break;

                case timeType.night:
                    rainySky_night.SetActive(true);
                    break;

            }

        }

        rain.SetActive(weatherType == weatherType.rainy);
        currentWeatherType = weatherType;
        currentTimeType = timeType;
    }
}

public enum weatherType
{
    none, sunny, rainy
}

public enum timeType
{
    none, day, evening, night
}