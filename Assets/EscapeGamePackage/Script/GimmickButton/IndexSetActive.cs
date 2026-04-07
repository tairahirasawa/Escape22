using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class IndexSetActive : MonoBehaviour
{
    public int index;
    public int defaultIndex;
    public List<GameObject> SortObjects;

    void Awake()
    {
        index = defaultIndex;
    }

    private void Update()
    {
        for (int i = 0; i < SortObjects.Count; i++)
        {
            SortObjects[i].SetActive(i == index);
        }
    }

}
