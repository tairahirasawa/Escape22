using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class EventActionTrigger
{
    [SerializeField] public EventActionTriggerType triggerType;
    [SerializeField] public bool needZoom;
    [SerializeField] public bool always;
    [SerializeField] public bool dontStopIsDoneProgress;

    [SerializeField] public bool presentUnDoneMessage;
    [SerializeField] public MessageEventAction UnDoneMessage;
    
    [SerializeField] public bool needItem;
    [SerializeField] public bool DontDeleteUseItem;

    [SerializeField] public string[] needItemTypes;
    [SerializeField] public ProgressSwitch targetSetting;
    [SerializeField] public bool DontStartNotShowStageScreen;

    [SerializeField] public Gimmick gimmick;

    [SerializeField] public MapChangeJudge targetMap;


}

public enum EventActionTriggerType
{
    None, ProgressEnd, ProgressStart,Gimmick,PointerClick, PointerDown, PointerUp,GameStart,ZoomIn,ZoomOut,Initialize,Loop,MapChange,Complete
}