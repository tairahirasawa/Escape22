using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class AutoFadeAnim : MonoBehaviour
{
    CanvasGroup canvas;

    public float endvalue;
    public float duration;

    public bool NoLoop;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        canvas.DOFade(endvalue, duration).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
