using TMPro;
using System;
using UnityEngine;
using System.Collections;

public class CUINickNameInputField : CBaseUI
{
    [SerializeField] private CSOPlayerInfo ins_cSOPlayerInfo;
    [HideInInspector] public CSOPlayerInfo m_cSOPlayerInfo { get { return ins_cSOPlayerInfo; } }
    [SerializeField] private TextMeshProUGUI ins_txtTitle;
    [SerializeField] private TextMeshProUGUI ins_txtContent;
    [SerializeField] private TextMeshProUGUI ins_txtPlaceholder;
    [SerializeField] private TextMeshProUGUI ins_txtText;


    private readonly int _nMinNickNameText = 2;
    private readonly int _nMaxNickNameText = 8;

    // UI 초기설정.
    public void Initialization()
    {
        SetTextInfo();
    }


    // UI 텍스트 정보.
    private void SetTextInfo()
    {
        ins_txtTitle.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 1);
        ins_txtPlaceholder.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 2);
        ins_txtContent.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 2);
    }

    public void OnClickComplete()
    {
        if(ins_txtText.text.Length <= _nMinNickNameText || ins_txtText.text.Length >= _nMaxNickNameText)
        {
            
            StartCoroutine(CUIManager.Inst.CorWarning());
        }
        else
        {
            CDebugLog.Log("young :  Complete", CDebugLog.ErrorID.Log);
            ins_cSOPlayerInfo.m_strName = ins_txtText.text.ToString();
            StartCoroutine(CUIManager.Inst.CorCreateCharacter());
            Close(false);
           
        }
    }

    

    public override void Open(Action<EmClickState> callBack)
    {
        base.Open(callBack);
    }

    public override void Close(bool bDestroy = true)
    {
        Destroy(gameObject);
    }
}
