using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningMark : MonoBehaviour
{
    public GameObject warningMark;

    public ProgressSwitch startProgress;
    public ProgressSwitch endProgress;

    private void Awake()
    {
        warningMark.SetActive(false);

    }

    void Update()
    {
        if(endProgress.JudgeSwitch())
        {
            warningMark.SetActive(false);
            return;
        }

        if(startProgress.JudgeSwitch())
        {
            warningMark.SetActive(true);
        }
        
    }
}
