using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CUIDialogue : CBaseUI
{
    [SerializeField] private GameObject ins_objDialog =null;
    [SerializeField] private GameObject ins_BtnGroup = null;
    [SerializeField] private TextMeshProUGUI ins_txtNpcNameTag;
    [SerializeField] private TextMeshProUGUI ins_txtNpcDescript;
    [SerializeField] private List<Button> ins_btnList;                      // Ins_btnNpcYes or Ins_btnNextDialog.
    [SerializeField] private List<Text> ins_txtSelectDialog;
    private int _nDialog = 0;
    public int m_nDialog { get { return _nDialog; } }

    private bool _bIsClick = false;
    private int _nBackDialog = 0;
    private string _strName = string.Empty;
    private string _strContent = string.Empty;

    public void SetNpcDialog(string strName, string strDescript)
    {
        ins_txtNpcNameTag.text = strName;
        ins_txtNpcDescript.text = strDescript;
    }

    private void SetBtnSelectDialog(string strSelectTop, string strSelectBottom)
    {
        ins_txtSelectDialog[0].text = strSelectTop.ToString();
        ins_txtSelectDialog[1].text = strSelectBottom.ToString();
    }

    public void OnClickSelect(int num)
    {
        ins_objDialog.SetActive(true);
        ins_BtnGroup.SetActive(false);
        _strName = CDialogManager.Inst.GetDialog(_nDialog+num).m_strNpcName.ToString();
        _strContent =  CDialogManager.Inst.GetDialog( _nDialog + num).m_strContent.ToString();
        SetNpcDialog(_strName, _strContent);
        ins_btnList[1].gameObject.SetActive(false);

        if (_strName == "유진")
        {
            CUIManager.Inst.m_cUIQuest.OnAddQuest(2);
        }
        if(_strName =="하디")
        {
            // 좀비, 톡 활성화.
            CUIManager.Inst.m_cUIPhone.m_objTalkAndGuide.SetActive(true);
            CUIManager.Inst.m_cUIQuest.OnRemoveQuest(2);
        }
    }


    public void OnClickYesDialog()
    {
        if (_bIsClick) return;

        StartCoroutine(CUIManager.Inst.CorStore());
        Close(false);

        _bIsClick = false;
    }

    public void OnClickNextDialog()
    {
        if (_bIsClick) return;

        _nDialog++;

     
        if (CDialogManager.Inst.GetDialog(_nDialog).m_eDialogType == EmDialogType.Button)
        {
            ins_objDialog.SetActive(false);
            ins_BtnGroup.SetActive(true);
            string strTop = CDialogManager.Inst.GetDialog(_nDialog).m_strContent.ToString();
            string strBottom = CDialogManager.Inst.GetDialog(_nDialog + 1).m_strContent.ToString();

            SetBtnSelectDialog(strTop, strBottom);
        }
       
        _strName = CDialogManager.Inst.GetDialog(_nDialog).m_strNpcName.ToString();
        _strContent = CDialogManager.Inst.GetDialog(_nDialog).m_strContent.ToString();

        SetNpcDialog(_strName, _strContent);

   
        _bIsClick = false;
    }


    // 버튼 List 비활성화 여부.
    public void SetBtnActive(int nBtnNum, bool bIsActive = true)
    {
        ins_btnList[nBtnNum].gameObject.SetActive(bIsActive);
    }


    public void GetNumDialog(int nFront, int nBack)
    {
        _nDialog = nFront;
        _nBackDialog = nBack;
    }

    public override void Open(Action<EmClickState> callBack)
    {
        base.Open(callBack);
    }

    public override void Close(bool bDestroy = true)
    {
        Destroy(this.gameObject);
        CCameraManager.Inst.SetCameraView(EmCameraType.Bunker);
    }

}
