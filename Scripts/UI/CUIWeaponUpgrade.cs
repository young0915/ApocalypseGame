using TMPro;
using System;
using UnityEngine;
using System.Collections.Generic;

public class CUIWeaponUpgrade : CBaseUI
{
    [SerializeField] private CSOItem ins_cSOItem;
    [SerializeField] private RectTransform ins_RectTransfrom;
    [SerializeField] private TextMeshProUGUI ins_txtTitle;
    [SerializeField] private TextMeshProUGUI ins_txtBuyBtn;

    [SerializeField] private Transform ins_traSlot;

    [SerializeField] private List<CUIUpgradeSlot> ins_cUIUpgradeSlot = new List<CUIUpgradeSlot>();
    public List<CUIUpgradeSlot> m_cUIUpgradeSlot { get { return ins_cUIUpgradeSlot; } }
    private int _nMoney = 0;
    private int _nItemId = 0;
    private int _nIron = 0;
    private int _nTape = 0;
    private int _nWirecutter = 0;

    public void Initialization(int nId, int nMoney)
    {
        this._nItemId = nId;
        this._nMoney = nMoney;

        ins_cUIUpgradeSlot[0].SetItem(nId);
        ins_cUIUpgradeSlot[1].SetItem(nId);
        ins_cUIUpgradeSlot[2].SetItem(nId);

        SetTextInfo();
    }

    private void SetTextInfo()
    {
        ins_txtTitle.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 26);
        ins_txtBuyBtn.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 27);
    }



    public void OnClickBuy()
    {
        CUIManager.Inst.m_cUIPhone.IsOpenPhone(true, false);

        // 구매 완료.
        CUIManager.Inst.m_cUIPhone.m_cUIInventory.SortItem(ins_cSOItem.m_listItem[_nItemId]);
        StartCoroutine(CUIManager.Inst.CorWarning(EmWarningType.PurchaseComplete));

        CUIManager.Inst.m_cUIPhone.SetMoney(_nMoney -= ins_cSOItem.m_listItem[_nItemId].m_nMoney);
        CUIManager.Inst.m_cUIPhone.m_cUIInventory.GetUseMaterial(ins_cSOItem.m_listItem[0].m_strItemName, _nIron);
        CUIManager.Inst.m_cUIPhone.m_cUIInventory.GetUseMaterial(ins_cSOItem.m_listItem[1].m_strItemName, _nTape);
        CUIManager.Inst.m_cUIPhone.m_cUIInventory.GetUseMaterial(ins_cSOItem.m_listItem[2].m_strItemName, _nWirecutter);

        Close(false);
    }

    public CItemInfo GetItemInfo(int num)
    {
        return m_cUIUpgradeSlot[num].m_cItemInfo;
    }


    public override void Close(bool bDestroy = true)
    {
        base.Close(bDestroy);
        Destroy(gameObject);
    }


    public override void Open(Action<EmClickState> callBack)
    {
        base.Open(callBack);
    }

}
