using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotateAnim : MonoBehaviour
{
    public Vector3 endValue;
    public float duration;
    public bool NoLoop;

    void Start()
    {
        transform.DORotate(endValue, duration, RotateMode.FastBeyond360).SetLoops(NoLoop ? 0 : -1, LoopType.Restart).SetRelative().SetEase(Ease.Linear);
    }
}
