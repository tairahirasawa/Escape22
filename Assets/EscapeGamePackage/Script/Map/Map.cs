using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class GameData
{
    //public List<MapInfo> S_MapInfos;
}


[Serializable]
public class MapInfo
{
    public string mapName;
    public int currentIndexX;
    public int currentIndexY;

    public MapInfo(string mapName)
    {
        this.mapName = mapName;
    }

}

[Serializable]
public class PreviousMapInfo
{
    public Map previousMap;

    [Header("前のインデックスに戻る場合は999を指定、その場合はマップを保存しないように注意")]
    public int previousMapIndex;
}


public class Map : MonoBehaviour
{
    //public List<MapInfo> mapInfos { get => DataPersistenceManager.I.gameData.S_MapInfos; set => DataPersistenceManager.I.gameData.S_MapInfos = value; }
    public MapInfo myMapInfo;
    public int currentIndexX;
    public int currentIndexY;

    public int cameraPosXIndex_max;
    public int cameraPosXIndex_min;
    public int cameraPosYIndex_max;
    public int cameraPosYIndex_min;



    public PreviousMapInfo previousMapInfo;

    public bool DiasbleBackButton;
    public GameObject[] ObjectToDeactivate;

    public bool DontLoadThisMap;

    private void Awake()
    {
        LoadMapInfo();
    }

    private void LoadMapInfo()
    {
        /*
        if (DataPersistenceManager.I.gameData.S_MapInfos == null)
        {
            DataPersistenceManager.I.gameData.S_MapInfos = new List<MapInfo>();
        }
        */
        
        bool already = false;
        var mapInfos = ProgressSaveManger.GetMapInfos(StageContext.currentStageType.ToString());

        if(mapInfos == null) return;

        for (int i = 0; i < mapInfos.Count; i++)
        {
            if (mapInfos[i].mapName == gameObject.name)
            {
                myMapInfo = mapInfos[i];
                currentIndexX = myMapInfo.currentIndexX;
                currentIndexY = myMapInfo.currentIndexY;
                already = true;
                break;
            }
        }

        if(already == false)
        {
            mapInfos.Add(myMapInfo = new MapInfo(gameObject.name));
        }

    }

    public void PresentMap()
    {
        for (int i = 0; i < ObjectToDeactivate.Length; i++)
        {
            ObjectToDeactivate[i].SetActive(true);
        }

        //transform.localScale = Vector3.one;
    }

    public void HideMap()
    {
        for (int i = 0; i < ObjectToDeactivate.Length; i++)
        {
            ObjectToDeactivate[i].SetActive(false);
        }
        

        //transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        myMapInfo.currentIndexX = currentIndexX;
        myMapInfo.currentIndexY = currentIndexY;

    }

    public void AddCameraPosXIndex(int num)
    {
        currentIndexX += num;
        currentIndexX = Math.Clamp(currentIndexX, cameraPosXIndex_min, cameraPosXIndex_max);
    }

    public void AddCameraPosYIndex(int num)
    {
        currentIndexY += num;
        currentIndexY = Math.Clamp(currentIndexY, cameraPosYIndex_min, cameraPosYIndex_max);
    }

    public bool EnableLeftMove()
    {
        return currentIndexX > cameraPosXIndex_min;
    }

    public bool EnableRightMove()
    {
        return currentIndexX < cameraPosXIndex_max;
    }

    public bool EnableUpMove()
    {
        return currentIndexY < cameraPosYIndex_max;
    }

    public bool EnableDownMove()
    {
        return currentIndexY > cameraPosYIndex_min;
    }



    public bool CanMoveCam()
    {
        if (currentIndexX > cameraPosXIndex_min && currentIndexX < cameraPosXIndex_max)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
