using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using Cysharp.Threading.Tasks;
using System.Threading;
using JetBrains.Annotations;

public partial class GameData
{
    public bool blockAd;
    public bool IsCompleteEventAction;
}

public class ProgressDirector : SingletonMonoBehaviour<ProgressDirector>
{
    public TargetKey NowProgressKey;

    public TargetKey CurrentProgressKey;//EventActionが始まったら、ここが変わる
    //public List<ProgressSaveDatas> progressDatas;
    public Action OnDoneProgress;

    protected override void OnAwake()
    {
        StageContext.Initialize();
        SetProgressData();
    }

    public void SetProgressData()
    {
        if(StageContext.currentStageType == StageContext.StageType.none) return;
        ProgressSaveManger.LoadOrCreateProgressSaveData(StageContext.GetCurrentPrefix());
        /*
        for (int i = 0; i < ProgressSaveManger.progressSaveDatas.Count; i++)
        {
            for (int c = 0; c < ProgressSaveManger.progressSaveDatas[i].progressSaveDatas.Count; c++)
            {
                if (ProgressSaveManger.progressSaveDatas[i].progressSaveDatas[c].saveKey == "questStart") break;
                ProgressSaveManger.progressSaveDatas[i].progressSaveDatas[c].IsDone = true;
            }

        }
        */
        
    }

    private void Update()
    {
        NowProgressKey = GetNowProgressKey();
    }

    public ProgressData GetProgressSaveData(TargetKey progressKey)
    {
        if (progressKey == null) return null;

        var progressDatas = ProgressSaveManger.GetProgressData(progressKey.episodeType.ToString());

        for (int i = 0; i < progressDatas.Count; i++)
        {
            if (progressDatas[i].saveKey == progressKey.saveKey)
            {
                return progressDatas[i];
            }
        }

        /*
        var newData = new ProgressData(key);
        progressDatas.Add(newData);

        return newData;
        */

        return null;
    }

    public bool IsComplete()
    {
        if(StageContext.currentStageType == StageContext.StageType.none) return true;

        var progressDatas = ProgressSaveManger.GetProgressData(StageContext.currentStageType.ToString());

        for (int i = 0; i < progressDatas.Count; i++)
        {
            if (progressDatas[i].IsDone == false)
            {
                return false;
            }
        }

        return true;
    }
    
    public void DoneProgressFalse(TargetKey progressKey)
    {
        if (progressKey.episodeType == StageContext.StageType.none) return;
        var progressDatas = ProgressSaveManger.GetProgressData(progressKey.episodeType.ToString());

        var data = GetProgressSaveData(progressKey);
        if (data != null)
        {
            data.IsDone = false;
        }
    }

    public void DoneProgress(TargetKey progressKey)
    {
        if (progressKey.episodeType == StageContext.StageType.none) return;
        var progressDatas = ProgressSaveManger.GetProgressData(progressKey.episodeType.ToString());

        var data = GetProgressSaveData(progressKey);
        if (data != null)
        {
            data.IsDone = true;
            OnDoneProgress?.Invoke();
        }
    }

    public bool IsDoneProgress(TargetKey progressKey)
    {
        if (progressKey.episodeType == StageContext.StageType.none) return false;

        var progressDatas = ProgressSaveManger.GetProgressData(progressKey.episodeType.ToString());

        if (progressDatas == null || progressDatas.Count == 0) return false;

        if (GetProgressSaveData(progressKey) == null)
        {
            progressDatas.Add(new ProgressData(progressKey.saveKey));
        }

        return GetProgressSaveData(progressKey).IsDone;
    }

    public bool IsDoneProgressAny(TargetKey[] progressKeys)
    {
        for (int i = 0; i < progressKeys.Length; i++)
        {
            if (IsDoneProgress(progressKeys[i]))
            {
                return true;
            }
        }

        return false;
    }


    public bool IsDoneProgressBoth(TargetKey[] progressKeys)
    {
        for (int i = 0; i < progressKeys.Length; i++)
        {
            if (!IsDoneProgress(progressKeys[i]))
            {
                return false;
            }
        }
        return true;

    }

    public TargetKey GetNowProgressKey()
    {
        var progressDatas = ProgressSaveManger.GetProgressData(StageContext.currentStageType.ToString());

        for (int i = 0; i < progressDatas.Count; i++)
        {
            if (progressDatas[i].IsDone == false && !string.IsNullOrEmpty(progressDatas[i].saveKey))
            {
                var progresskey = new TargetKey()
                {
                    episodeType = StageContext.currentStageType,
                    saveKey = progressDatas[i].saveKey
                };

                return progresskey;
            }
        }

        return null;
    }


    public void DoneHint(int hintnum)
    {

        switch (hintnum)
        {
            case 1:
                GetProgressData(NowProgressKey).ShowedHint1 = true;
                break;

            case 2:
                GetProgressData(NowProgressKey).ShowedAnswer = true;
                break;

            default: break;
        }
    }

    public bool ShowedHintAnswer(int hintnum)
    {
        if (NowProgressKey == null) return false;
        if (NowProgressKey.episodeType == StageContext.StageType.none) return false;

        if (GetProgressData(NowProgressKey) == null)
        {
            return false;
        }

        switch (hintnum)
            {
                case 1:
                    return GetProgressData(NowProgressKey).ShowedHint1;

                case 2:
                    return GetProgressData(NowProgressKey).ShowedAnswer;

                default: return false;
            }
    }

    public ProgressData GetProgressData(TargetKey progressKey)
    {
        var progressDatas = ProgressSaveManger.GetProgressData(progressKey.episodeType.ToString());

        for (int i = 0; i < progressDatas.Count; i++)
        {
            if (progressDatas[i].saveKey == progressKey.saveKey)
            {
                return progressDatas[i];
            }
        }

        return progressDatas[0];
    }

    public int GetProgressKeyIndex(TargetKey progressKey)
    {
        if (progressKey == null) return -1;

        var allProgressSaveDataList = new AllProgressSaveDatalist();

        for (int i = 0; i < allProgressSaveDataList.progressSaveDatas.Count; i++)
        {
            if (allProgressSaveDataList.progressSaveDatas[i].prefix != progressKey.episodeType) continue;

            return allProgressSaveDataList.progressSaveDatas[i].saveKeys.IndexOf(progressKey.saveKey);
        }

        return -1;
    }

}
