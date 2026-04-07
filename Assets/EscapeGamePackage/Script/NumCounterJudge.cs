using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NumCounterJudge
{
    [SerializeField] public bool UseNumCounterJudge;
    [SerializeField] public CounterJudgeType type;
    [SerializeField] public NumCounter numCounter;
    [SerializeField] public int targetNum;

    public bool numCounterJudge()
    {
        if (!UseNumCounterJudge) return true;

        switch(type)
        {
            case CounterJudgeType.equal:
                return targetNum == numCounter.counter;

            case CounterJudgeType.OrMore:
                return  numCounter.counter >= targetNum;

            case CounterJudgeType.OrLess:
                return  numCounter.counter <= targetNum ;
        }

        return false;

    }

    
    public enum CounterJudgeType
    {
        equal,OrMore,OrLess
    }
}
