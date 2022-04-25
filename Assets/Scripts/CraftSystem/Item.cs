using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// アイテム
/// </summary>
public class Item : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image _image;
    private string _name;
    private string _tooltip;
    private ItemID _id;
    private ItemState _state;
    private CraftSystem _craftSystem;
    /// <summary>アイテムの状態</summary>
    public ItemState ItemState { get => _state; set => _state = value; }
    /// <summary>アイテムID</summary>
    public ItemID ItemID => _id;

    public void Setup(ItemDataBase database, CraftSystem craftSystem)
    {
        _image.sprite = database.Icon;
        _name = database.Name;
        _tooltip = database.Tooltip;
        _id = database.Id;
        _craftSystem = craftSystem;
    }

    /// <summary>
    /// クリックされた時
    /// </summary>
    public void OnClick()
    {
        switch (_state)
        {
            case ItemState.Display:
                _craftSystem.CraftList(true, this);
                break;
            case ItemState.Selected:
                _craftSystem.CraftList(false, this);
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

    //以下インターフェースの実装

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnClick();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _craftSystem.SetText(_name, _tooltip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _craftSystem.SetText("", "");
    }
}

/// <summary>
/// アイテムの状態
/// </summary>
public enum ItemState
{
    Display,//アイテム一覧
    Selected,//選択済み
}
