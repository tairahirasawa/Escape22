using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisablePanel : SingletonMonoBehaviour<DisablePanel>
{
    private Image image;
    public bool force;

    public bool DisableDisablePanel;

    protected override void OnAwake()
    {
        image = GetComponent<Image>();
        image.raycastTarget = false;
    }

    public void Disable()
    {
        if (!force)
        {
            image.raycastTarget = true;

        }
    }

    private void Update()
    {
        if (DisableDisablePanel) return;

        if(EventActionCreator.NowEventList.Count > 0 || MoveBtn.DisableMove || GimmickManager.I.StopGimmick)
        {
            //ForceDisable();
            Disable();
        }
        else
        {
            //ForceEnable();
            Enable();
        }
    }

    public void Enable()
    {
        if (!force)
        {
            image.raycastTarget = false;

        }
    }

    public void ForceDisable()
    {
        force = true;
        image.raycastTarget = true;
    }

    public void ForceEnable()
    {
        force = true;
        image.raycastTarget = false;
    }

    public void ExitForce()
    {
        force = false;
    }
}
