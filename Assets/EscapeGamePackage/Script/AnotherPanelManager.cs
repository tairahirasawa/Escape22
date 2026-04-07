using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherPanelManager : SingletonMonoBehaviour<AnotherPanelManager>
{
    public List<GameObject> anotherPanels = new List<GameObject>();

    public bool isPresentAnotherPanel;

    private void Update()
    {
        isPresentAnotherPanel = IsPresentAnotherPanel();
    }

    public void DissMissAnotherPanel()
    {

    }

    public bool IsPresentAnotherPanel()
    {
        if(anotherPanels == null) return false;

        return anotherPanels.Count > 0;

    }

}
