using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PopupBase : MonoBehaviour
{
    [SerializeField] RectTransform m_rect;
    [SerializeField] Image m_image;
    [SerializeField] Text m_text;
    protected Subject<Unit> m_changeIndex = new Subject<Unit>();
    public RectTransform RectTransform => m_rect;
    /// <summary>ヒエラルキーのインデックスを更新して欲しい時に通知される</summary>
    public System.IObservable<Unit> ChangeIndexSubject => m_changeIndex;

    public static PopupBase Init(PopupBase prefab, Color color, string text)
    {
        PopupBase ret = Instantiate(prefab);
        ret.Setup(color, text);
        return ret;
    }

    public void Setup(Color color, string text)
    {
        m_image.color = color;
        m_text.text = text;
    }
}
