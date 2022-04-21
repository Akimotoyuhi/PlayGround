using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Craft Data")]
public class CraftData : ScriptableObject
{
    [SerializeField] List<CraftDataBase> _craftDataBases;
    public List<CraftDataBase> CraftDataBases => _craftDataBases;
}

[System.Serializable]
public class CraftDataBase
{
    [SerializeField] List<ItemID> _craftRecipe;
    [SerializeField] ItemID _craftAfterItem;
    public ItemID CraftAfterItem => _craftAfterItem;
}