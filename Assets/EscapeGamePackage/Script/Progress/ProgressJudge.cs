using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ProgressJudge
{
    [SerializeField] public bool UseProgressSwitch;
    [SerializeField] public ProgressSwitch startSwitch;
    [SerializeField] public ProgressSwitch endSwitch;

    //trueなら通す。
    public bool EnableAction()
    {
        if (!UseProgressSwitch) return true;
        if (IsCreateSwitch(startSwitch) && IsCreateSwitch(endSwitch))
        {
            return startSwitch.JudgeSwitch() && !endSwitch.JudgeSwitch(); 
        }

        if (IsCreateSwitch(startSwitch))
        {

            return startSwitch.JudgeSwitch();
        }

        if (IsCreateSwitch(endSwitch))
        {

            return !endSwitch.JudgeSwitch();
        }
        
        return true;
    }

    public bool IsCreateSwitch(ProgressSwitch progressSwitch)
    {
        if (progressSwitch.progressKeys != null)
        {
            if (progressSwitch.progressKeys.Length != 0)
            {
                return true;
            }
        }
        
        return false;
    }
}

[Serializable]
public class ProgressSwitch
{
    public JudgeType judgeType;
    public TargetKey[] progressKeys;

    public bool JudgeSwitch()
    {
        switch (judgeType)
        {
            case JudgeType.any:

                return ProgressDirector.I.IsDoneProgressAny(progressKeys);

            case JudgeType.both:

                return ProgressDirector.I.IsDoneProgressBoth(progressKeys);

            case JudgeType.none:

                return true;

            default: return false;
        }
    }

    public enum JudgeType
    {
        none, any, both
    }


}
