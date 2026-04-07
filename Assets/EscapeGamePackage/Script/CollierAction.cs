using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ColliderAction : MonoBehaviour
{
    public EventActionCreator OnEnterEventAction;
    public EventActionCreator OnExitEventAction;

    public string objectName;

    private async Task OnTriggerEnter2D(Collider2D col)
    {
        if(objectName != null && objectName != "")
        {
            if(col.gameObject.name != objectName)
            {
                return;
            }
        }

        if (OnEnterEventAction == null) return;
        await OnEnterEventAction.EventAction();
    }

    async Task OnTriggerExit2D(Collider2D other)
    {
        if(objectName != null && objectName != "")
        {
            if(other.gameObject.name != objectName)
            {
                return;
            }
        }

         if (OnExitEventAction == null) return;
        await OnExitEventAction.EventAction();
    }
}
