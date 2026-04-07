using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapChangeJudge
{
    [SerializeField] public bool UseMapChangeJudge;
    [SerializeField] public MapChangeTrigger[] targetMap;

    public bool JudeTargetMap()
    {
        if (!UseMapChangeJudge) return true;

        for (int i = 0; i < targetMap.Length; i++)
        {
            if (targetMap[i].CanDifferenceMap)
            {
                if (StageSwitcher.I.mapManager.maps[targetMap[i].targetMap.name] == targetMap[i].targetMap
                    && StageSwitcher.I.mapManager.maps[targetMap[i].targetMap.name].currentIndexX == targetMap[i].targetIndexX
                    && StageSwitcher.I.mapManager.maps[targetMap[i].targetMap.name].currentIndexY == targetMap[i].targetIndexY)
                {
                    return true;
                }
            }
            else
            {

                if (StageSwitcher.I.mapManager.currentMap == targetMap[i].targetMap
                    && StageSwitcher.I.mapManager.currentMap.currentIndexX == targetMap[i].targetIndexX
                    && StageSwitcher.I.mapManager.currentMap.currentIndexY == targetMap[i].targetIndexY)
                {
                    return true;
                }
            }

        }

        return false;
    }
}


[Serializable]
public class MapChangeTrigger
{
    public Map targetMap;
    public int targetIndexX;
    public int targetIndexY;
    public bool CanDifferenceMap;

}
