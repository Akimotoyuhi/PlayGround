using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;

/// <summary>
/// �N���b�N���ꂽ���̂��őO�ʂɗ���^�C�v�̃|�b�v�A�b�v
/// </summary>
public class MultiPopup : PopupBase, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        m_changeIndex.OnNext(Unit.Default);
    }
}
