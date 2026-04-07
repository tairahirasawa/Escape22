using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSwitch : MonoBehaviour
{
    public GameObject ending01;
    public GameObject ending02;
    public GameObject ending03;

    private void Update()
    {
        if (ending01 != null) ending01.SetActive(ItemManager.I.currentCollectionItemCount < 5);
        if (ending02 != null) ending02.SetActive(ItemManager.I.currentCollectionItemCount >= 5 && ItemManager.I.currentCollectionItemCount < 10);
        if (ending03 != null) ending03.SetActive(ItemManager.I.currentCollectionItemCount >= 10);
    }


}
