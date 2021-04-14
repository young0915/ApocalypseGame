using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CUIMain : CBaseUI
{
    [SerializeField] private List<Button> ins_btnlistGameMenu = new List<Button>();

    private bool _bClickLock = false;

    public void Initialization()
    {
    }


    // 설정.
    public void OnClickGameMenu(int nIdx)
    {
        if (_bClickLock)
            return;
        _bClickLock = true;

        SetLayerMenu(nIdx);

        _bClickLock = false;
    }

    // 기능.
    public void SetLayerMenu(int nIdx)
    {
        switch(nIdx)
        {
            // 게임 시작.
            case 0:
                StartCoroutine(CUIManager.Inst.CorNickNameInputFiled());
                CUIManager.Inst.m_cUIMain.Close(false);
                break;

                // 환경설정.
            case 1:
                StartCoroutine(CUIManager.Inst.CorPreferences());

                break;

            // 게임 종료.
            case 2:
                Application.Quit();

                break;
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
