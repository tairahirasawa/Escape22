using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class objectChangeButton : MonoBehaviour,IPointerClickHandler
{
    public AudioDirector.SeType seType;
    public string[] needItemTypes;

    public int index;
    public List<GameObject> SortObjects;

    private void Update()
    {
        for (int i = 0; i < SortObjects.Count; i++)
        {
            SortObjects[i].SetActive(i == index);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (needItemTypes.Length != 0)
        {
            if(needItemTypes.Contains(ItemManager.I.currentItem) == false) return;
        }

        AudioDirector.I.PlaySE(seType);
        index++;

        if (index >= SortObjects.Count)
        {
            index = 0;
        }
    }
}
