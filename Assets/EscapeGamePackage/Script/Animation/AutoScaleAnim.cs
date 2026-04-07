using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AutoScaleAnim : MonoBehaviour
{
    public Vector3 endScale;
    public float duration;
    public bool NoLoop;

    // Start is called before the first frame update
    void Start()
    {
        
        if(!NoLoop)
        {
            transform.DOScale(endScale, duration).SetLoops(-1, LoopType.Yoyo);

        }
        else
        {
            transform.DOScale(endScale, duration).SetLoops(1, LoopType.Yoyo);
        }
    }

    private void OnEnable()
    {
        //transform.DOScale(endScale, duration).SetLoops(-1, LoopType.Yoyo);
    }
}
