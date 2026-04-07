using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FindItemCounterUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI counterText;
    public FindItemTypeSaveData findItemTypeSaveData;

    public void UpdateCount()
    {
        int totalcount = 0;

        for (int i = 0; i < findItemTypeSaveData.findItems.Count; i++)
        {
            if(findItemTypeSaveData.findItems[i].IsCollected)
            {
                totalcount ++;
            }
        }

        counterText. text = string.Format("{0}/{1}", totalcount,findItemTypeSaveData.findItems.Count);
    }

}
