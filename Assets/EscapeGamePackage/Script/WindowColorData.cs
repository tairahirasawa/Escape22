using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/WindowColorData")]
public class WindowColorData : ScriptableObject
{
    public Color dayWindowColor;
    public Color nightWindowColor;
    public Color dayDoorOpenColor;
    public Color nightDoorOpenColor;
}
