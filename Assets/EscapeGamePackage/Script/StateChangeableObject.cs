using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class StateChangeableObject : MonoBehaviour,IPointerClickHandler
{
    public GameObject state_00;
    public GameObject state_01;

    public StateType stateType;
    public AudioDirector.SeType seType;

    public bool needZoom;
    public bool IsLock;
    public bool KeepOpen;
    public bool AutoReturn;
    public int AutoReturnTime;

    private Collider2D col;

    public List<string> NeedItemType;

    public Action OnClickAction;

    public async void OnClickAfterUnLockAction()
    {
        await ChangeState();
    }

    private void Awake()
    {
        stateType = StateType.state_00;
        col = GetComponent<Collider2D>();
    }


    public void Update()
    {
        state_00.SetActive(stateType == StateType.state_00);
        state_01.SetActive(stateType == StateType.state_01);

        if (KeepOpen)
        {
            if (stateType == StateType.state_01)
            {
                col.enabled = false;
            }
            else
            {
                col.enabled = true;
            }
        }
    }

    public async Task ChangeState()
    {
        if (NeedItemType != null && NeedItemType.Count != 0)
        {
            if (!NeedItemType.Contains(ItemManager.I.currentItem))
            {
                return;
            }
        }

        switch (stateType)
            {
                case StateType.state_00:
                    stateType = StateType.state_01;
                    AudioDirector.I.PlaySE(seType);

                    if (AutoReturn)
                    {
                        await UniTask.Delay(AutoReturnTime * 1000);
                        stateType = StateType.state_00;
                    }

                    break;

                case StateType.state_01:
                    stateType = StateType.state_00;
                    AudioDirector.I.PlaySE(seType);
                    break;
            }
    }



    public void LoadOpen()
    {
        KeepOpen = true;
        stateType = StateType.state_01;
    }

    public async void OnPointerClick(PointerEventData eventData)
    {
        if (AutoReturn && stateType == StateType.state_01) return;

        OnClickAction?.Invoke();
        await ChangeState();

    }

    public enum StateType
    { 
       state_00,state_01,
    }
}
