using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class CUIStoreCell : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{

    [SerializeField] private CSOPlayerInfo ins_cSOPlayerInfo;
    [SerializeField] private CSOItem ins_cSOItem;

    [SerializeField] private TextMeshProUGUI ins_txtName;
    [SerializeField] private TextMeshProUGUI ins_txtDescript;
    [SerializeField] private TextMeshProUGUI ins_txtMoney;
    [SerializeField] private Button ins_btnBuyItem;
    [SerializeField] private Image ins_imgItem;

    private EmItemType _eItemType;

    private int _nItemId = 0;
    private int _nMoney = 0;


    public void SetData(CStoreModel cModel)
    {
        CUIManager.Inst.m_cUIPhone.IsOpenPhone(true, false);
        _nItemId = cModel.m_nItemId;

        // OSA 의 버그를 방지하기 위한 것.
        if (cModel.m_eCellState == EmCellState.NotComplete)
        {
            ins_btnBuyItem.interactable = true;
        }
        else
        {
            ins_btnBuyItem.interactable = false;
        }

        ins_txtName.text = ins_cSOItem.m_listItem[_nItemId].m_strItemName.ToString();
        ins_txtDescript.text = ins_cSOItem.m_listItem[_nItemId].m_strItemDescript.ToString();
        ins_txtMoney.text = ins_cSOItem.m_listItem[_nItemId].m_nMoney.ToString();
        ins_imgItem.sprite = ins_cSOItem.m_listItem[_nItemId].m_ItemSprite;
    }

    public void OnClickBuyItem()
    {
        int nPrice = ins_cSOItem.m_listItem[_nItemId].m_nMoney;
        if (ins_cSOPlayerInfo.m_nMoney >= nPrice)
        {
            if (ins_cSOItem.m_listItem[_nItemId].m_nIron.Equals(0) && ins_cSOItem.m_listItem[_nItemId].m_nTape.Equals(0) &&
                ins_cSOItem.m_listItem[_nItemId].m_nWirecutter.Equals(0))
            {
                ins_btnBuyItem.interactable = false;
                StartCoroutine(CorBtnCoolTime());
                ins_cSOPlayerInfo.m_nMoney -= nPrice;
                CUIManager.Inst.m_cUIPhone.m_cUIInventory.SortItem(ins_cSOItem.m_listItem[_nItemId]);
            }
            else
            {
                // 재료 유무 확인.
                if (CUIManager.Inst.m_cUIPhone.m_cUIInventory.GetMaterialCheck(ins_cSOItem.m_listItem[0].m_strItemName, ins_cSOItem.m_listItem[_nItemId].m_nIron) &&
                    CUIManager.Inst.m_cUIPhone.m_cUIInventory.GetMaterialCheck(ins_cSOItem.m_listItem[1].m_strItemName, ins_cSOItem.m_listItem[_nItemId].m_nTape) &&
                  CUIManager.Inst.m_cUIPhone.m_cUIInventory.GetMaterialCheck(ins_cSOItem.m_listItem[2].m_strItemName, ins_cSOItem.m_listItem[_nItemId].m_nWirecutter))
                {
                    // 무기 업그레이드UI 생성.
                    StartCoroutine(CUIManager.Inst.CorWeaponUpgrade(_nItemId, ins_cSOPlayerInfo.m_nMoney));
                }
                else
                {
                    StartCoroutine(CUIManager.Inst.CorWarning(EmWarningType.LackMaterial));
                    return;
                }

            }

        }
        else
        {
            StartCoroutine(CUIManager.Inst.CorWarning(EmWarningType.LackMoney));
        }

    }

    private IEnumerator CorBtnCoolTime()
    {
        yield return new WaitForSeconds(0.5f);
        ins_btnBuyItem.interactable = true;
    }

    #region  [code] EventSystems

    public void OnPointerExit(PointerEventData eventData)
    {
        CUIManager.Inst.m_cUIItemInfo.Close(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(CUIManager.Inst.CorItemInfo(transform.position, EmInfoType.Shop, _nItemId));
    }

    #endregion

}
