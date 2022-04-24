using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Craft Data")]
public class CraftData : ScriptableObject
{
    [SerializeField] List<CraftDataBase> _craftDataBases;
    public ItemID? Craft(List<ItemID> items)
    {
        for (int i = 0; i < _craftDataBases.Count; i++)
        {
            if (_craftDataBases[i].Evaluation(items))
            {
                return _craftDataBases[i].CraftAfterItem;
            }
        }
        return null;
    }
}

[System.Serializable]
public class CraftDataBase
{
    [SerializeField] List<ItemID> _craftRecipe;
    [SerializeField] ItemID _craftAfterItem;
    public ItemID CraftAfterItem => _craftAfterItem;
    public bool Evaluation(List<ItemID> items)
    {
        if (_craftRecipe.Count == items.Count)
        {
            for (int i = 0; i < _craftRecipe.Count; i++)
            {
                if (_craftRecipe[i] != items[i])
                    return false;
            }
            return true;
        }
        return false;
    }
}