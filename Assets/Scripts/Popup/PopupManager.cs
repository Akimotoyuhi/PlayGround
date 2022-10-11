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
    /// �|�b�v�A�b�v�̓��e�̐ݒ荀��
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
    /// Popup�̐���
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
                    //�N���b�N���ɂ��̃|�b�v�A�b�v���őO�ʂɎ����Ă���
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
/// �\��������R�}���h�̎��
/// </summary>
public enum PopupType
{
    Normal,
    CommandType,
}
