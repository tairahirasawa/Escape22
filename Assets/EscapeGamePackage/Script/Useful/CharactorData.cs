using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CharactorData")]
public class CharactorData:ScriptableObject
{
    public List<Charactor> Charactors;


    public Sprite GetCharactorSprite(CharactorType type)
    {
        for (int i = 0; i < Charactors.Count; i++)
        {
            if (Charactors[i].chractorType == type)
            {
                return Charactors[i].ImageSprite;
            }
        }

        return null;
    }

    public string GetCharactorTerm(CharactorType type)
    {
        for (int i = 0; i < Charactors.Count; i++)
        {
            if (Charactors[i].chractorType == type)
            {
                return Charactors[i].displayNameTerm;
            }
        }

        return null;
    }

}

[Serializable]
public class Charactor
{
    public CharactorType chractorType;
    public string displayNameTerm;
    public Sprite ImageSprite;
}

public enum CharactorType
{
    none, George_normal, Edward_normal, Edward_sunglass, CafeMaster, ChildDemon, ShopOwner, Detective, Client, Boy, Performer,
    Grandmother,Snowman,King,Troll,Wizard,DemonKing,StationStaff,Doctor,Police
}