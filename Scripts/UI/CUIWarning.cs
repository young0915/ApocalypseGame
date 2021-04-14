using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

// 최대한 재활용할 수 있는 함수.
public class CUIWarning : CBaseUI
{
    [SerializeField] private TextMeshProUGUI Ins_txtWarningTitle;                                          // 경고 메시지 제목.
    [SerializeField] private TextMeshProUGUI Ins_txtWarningContent;                                     // 경고 메시지 문구 내용.
    [SerializeField] private List<Button> ins_listbtn;

    private EmWarningType _eWarningType;


    // UI 초기설정.
    public void Initialization(EmWarningType eType)
    {
        int nBtnNum = 0;
        string strTitle = string.Empty;
        string strContent = string.Empty;
        _eWarningType = eType;
        switch (_eWarningType)
        {
            case EmWarningType.NicKaNemeError:

                strTitle = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 3);
                strContent = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 4);

                break;
            case EmWarningType.SceneChange:
                nBtnNum = 1;

                strTitle = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 10); 
                strContent = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 11); 

                break;

            case EmWarningType.LackMoney:

                strTitle = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 3);
                strContent = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 14);

                break;

            case EmWarningType.Inventory:

                strTitle = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 3);
                strContent = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 15);

                break;

            case EmWarningType.PurchaseComplete :
                strTitle = string.Empty;
                strContent = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 21);

                break;

            case EmWarningType.ApplicationQuit:
                nBtnNum = 1;
                strTitle = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 23);
                strTitle = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 24);


                break;
            case EmWarningType.LackMaterial:

                strTitle = string.Empty;
                strContent = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 36);

                break;

        }
        if(nBtnNum ==0)
        {
            ins_listbtn[nBtnNum].onClick.AddListener(() => OnClickConfirm());
        }
        else
        {
            if(_eWarningType == EmWarningType.SceneChange)
            {
                ins_listbtn[0].onClick.AddListener(() => OnClickConfirm(EmWarningType.SceneChange));
                ins_listbtn[nBtnNum].gameObject.SetActive(true);
            }
            else
            {
                ins_listbtn[0].onClick.AddListener(() => OnClickConfirm(EmWarningType.ApplicationQuit));
                ins_listbtn[nBtnNum].gameObject.SetActive(true);
            }
        }

        SetTextInfo(strTitle, strContent);

    }

    // 확인 버튼.
    public void OnClickConfirm(EmWarningType eWarningType = EmWarningType.NicKaNemeError)
    {
        _eWarningType = eWarningType;
        switch (_eWarningType)
        {
            case EmWarningType.NicKaNemeError:
            case EmWarningType.Inventory:
            case EmWarningType.LackMoney:
            case EmWarningType.LackMaterial:
            case EmWarningType.PurchaseComplete:
                break;
            case EmWarningType.SceneChange:
                StartCoroutine(CUIManager.Inst.CorITweenFade());
                CSceneManager.Inst.OnSceneMovement(CDataManager.m_strCityScene);
                break;

            case EmWarningType.ApplicationQuit:
                Application.Quit();

                break;
        }
        Close(false);
    }


    // UI 텍스트 정보.
    public void SetTextInfo(string strUITitle, string strUIContent)
    {
        Ins_txtWarningTitle.text = strUITitle.ToString();
        Ins_txtWarningContent.text = strUIContent.ToString();
    }

    public override void Open(Action<EmClickState> callBack)
    {
        base.Open(callBack);
    }

    public override void Close(bool bDestroy = true)
    {
        base.Close(bDestroy);
    }
}
