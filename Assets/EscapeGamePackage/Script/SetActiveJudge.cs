using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SetActiveJudge
{
    [SerializeField] public bool UseSetActiveJudge;
    [SerializeField] public List<SetActiveSet> setActiveSets;


    public bool setActiveJudge()
    {
        if (!UseSetActiveJudge) return true;

        for (int i = 0; i < setActiveSets.Count; i++)
        {
            if (setActiveSets[i].target.activeSelf != setActiveSets[i].setActive)
            {
                return false;
            }
        }

        return true;

    }
}

[Serializable]
public class SetActiveSet
{
    public GameObject target;
    public bool setActive;
}
