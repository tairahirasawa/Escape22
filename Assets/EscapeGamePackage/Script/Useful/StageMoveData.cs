using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

[CreateAssetMenu(menuName = "ScriptableObjects/StageMoveData")]
public class StageMoveData : ScriptableObject
{
    public SerializableDictionaryBase<StageContext.StageType, string> MoveScenes;
}
