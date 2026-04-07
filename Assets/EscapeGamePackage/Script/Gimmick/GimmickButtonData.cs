using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using System;

[CreateAssetMenu(menuName = "ScriptableObjects/GimmickButtonData")]
public class GimmickButtonData : ScriptableObject
{
    public SerializableDictionaryBase<GimickButtonType,hoge> SortButtonSprites;
}

[Serializable]
public class hoge
{
    public Sprite[] uraaa; 
}

[Serializable]
public class ButtonSprite
{
    public Sprite sprite;
    public Color color = Color.white;
}

public enum GimickButtonType
{
    none,textMesh,sprite
}