using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CUIPhone : CBaseUI
{
    [SerializeField] private Image ins_ImgCUIPhone;

    [SerializeField] private CUIInventory ins_cUIInventory = null;
    [HideInInspector] public CUIInventory m_cUIInventory { get { return ins_cUIInventory; } }

    [SerializeField] private CUIZombieScrollView ins_cUIZombieGuide;
    [SerializeField] private CUITalk ins_cUITalk;
    [SerializeField] private GameObject ins_objBgPhone;
    [SerializeField] private GameObject ins_objTalkAndGuide;
    public GameObject m_objTalkAndGuide { get { return ins_objTalkAndGuide; } }

    [SerializeField] private TextMeshProUGUI ins_txtYouHaveMoney;                                   // 현재가지고 있는 돈이라는 문장.
    [SerializeField] private TextMeshProUGUI ins_txtMoney;

    private int _nMoney = 0;

    private EmPhoneState _ePhoneState;
    private bool _bClickLock = false;

    public void Initialization()
    {
        SetTextInfo();
    }

    public void SetTextInfo()
    {
        // 소지하고 있는 돈.
        ins_txtYouHaveMoney.text = string.Format(CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 29),
            ins_cUIInventory.m_cPlayerInfo.m_strName);

        _nMoney = ins_cUIInventory.m_cPlayerInfo.m_nMoney;

        ins_txtMoney.text = _nMoney.ToString() + CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 30);
    }

 
    public void OnClickPhoneList(int nIdx)
    {
        if (_bClickLock) return;

        EmPhoneState ePhoneState = (EmPhoneState)nIdx;

        if (_ePhoneState == ePhoneState)
            return;

        SetPhoneState(ePhoneState);
    }

    private void SetPhoneState(EmPhoneState ePhoneState)
    {
        ins_cUIInventory.gameObject.SetActive(false);
        ins_cUIInventory.m_btnDeletItem.gameObject.SetActive(false);
        ins_cUIZombieGuide.gameObject.SetActive(false);
        ins_cUITalk.gameObject.SetActive(false);
        _ePhoneState = ePhoneState;

        ins_ImgCUIPhone.gameObject.SetActive(true);

        switch (_ePhoneState)
        {
            case EmPhoneState.Inventory:
                ins_cUIInventory.gameObject.SetActive(true);
                ins_cUIInventory.m_btnDeletItem.gameObject.SetActive(true);
                StartCoroutine(ins_cUIInventory.CorMakeInvenSlot());
                ins_objBgPhone.gameObject.SetActive(false);
                break;

            case EmPhoneState.ZombieGuide:
                ins_cUIZombieGuide.gameObject.SetActive(true);
                ins_objBgPhone.gameObject.SetActive(false);

                break;

            case EmPhoneState.Talk:
                ins_cUITalk.gameObject.SetActive(true);
                ins_objBgPhone.gameObject.SetActive(false);

                break;

            case EmPhoneState.None:
                ins_cUIInventory.gameObject.SetActive(false);
                ins_cUIZombieGuide.gameObject.SetActive(false);
                ins_cUITalk.gameObject.SetActive(false);
                ins_objBgPhone.gameObject.SetActive(true);
                break;
        }
    }


    // 뒤로가기 버튼.
    public void OnClickBackButton()
    {
        if (_bClickLock) return;
        _bClickLock = true;

        SetPhoneState(EmPhoneState.None);

        _bClickLock = false;
    }


    public CInvenSlot GetInvenSlot(int nSlot)
    {
        return m_cUIInventory.m_listInvenSlot[nSlot];
    }


    public void SetMoney(int num)
    {
        ins_txtMoney.text = num.ToString();
    }


    public void GetAddMoney(int nMoney)
    {
        ins_cUIInventory.m_cPlayerInfo.m_nMoney += nMoney;
        ins_txtMoney.text = ins_cUIInventory.m_cPlayerInfo.m_nMoney.ToString();
    }

    public void IsOpenPhone(bool IsPhone, bool IsPhoneBg)
    {
        gameObject.SetActive(IsPhone);
        ins_ImgCUIPhone.gameObject.SetActive(IsPhoneBg);
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
