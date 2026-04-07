using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LinkButton : MonoBehaviour
{
    public string link;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(onClick);
    }

    public void onClick()
    {
        Application.OpenURL(link);//""の中には開きたいWebページのURLを入力します
    }
}
