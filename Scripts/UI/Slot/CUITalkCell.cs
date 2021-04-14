using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class CUITalkCell : MonoBehaviour
{

    // 상대방.
    [SerializeField] private Image ins_ImgProfile;
    [SerializeField] private Image ins_ImgContent;
    [SerializeField] private Text ins_txtName;
    [SerializeField] private Text ins_txtContent;

    [Space]
    [SerializeField] private Text ins_txtContentMe;
    [SerializeField] private Image ins_ImgMeContent;
    [Space]

    [SerializeField] private Image ins_ImgInvitation;
    [SerializeField] private Text ins_txtInvitation;

    private int _nId = 0;
    private int _nProfileNum = 0;
    private string _strName = string.Empty;
    private string _strContent = string.Empty;

    private EmDialogType _eDialogType;
    private bool _bIsClick = false;
    private bool _bIsMeEnabled = false;

    private const string _strProfilePath = "Texture/UI/UI/";

    public void SetData(CTalkModel cModel)
    {
        this._eDialogType = cModel.m_eDialogType;
        this._nId = cModel.m_nId;
        this._strName = cModel.m_strName;
        this._strContent = cModel.m_strContent;

        this._strName = string.Empty;
        this._strContent = string.Empty;

        ins_ImgProfile.gameObject.SetActive(false);
        ins_ImgInvitation.gameObject.SetActive(false);
        ins_ImgContent.gameObject.SetActive(false);
        ins_ImgMeContent.gameObject.SetActive(false);
        ins_txtInvitation.gameObject.SetActive(false);
        ins_txtContentMe.gameObject.SetActive(false);
        ins_txtName.gameObject.SetActive(false);
        ins_txtContent.gameObject.SetActive(false);


        // 이미지 비활성화 또는 활성화 불러오는 여부.
        switch (_eDialogType)
        {
            case EmDialogType.Me:
                ins_ImgMeContent.gameObject.SetActive(true);
                ins_txtContentMe.gameObject.SetActive(true);

                break;

            case EmDialogType.Hadi:
            case EmDialogType.UJin:
                ins_ImgProfile.gameObject.SetActive(true);
                ins_ImgContent.gameObject.SetActive(true);
                ins_txtName.gameObject.SetActive(true);
                ins_txtContent.gameObject.SetActive(true);

                if (_eDialogType == EmDialogType.Hadi)
                {
                    _nProfileNum = 5;
                }
                else
                {
                    _nProfileNum = 6;
                }
                ins_ImgProfile.sprite = CResourceLoader.Load<Sprite>(_strProfilePath + _nProfileNum);

                break;

            case EmDialogType.Invitation:
                ins_ImgInvitation.gameObject.SetActive(true);
                ins_txtInvitation.gameObject.SetActive(true);
                break;

        }

        string strNickName = CUIManager.Inst.m_cUIPhone.m_cUIInventory.m_cPlayerInfo.m_strName.ToString();

        _strName = cModel.m_strName.ToString();
        _strContent = string.Format(cModel.m_strContent.ToString(), strNickName); 

        ins_txtName.text = _strName;
        ins_txtContent.text = _strContent;
        ins_txtContentMe.text = _strContent;
    }


}
