using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromothinMovieMaker : MonoBehaviour
{
    public bool move;

    [Header("+-200が丁度いいかも")]
    public Vector3 MoveValue;


    private void Update()
    {
        if (!move) return;
        transform.position += MoveValue * Time.deltaTime;
    }

}
