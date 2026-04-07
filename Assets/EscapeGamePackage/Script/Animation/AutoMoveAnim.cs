using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AutoMoveAnim : MonoBehaviour
{
    public Vector3 offset;
    public float duration;
    public bool NoLoop;

    // Start is called before the first frame update
    void Start()
    {
        
        var value = transform.localPosition + offset;

        transform.DOLocalMove(value, duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
}
