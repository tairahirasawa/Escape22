using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.IO;
using TMPro;

public class DataPersistenceManager : SingletonMonoBehaviour<DataPersistenceManager>
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    public GameData gameData;

    protected override void OnAwake()
    {
        DontDestroyEnabled = true;
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
 
    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {

        this.gameData = dataHandler.Load();

        if(this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }

    }

    public void SaveGame()
    {
        dataHandler.Save(gameData);
        Debug.Log(Application.persistentDataPath);
    }



    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }


    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void OnApplicationPause(bool pause)
    {
        SaveGame();
    }

    private void OnApplicationFocus(bool focus)
    {
        SaveGame();
    }
}
