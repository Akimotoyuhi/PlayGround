using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 全体を管理するクラス
/// </summary>
public class CraftSystem : MonoBehaviour
{
    [SerializeField] ItemData _itemData;
    [SerializeField] CraftData _craftData;
    [SerializeField] Item _itemPrefab;
    /// <summary>アイテム一覧を表示する場所</summary>
    [SerializeField] Transform _itemDisplayParent;
    /// <summary>クラフトする物(選択済みアイテム)を表示する場所</summary>
    [SerializeField] Transform _itemCraftParent;
    /// <summary>最初に持ってるアイテム</summary>
    [SerializeField] List<HaveItem> _startHaveItems;
    /// <summary>カーソルを合わせたアイテムの名前を表示するやつ</summary>
    [SerializeField] Text _nameText;
    /// <summary>カーソルを合わせたアイテムの説明文を表示するやつ</summary>
    [SerializeField] Text _tooltipText;
    //private List<Item> _items = new List<Item>();
    /// <summary>選択中のアイテム</summary>
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
        //持ってるアイテムを並べる
        foreach (var item in _startHaveItems)
        {
            Item i = Instantiate(_itemPrefab);
            i.Setup(_itemData.DataBase[(int)item.ItemID], this);
            i.ItemState = ItemState.Display;
            i.transform.SetParent(_itemDisplayParent);
        }
        SetText("", ""); //テキスト初期化
    }

    /// <summary>
    /// アイテムにカーソルを合わせた時に出すアイテム詳細テキストの更新
    /// </summary>
    /// <param name="name"></param>
    /// <param name="tooltip"></param>
    public void SetText(string name, string tooltip)
    {
        _nameText.text = name;
        _tooltipText.text = tooltip;
    }

    /// <summary>
    /// クラフトボタンが押された時の処理<br/>
    /// unityのボタンから呼ばれる事を想定している
    /// </summary>
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

    /// <summary>
    /// 選択済みアイテムリストの更新
    /// </summary>
    /// <param name="isAdd">追加かどうか</param>
    /// <param name="item">選択されたアイテム</param>
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

    /// <summary>
    /// クラフト画面の更新<br/>
    /// 選択済みアイテムを並べる
    /// </summary>
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
