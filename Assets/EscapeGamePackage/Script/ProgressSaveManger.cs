using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JetBrains.Annotations;
using UnityEngine;

public partial class GameData
{
    public List<ProgressSaveDatas> S_progressSaveDatas;
}

public static class ProgressSaveManger
{
    public static List<ProgressSaveDatas> progressSaveDatas { get => DataPersistenceManager.I.gameData.S_progressSaveDatas; set => DataPersistenceManager.I.gameData.S_progressSaveDatas = value; }

    public static ProgressSaveDatas LoadOrCreateProgressSaveData(string prefix)
    {
        if (progressSaveDatas == null) progressSaveDatas = new List<ProgressSaveDatas>();

        // 既存のデータを検索
        foreach (var data in progressSaveDatas)
        {
            if (data.prefix == prefix)
            {
                return data;
            }
        }

        // なければ新規作成して追加
        var newData = new ProgressSaveDatas(prefix);

        progressSaveDatas.Add(newData);

        return newData;
    }

    
    public static ProgressSaveDatas GetProgressDatas(string prefix)
    {
        if (progressSaveDatas == null)
        {
            return LoadOrCreateProgressSaveData(prefix);
        }

        for (int i = 0; i < progressSaveDatas.Count; i++)
            {
                if (progressSaveDatas[i].prefix == prefix)
                {
                    return progressSaveDatas[i];
                }
            }

        return LoadOrCreateProgressSaveData(prefix);
    }

    public static List<ProgressData> GetProgressData(string prefix)
    {
        if (progressSaveDatas == null) return null;

        for (int i = 0; i < progressSaveDatas.Count; i++)
        {
            if (progressSaveDatas[i].prefix == prefix)
            {
                return progressSaveDatas[i].progressSaveDatas;
            }
        }

        return null;
    }

    public static bool IsAlreadyPlay(string prefix)
    {
        if (progressSaveDatas == null) return false;

        var datas = GetProgressData(prefix);

        for (int i = 0; i < datas.Count; i++)
        {
            if (datas[i].IsDone)
            {
                GetProgressDatas(prefix).IsUnLockStage = true;
                return true;
            }
        }

        return false;
    }

    public static int GetClearCount()
    {
        if (progressSaveDatas == null) return 0;

        var total = 0;

        for (int i = 0; i < progressSaveDatas.Count; i++)
        {
            if (progressSaveDatas[i].IsClear)
            {
                total ++;
            }
        }

        return total;
    }


    public static List<ItemConfig> GetItems(string prefix)
    {
        if (progressSaveDatas == null) return null;

        for (int i = 0; i < progressSaveDatas.Count; i++)
        {
            if (progressSaveDatas[i].prefix == prefix)
            {
                return progressSaveDatas[i].items;
            }
        }

        return null;
    }

    public static List<MapInfo> GetMapInfos(string prefix)
    {
        if (progressSaveDatas == null) return null;

        for (int i = 0; i < progressSaveDatas.Count; i++)
        {
            if (progressSaveDatas[i].prefix == prefix)
            {
                if (progressSaveDatas[i].mapInfos == null)
                {
                    progressSaveDatas[i].mapInfos = new List<MapInfo>();
                }

                return progressSaveDatas[i].mapInfos;
            }
        }

        return null;
    }

    public static bool IsClearStage(string prefix)
    {
        if (GetProgressDatas(prefix) == null) return false;
        return GetProgressDatas(prefix).IsClear;
    }

    public static bool IsUnlockStage(string prefix)
    {
        if (GetProgressDatas(prefix) == null) return false;
        return GetProgressDatas(prefix).IsUnLockStage;
    }

    public static void EndStage()
    {
        for (int i = 0; i < progressSaveDatas.Count; i++)
        {
            if (progressSaveDatas[i].prefix == StageContext.currentStageType.ToString())
            {
                progressSaveDatas[i].IsClear = true;
            }
        }
    }
}

[Serializable]
public class TargetKey
{
    public StageContext.StageType episodeType;
    public string saveKey;
}

[Serializable]
public class ProgressSaveDatas
{
    public string prefix;
    public bool IsClear;
    public bool IsUnLockStage;
    public List<ProgressData> progressSaveDatas;
    public List<ItemConfig> items;
    public string currentMapName;//ここに今のマップネームを保存
    public List<MapInfo> mapInfos;

    public bool interAdShowed;//アイテムゲットのインタースティシャル用

    public ProgressSaveDatas(string prefix)
    {
        this.prefix = prefix;
        var datas = new AllProgressSaveDatalist();
        progressSaveDatas = datas.GetProgressSaveData(prefix);
        items = new List<ItemConfig>();
    }

    public void ResetData()
    {
        if(progressSaveDatas != null) progressSaveDatas.Clear();

        var datas = new AllProgressSaveDatalist();
        progressSaveDatas = datas.GetProgressSaveData(prefix);

        if(items != null) items.Clear();
        if(mapInfos != null) mapInfos.Clear();
        currentMapName = null;

        interAdShowed = false;

    }
}


[Serializable]
public class ProgressData
{
    public string saveKey;
    public bool IsDone;

    public bool AdFinish;
    public bool ShowedHint1;
    public bool ShowedAnswer;

    public ProgressData(string key)
    {
        saveKey = key;
    }
}

