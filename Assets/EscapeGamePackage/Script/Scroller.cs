using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Scroller : MonoBehaviour
{
    public RawImage rawImage;
    public float dx, dy;

    private void Update()
    {
        rawImage.uvRect = new Rect(
            rawImage.uvRect.position + new Vector2(dx,dy) * Time.deltaTime,
            rawImage.uvRect.size
            );
    }

}
