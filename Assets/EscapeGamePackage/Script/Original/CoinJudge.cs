using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinJudge : MonoBehaviour
{
    public GameObject MatchCoinOjisan;
    public GameObject NoCoinOjisan;

    public GameObject TomodachiImages;

    public GameObject MatchCoinTomodachi;
    public GameObject NoCoinTomodachi;

    public int TotalNum;
    public int TotalEnughNum;

    public int HowMatch;
    public int TotalHowMatch;

    void Update()
    {
        if (TomodachiImages.activeSelf)
        {
            TotalNum = 2;
        }
        else
        {
            TotalNum = 1;
        }
    }


    public void CalValues(int Yen)
    {
        if(TotalNum == 1)
        {
            if(Yen >= HowMatch)
            {
                TotalEnughNum = 1;
                MatchCoinOjisan.SetActive(true);
                NoCoinOjisan.SetActive(false);
            }
            else
            {
                TotalEnughNum = 0;
                MatchCoinOjisan.SetActive(false);
                NoCoinOjisan.SetActive(true);
            }
        }
        else//totalnumが２のとき
        {
            Debug.Log(TotalNum * HowMatch);

            if (Yen >= TotalNum * HowMatch) 
            {
                MatchCoinTomodachi.SetActive(true);
                NoCoinTomodachi.SetActive(false);

                MatchCoinOjisan.SetActive(true);
                NoCoinOjisan.SetActive(false);

                Debug.Log("ここはいった１");

                TotalEnughNum = 2;
            }
            else if(Yen >= HowMatch)
            {
                MatchCoinTomodachi.SetActive(false);
                NoCoinTomodachi.SetActive(true);

                MatchCoinOjisan.SetActive(true);
                NoCoinOjisan.SetActive(false);


                Debug.Log("ここはいった2");

                TotalEnughNum = 1;
            }
            else
            {
                MatchCoinTomodachi.SetActive(false);
                NoCoinTomodachi.SetActive(true);

                MatchCoinOjisan.SetActive(false);
                NoCoinOjisan.SetActive(true);

                TotalEnughNum = 0;

            }
        }

        var enughnum = TotalEnughNum;

        if(TotalEnughNum == 0)
        {
            enughnum = 1;
        }

        TotalHowMatch = enughnum * HowMatch;
    }

}
