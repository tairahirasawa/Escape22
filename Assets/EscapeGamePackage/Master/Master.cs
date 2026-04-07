using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Master")]
public class Master : ScriptableObject
{
    public string version;
    public GameMode gameMode;
    public PlatForm platForm;
    public GoogleAdSettings googleAdSettings;

    public enum GameMode
    {
        Prodact, Develop
    }

    public enum PlatForm
    {
        SmartPhone,WebGL
    }

}
