using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �S�̂��Ǘ�����N���X
/// </summary>
public class CraftSystem : MonoBehaviour
{
    [SerializeField] ItemData _itemData;
    [SerializeField] CraftData _craftData;
    [SerializeField] Item _itemPrefab;
    /// <summary>�A�C�e���ꗗ��\������ꏊ</summary>
    [SerializeField] Transform _itemDisplayParent;
    /// <summary>�N���t�g���镨(�I���ς݃A�C�e��)��\������ꏊ</summary>
    [SerializeField] Transform _itemCraftParent;
    /// <summary>�ŏ��Ɏ����Ă�A�C�e��</summary>
    [SerializeField] List<HaveItem> _startHaveItems;
    /// <summary>�J�[�\�������킹���A�C�e���̖��O��\��������</summary>
    [SerializeField] Text _nameText;
    /// <summary>�J�[�\�������킹���A�C�e���̐�������\��������</summary>
    [SerializeField] Text _tooltipText;
    //private List<Item> _items = new List<Item>();
    /// <summary>�I�𒆂̃A�C�e��</summary>
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
        //�����Ă�A�C�e������ׂ�
        foreach (var item in _startHaveItems)
        {
            Item i = Instantiate(_itemPrefab);
            i.Setup(_itemData.DataBase[(int)item.ItemID], this);
            i.ItemState = ItemState.Display;
            i.transform.SetParent(_itemDisplayParent);
        }
        SetText("", ""); //�e�L�X�g������
    }

    /// <summary>
    /// �A�C�e���ɃJ�[�\�������킹�����ɏo���A�C�e���ڍ׃e�L�X�g�̍X�V
    /// </summary>
    /// <param name="name"></param>
    /// <param name="tooltip"></param>
    public void SetText(string name, string tooltip)
    {
        _nameText.text = name;
        _tooltipText.text = tooltip;
    }

    /// <summary>
    /// �N���t�g�{�^���������ꂽ���̏���<br/>
    /// unity�̃{�^������Ă΂�鎖��z�肵�Ă���
    /// </summary>
    public void CraftButton()
    {
        ItemID? item = _craftData.Craft(_selectedItems);
        if (item == null)
        {
            Debug.Log("�N���t�g�o���Ȃ�");
            return;
        }
        Item i = Instantiate(_itemPrefab);
        i.Setup(_itemData.DataBase[(int)item], this);
        i.ItemState = ItemState.Display;
        i.transform.SetParent(_itemDisplayParent);
    }

    /// <summary>
    /// �I���ς݃A�C�e�����X�g�̍X�V
    /// </summary>
    /// <param name="isAdd">�ǉ����ǂ���</param>
    /// <param name="item">�I�����ꂽ�A�C�e��</param>
    public void CraftList(bool isAdd, Item item)
    {
        if (isAdd)
        {
            if (_selectedItems.Count < 5)//�I�𐧌�
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
    /// �N���t�g��ʂ̍X�V<br/>
    /// �I���ς݃A�C�e������ׂ�
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
