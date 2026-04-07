using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

public class StageSwitcher : SingletonMonoBehaviour<StageSwitcher>
{
    public SerializableDictionaryBase<StageContext.StageType, GameObject> stages;
    public GameObject currentStageObject;
    public MapManager mapManager;

    public void SetStage(StageContext.StageType stageType)
    {
        foreach (var stage in stages)
        {
            stage.Value.SetActive(false);
        }

        stages[stageType].SetActive(true);

        currentStageObject = stages[stageType];
        mapManager = stages[stageType].GetComponent<MapManager>();
    }
}