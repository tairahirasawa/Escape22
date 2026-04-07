using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveJudgeLoop : MonoBehaviour
{

    public SetActiveJudge setActiveJudge;
    public EventActionCreator eventActionCreator;

    private bool IsDone;

    private void Start()
    {
        if(ProgressDirector.I.IsDoneProgress(eventActionCreator.eventActionConfig.progressKey))
        {
            IsDone = true;

            for (int i = 0; i < setActiveJudge.setActiveSets.Count; i++)
            {
                setActiveJudge.setActiveSets[i].target.SetActive(setActiveJudge.setActiveSets[i].setActive);
            } 
        }
    }

    void Update()
    {
        if (ProgressDirector.I.IsDoneProgress(eventActionCreator.eventActionConfig.progressKey)) return;

        if(setActiveJudge.setActiveJudge() && !IsDone)
        {
            eventActionCreator.EventAction().Forget();
            IsDone = true;
        }

    }
}
