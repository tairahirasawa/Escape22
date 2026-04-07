using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerClickHandler
{
    public string type;
    public Image itemImage;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (type == ItemWindow.I.ScrItemType && ItemWindow.I.IsPresent) return;

        if(ItemManager.I.currentItem == type)
        {
            ItemWindow.I.PresentItemWindow(type);
        }

        ItemManager.I.currentItem = type;
    }

}
