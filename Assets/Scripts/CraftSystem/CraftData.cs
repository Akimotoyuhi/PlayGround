using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クラフトレシピのデータ<br/>
/// クラフトの成功可否を行う
/// </summary>
[CreateAssetMenu(fileName = "Craft Data")]
public class CraftData : ScriptableObject
{
    [SerializeField] List<CraftDataBase> _craftDataBases;
    /// <summary>クラフトする</summary>
    /// <param name="items">選択済みアイテム</param>
    /// <returns>クラフト後のアイテムID</returns>
    public ItemID? Craft(List<ItemID> items)
    {
        //クラフト条件の評価
        //成功ならクラフト後のアイテムIDを返し、一致するレシピが無ければnullが変える
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
    /// <summary>
    /// レシピの評価
    /// </summary>
    /// <param name="items">選択済みアイテム</param>
    /// <returns>成功可否</returns>
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