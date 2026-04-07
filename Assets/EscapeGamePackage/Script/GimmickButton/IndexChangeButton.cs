using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class IndexChangeButton : MonoBehaviour,IPointerClickHandler
{
    public int amount;
    public AudioDirector.SeType seType;    
    public IndexSetActive indexSetActive;


    public void OnPointerClick(PointerEventData eventData)
    {
        AudioDirector.I.PlaySE(seType);
        indexSetActive.index += amount;

        if(indexSetActive.index <= 0)
        {
            indexSetActive.index = 0;
        }

         if(indexSetActive.SortObjects.Count-1 < indexSetActive.index)
        {
            indexSetActive.index = indexSetActive.SortObjects.Count-1;
        }
    }
}
