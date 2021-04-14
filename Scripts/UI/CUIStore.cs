using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// 시리아를 사용할 예정.
/// </summary>
public class CUIStore : CBaseUI
{
    [SerializeField] private CUIStoreScrollView ins_cUIStoreScrollView;

    //BasicListAdapter
    private EmItemType _eStoreType;
    private bool _bClickLock = false;


    public void OnClickStoreTab(int nIdx)
    {
        if (_bClickLock) return;

        EmItemType eStoreType = (EmItemType)nIdx;

        if (_eStoreType == eStoreType)
            return;

        SetStore(eStoreType);
    }

    private void SetStore(EmItemType eStoreType)
    {

        switch (eStoreType)
        {
            case EmItemType.Medical:
                ins_cUIStoreScrollView.SetMakeDataModel(EmItemType.Medical);
                break;

            case EmItemType.Weapon:

                ins_cUIStoreScrollView.SetMakeDataModel(EmItemType.Weapon);

                break;

        }
    }

    public override void Open(Action<EmClickState> callBack)
    {
        base.Open(callBack);
    }

    public override void Close(bool bDestroy = true)
    {
        base.Close(bDestroy);
        CUIManager.Inst.m_cUIPhone.IsOpenPhone(false, true);
    }
}
