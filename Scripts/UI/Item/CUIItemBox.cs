using TMPro;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CUIItemBox : CBaseUI
{
    [SerializeField] private TextMeshProUGUI ins_txtTitle;                                                          // 이름 : 보급품.
    [SerializeField] private TextMeshProUGUI ins_txtRecivedItemBtn;
    [SerializeField] private Transform ins_traItemBox;                                                              // 아이템 배치할 곳.

    // 슬롯으로 받아야할 것 같음.
    [SerializeField] private List<CItemBoxSlot> ins_ItemBoxlist = new List<CItemBoxSlot>();

    private const string _strItemBoxSlot = "Prefab/UI/Slot/CItemBoxSlot";

    public void Initialization()
    {
        ins_ItemBoxlist.Clear();
        if (ins_traItemBox)
        {
            int nRandBox = UnityEngine.Random.Range(1, 4);
            for (int i = 0; i <= nRandBox; i++)
            {

                CItemBoxSlot cItem = new CItemBoxSlot();
                cItem = CResourceLoader.Load<CItemBoxSlot>(_strItemBoxSlot);
                cItem = Instantiate(cItem);
                cItem.transform.parent = ins_traItemBox;

                ins_ItemBoxlist.Add(cItem);

            }

        }
        SetTextInfo();
    }

    public void SetTextInfo()
    {
        ins_txtTitle.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 12);
        ins_txtRecivedItemBtn.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 13);
    }

    // 아이템 모두받기 버튼.
    public void OnClickReceiveItem()
    {
        Close(false);
    }

    public override void Open(Action<EmClickState> callBack)
    {
        base.Open(callBack);
    }

    public override void Close(bool bDestroy = true)
    {
        base.Close(bDestroy);
        Destroy(this.gameObject);
    }
}
