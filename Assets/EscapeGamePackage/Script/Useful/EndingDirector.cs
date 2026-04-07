using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using I2.Loc;
using DG.Tweening;

public class EndingDirector : SingletonMonoBehaviour<EndingDirector>
{
    public GameObject EndButtons;
    public TextMeshProUGUI EscapeJudgeText;

    protected override void OnAwake()
    {
        base.OnAwake();

        EscapeJudgeText.gameObject.SetActive(false);
        EndButtons.SetActive(false);
    }



    public void EndingSuccess()
    {
        EscapeJudgeText.gameObject.GetComponent<Localize>().Term = "EscapeSuccess";
        EscapeJudgeText.gameObject.SetActive(true);
        EscapeJudgeText.transform.DOPunchScale(Vector3.one * 1.1f, 1f, 5, 1f);

        EndButtons.SetActive(true);

    }

    public void EndingFail()
    {
        EscapeJudgeText.gameObject.GetComponent<Localize>().Term = "EscapeFail";
        EscapeJudgeText.gameObject.SetActive(true);
        EscapeJudgeText.transform.DOPunchScale(Vector3.one * 1.1f, 1f, 5, 1f);

        EndButtons.SetActive(true);

    }
}
