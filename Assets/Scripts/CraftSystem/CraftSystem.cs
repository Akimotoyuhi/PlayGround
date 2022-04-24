using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftSystem : MonoBehaviour
{
    [SerializeField] ItemData _itemData;
    [SerializeField] CraftData _craftData;
    [SerializeField] Item _itemPrefab;
    [SerializeField] Transform _itemDisplayParent;
    [SerializeField] Transform _itemCraftParent;
    [SerializeField] List<HaveItem> _startHaveItems;
    [SerializeField] Text _nameText;
    [SerializeField] Text _tooltipText;
    private List<Item> _items = new List<Item>();
    private List<ItemID> _selectedItems = new List<ItemID>();
    [System.Serializable]
    public class HaveItem
    {
        [SerializeField] ItemID _id;
        [SerializeField] int _num;
        public ItemID ItemID => _id;
        public int Num => _num;
    }

    private void Start()
    {
        foreach (var item in _startHaveItems)
        {
            Item i = Instantiate(_itemPrefab);
            i.Setup(_itemData.DataBase[(int)item.ItemID], this);
            i.ItemState = ItemState.Display;
            i.transform.SetParent(_itemDisplayParent);
        }
        SetText("", "");
    }

    public void SetText(string name, string tooltip)
    {
        _nameText.text = name;
        _tooltipText.text = tooltip;
    }

    public void CraftButton()
    {
        ItemID? item = _craftData.Craft(_selectedItems);
        if (item == null)
        {
            Debug.Log("クラフト出来ない");
            return;
        }
        Item i = Instantiate(_itemPrefab);
        i.Setup(_itemData.DataBase[(int)item], this);
        i.ItemState = ItemState.Display;
        i.transform.SetParent(_itemDisplayParent);
    }

    public void CraftList(bool isAdd, Item item)
    {
        if (isAdd)
        {
            if (_selectedItems.Count < 5)//選択制限
            {
                Item i = Instantiate(_itemPrefab);
                i.Setup(_itemData.DataBase[(int)item.ItemID], this);
                i.ItemState = ItemState.Selected;
                i.transform.SetParent(_itemCraftParent);
                _selectedItems.Add(i.ItemID);
            }
        }
        else
        {
            for (int i = 0; i < _selectedItems.Count; i++)
            {
                if (_selectedItems[i] == item.ItemID)
                    _selectedItems.RemoveAt(i);
            }
        }
        CraftPanelUpdate();
    }

    private void CraftPanelUpdate()
    {
        for (int i = 0; i < _itemCraftParent.childCount; i++)
        {
            Destroy(_itemCraftParent.GetChild(i).gameObject);
        }
        foreach (var item in _selectedItems)
        {
            Item i = Instantiate(_itemPrefab);
            i.Setup(_itemData.DataBase[(int)item], this);
            i.ItemState = ItemState.Selected;
            i.transform.SetParent(_itemCraftParent);
        }
    }
}
