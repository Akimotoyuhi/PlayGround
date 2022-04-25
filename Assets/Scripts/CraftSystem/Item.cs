using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// �A�C�e��
/// </summary>
public class Item : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image _image;
    private string _name;
    private string _tooltip;
    private ItemID _id;
    private ItemState _state;
    private CraftSystem _craftSystem;
    /// <summary>�A�C�e���̏��</summary>
    public ItemState ItemState { get => _state; set => _state = value; }
    /// <summary>�A�C�e��ID</summary>
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
    /// �N���b�N���ꂽ��
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

    //�ȉ��C���^�[�t�F�[�X�̎���

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
/// �A�C�e���̏��
/// </summary>
public enum ItemState
{
    Display,//�A�C�e���ꗗ
    Selected,//�I���ς�
}
