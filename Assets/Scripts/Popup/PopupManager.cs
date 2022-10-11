using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PopupManager : MonoBehaviour
{
    [SerializeField] PopupBase m_popup;
    [SerializeField] Transform m_popupParent;
    [SerializeField] PopupType m_popupType;
    [SerializeField] List<PopupSetting> m_popupSettings;
    /// <summary>
    /// ポップアップの内容の設定項目
    /// </summary>
    [System.Serializable]
    public class PopupSetting
    {
        [SerializeField] string m_text;
        [SerializeField] Color m_color;
        [SerializeField] RectTransform m_transform;
        public string Text => m_text;
        public Color Color => m_color;
        public Vector2 AnchoredPosition => m_transform.anchoredPosition;
    }
    private List<PopupBase> m_popupObjects = new List<PopupBase>();


    private void Start()
    {
        CreatePopup();
    }

    /// <summary>
    /// Popupの生成
    /// </summary>
    private void CreatePopup()
    {
        m_popupSettings.ForEach(x =>
        {
            PopupBase popup = PopupBase.Init(m_popup, x.Color, x.Text);
            popup.RectTransform.anchoredPosition = x.AnchoredPosition;
            popup.transform.SetParent(m_popupParent, false);
            m_popupObjects.Add(popup);
            switch (m_popupType)
            {
                case PopupType.Normal:
                    //クリック時にそのポップアップを最前面に持ってくる
                    popup.ChangeIndexSubject
                    .Subscribe(x => popup.transform.SetAsLastSibling());
                    break;
                case PopupType.CommandType:
                    break;
                default:
                    break;
            }
        });
    }
}

/// <summary>
/// 表示させるコマンドの種類
/// </summary>
public enum PopupType
{
    Normal,
    CommandType,
}
