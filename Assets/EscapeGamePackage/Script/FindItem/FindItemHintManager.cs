using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FindItemHintManager : SingletonMonoBehaviour<FindItemHintManager>
{
    public List<FindItemObject> findItemObjects;
    public GameObject hintCircle;
    public Button hintButton;
    public GameObject hintDescription;

    private GameObject targetObject;

    protected override void OnAwake()
    {
        hintButton.onClick.AddListener(OnClickHintButton);

        hintCircle.SetActive(false);
        hintDescription.SetActive(false);

        findItemObjects = FindObjectsByType<FindItemObject>(FindObjectsInactive.Include,FindObjectsSortMode.None).ToList();
    }

    public void OnClickHintButton()
    {
        GoogleAdManager.I.googleAD_RewardSubEvent.ShowRewardedAd(()=>presentHint());
    }

    void Update()
    {
        hintButton.gameObject.SetActive(!hintCircle.activeSelf && !FindItemManager.I.savedata.IsAllCollect);

        if(targetObject != null)
        {
            hintCircle.transform.position = targetObject.transform.position;
        }
    }

    public void presentHint()
    {
        var key = FindItemManager.I.GetNoCollectFindItemKey();

        for (int i = 0; i < findItemObjects.Count; i++)
        {
            if(findItemObjects[i].findItemObjectKey == key)
            {
                targetObject = findItemObjects[i].gameObject;
            }
        }

        hintCircle.SetActive(true);
        hintDescription.SetActive(true);
        
    }

    public void hideHint()
    {
        hintCircle.SetActive(false);
        hintDescription.SetActive(false);
    }

}
