using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/FindItemDatas")]
public class FindItemDatas : ScriptableObject
{
    public string FindItemKeysName;
    public List<FindItemKeylist> findItemKeylist;
}

[Serializable]
public class FindItemKeylist
{
    public string findItemType;
    public Sprite findItemSprite;
    public List<string> findItemKeylist;
}
