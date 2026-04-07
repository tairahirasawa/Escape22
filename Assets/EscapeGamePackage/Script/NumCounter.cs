using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NumCounter : MonoBehaviour
{
    public int counter;
    public int targetCount;
    public EventActionCreator eventaction;

    public async UniTask EventCheck()
    {
        if (eventaction == null) return;

        if(counter == targetCount)
        {
            await eventaction.EventAction();
        }
    }

}
