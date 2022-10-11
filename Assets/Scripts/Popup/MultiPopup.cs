using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;

/// <summary>
/// クリックされたものが最前面に来るタイプのポップアップ
/// </summary>
public class MultiPopup : PopupBase, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        m_changeIndex.OnNext(Unit.Default);
    }
}
