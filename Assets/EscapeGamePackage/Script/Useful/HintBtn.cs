using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintBtn : MonoBehaviour
{
    public int hintBtnNum;

    public GameObject HintMovieImage;
    public GameObject HintNoMovieImage;

    private void Update()
    {
        if (!ProgressDirector.I) return;
        
        if (!ProgressDirector.I.ShowedHintAnswer(hintBtnNum) && !DataPersistenceManager.I.gameData.blockAd)
        {
            HintMovieImage.SetActive(true);
            HintNoMovieImage.SetActive(false);
        }
        else
        {
            HintMovieImage.SetActive(false);
            HintNoMovieImage.SetActive(true);
        }
        
    }
}
