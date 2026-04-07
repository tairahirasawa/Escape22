using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

public class CollectionItemImageSwitcher : SingletonMonoBehaviour<CollectionItemImageSwitcher>
{
    public SerializableDictionaryBase<StageContext.StageType, GameObject> stages;

    
    public void SetCollectionItemImage(StageContext.StageType stageType)
    {
        foreach (var stage in stages)
        {
            stage.Value.SetActive(false);
        }

        stages[stageType].SetActive(true);
    }
}
