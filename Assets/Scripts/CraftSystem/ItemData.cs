using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムのデータ
/// </summary>
[CreateAssetMenu(fileName = "Item Data")]
public class ItemData : ScriptableObject
{
    [SerializeField] List<ItemDataBase> _itemDataBase;
    public List<ItemDataBase> DataBase => _itemDataBase;
}

[System.Serializable]
public class ItemDataBase
{
    [SerializeField] string _name;
    [SerializeField] string _tooltip;
    [SerializeField] Sprite _icon;
    [SerializeField] ItemID _id;
    public string Name => _name;
    public string Tooltip => _tooltip;
    public Sprite Icon => _icon;
    public ItemID Id => _id;
}

/// <summary>アイテムID</summary>
public enum ItemID
{
    RedGem,
    RedGems,
    GemWaterMelon,
}