using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �N���t�g���V�s�̃f�[�^<br/>
/// �N���t�g�̐����ۂ��s��
/// </summary>
[CreateAssetMenu(fileName = "Craft Data")]
public class CraftData : ScriptableObject
{
    [SerializeField] List<CraftDataBase> _craftDataBases;
    /// <summary>�N���t�g����</summary>
    /// <param name="items">�I���ς݃A�C�e��</param>
    /// <returns>�N���t�g��̃A�C�e��ID</returns>
    public ItemID? Craft(List<ItemID> items)
    {
        //�N���t�g�����̕]��
        //�����Ȃ�N���t�g��̃A�C�e��ID��Ԃ��A��v���郌�V�s���������null���ς���
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
    /// ���V�s�̕]��
    /// </summary>
    /// <param name="items">�I���ς݃A�C�e��</param>
    /// <returns>������</returns>
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