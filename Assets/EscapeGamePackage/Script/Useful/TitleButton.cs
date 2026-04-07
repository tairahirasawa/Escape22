using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using I2.Loc;

public class TitleButton : MonoBehaviour
{
    private Button titleBtn;

    public bool Ad;

    private void Start()
    {
        titleBtn = GetComponent<Button>();
        titleBtn.onClick.AddListener(() => CautionScr.I.PresentCautionScrLocalize("GoTitleMessage", OnClickYesBtn));
    }

    private void OnClickYesBtn()
    {
        if(Ad && !DataPersistenceManager.I.gameData.blockAd)
        {
            StartCoroutine(GoTitle());
        }
        else
        {
            SceneManager.LoadSceneAsync("Title");
        }

    }

    IEnumerator GoTitle()
    {
        GoogleAdManager.I.googleAD_Inter.ShowAd();

        yield return new WaitUntil(()=>!GoogleAdManager.I.googleAD_Inter.IsPlayAd);

        GoogleAdManager.I.googleAD_Banar.DestroyAd();

        TitleDirector.IsLoadAd = true;

        SceneManager.LoadSceneAsync("Title");

    }


}
