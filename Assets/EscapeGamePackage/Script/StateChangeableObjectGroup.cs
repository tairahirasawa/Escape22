using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class StateChangeableObjectGroup : MonoBehaviour
{
    public List<StateChangeableObject> stateChangeableObjects;
    public List<int> answerIndex;
    public List<int> indexes = new List<int>();
    public bool NeedZoom;

    public EventActionCreator eventAction;
    public EventActionCreator cancelAction;
    public bool IsEnd;

    private void Start()
    {
        for (int i = 0; i < stateChangeableObjects.Count; i++)
        {
            int fixedIndex = i;
            stateChangeableObjects[i].OnClickAction += () => AddIndex(fixedIndex);
            stateChangeableObjects[i].needZoom = NeedZoom;
        }

        StageSwitcher.I.mapManager.OnZoomOut += OnZoomOut;
    }

    private async Task Update()
    {
        if (IsEnd || !gameObject.activeSelf) return;


        if (indexes.Count > answerIndex.Count)
        {
            indexes.RemoveAt(0);
        }

        if (indexes.Count == answerIndex.Count)
        {
            for (int i = 0; i < answerIndex.Count; i++)
            {
                if (indexes[i] != answerIndex[i]) return;
            }
        }
        else
        {
            return;
        }

        await eventAction.EventAction();
        IsEnd = true;
        
    }

    public void AddIndex(int index)
    {
        if (IsEnd || !gameObject.activeSelf) return;
        indexes.Add(index);
    }

    public async UniTask OnZoomOut()
    {
        indexes.Clear();
        IsEnd = false;

        for (int i = 0; i < stateChangeableObjects.Count; i++)
        {
            stateChangeableObjects[i].stateType = StateChangeableObject.StateType.state_00;
        }

        if (cancelAction != null)
        {
            await cancelAction.EventAction();
        }

        await UniTask.Yield();
    }

}
