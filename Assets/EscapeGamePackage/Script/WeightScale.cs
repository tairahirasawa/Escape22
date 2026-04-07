using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WeightScale : MonoBehaviour
{
    public List<Weight> OnWeighList = new List<Weight>();
    public int weight;

    public TextMeshPro weightText;

    void Update()
    {
        weightText.text = weight.ToString() + "g";
        CalcurateWeight();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.GetComponent<Weight>())
        {
            OnWeighList.Add(col.GetComponent<Weight>());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(OnWeighList.Contains(other.GetComponent<Weight>()))
        {
            OnWeighList.Remove(other.GetComponent<Weight>());
        }
    }

    public int CalcurateWeight()
    {
        weight = 0;

        for (int i = 0; i < OnWeighList.Count; i++)
        {
            weight += OnWeighList[i].weight;
        }

        return weight;
    }


}
