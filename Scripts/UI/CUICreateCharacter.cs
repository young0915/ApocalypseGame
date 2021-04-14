using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CUICreateCharacter : CBaseUI
{
    private EmProfessional _eProfessional = EmProfessional.None;
    [SerializeField] private CSOPlayerStats ins_cSOPlayerStats = null;
    [SerializeField] private CSOPlayerInfo ins_cSOPlayerInfo = null;
    public CSOPlayerInfo m_cSoPlayerInfo { get { return ins_cSOPlayerInfo; } }
    [SerializeField] private List<TextMeshProUGUI> ins_listTxtCreatCharacter = new List<TextMeshProUGUI>();

    private CPlayer _cPlayer;
    private bool _bClickLock = false;
    private const float _fSec = 0.7f;
    private Transform StartPos;

    public void Initialization()
    {
        SetTextInfo();
    }

    private void SetTextInfo()
    {
        for (int i = 0; i < ins_listTxtCreatCharacter.Count; i++)
        {
            ins_listTxtCreatCharacter[i].text
                = ins_cSOPlayerStats.m_listPlayerStat[i].m_strProfessionalName.ToString();
        }
    }

    public void OnClickCreateCharacter(int nIdx)
    {
        if (_bClickLock) return;

        EmProfessional eProfessional = (EmProfessional)nIdx;


        if (_eProfessional == eProfessional)
            return;

        SetCreateCharacter(eProfessional);
    }

    private void SetCreateCharacter(EmProfessional eProfessional)
    {
        string strCharacterPath = string.Empty;
        int nChracter = 0;
        switch (eProfessional)
        {
            case EmProfessional.Orignal:
                strCharacterPath = String.Format("{0}{1}", CDataManager.m_strPlayerFolder.ToString(), 1.ToString("00"));
                nChracter = 0;
                break;

            case EmProfessional.Medical:
                strCharacterPath = String.Format("{0}{1}", CDataManager.m_strPlayerFolder.ToString(), 2.ToString("00"));
                nChracter = 1;

                break;

            case EmProfessional.Soldier:
                strCharacterPath = String.Format("{0}{1}", CDataManager.m_strPlayerFolder.ToString(), 3.ToString("00"));
                nChracter = 2;

                break;
        }

        Quaternion quaternion = new Quaternion();
        quaternion.eulerAngles = new Vector3(0, 0, 0);

        _cPlayer = CPlayer.GetPlayerInstance(null, Vector3.zero, quaternion, true, strCharacterPath);


        ins_cSOPlayerInfo.SetCSOPlayerInfo((EmProfessional)ins_cSOPlayerStats.m_listPlayerStat[nChracter].m_nId,
        EmWeapon.None,
        ins_cSOPlayerStats.m_listPlayerStat[nChracter].m_nHp,
        0,
        0,
        ins_cSOPlayerStats.m_listPlayerStat[nChracter].m_fSpeed,
        ins_cSOPlayerStats.m_listPlayerStat[nChracter].m_nAttack,
        ins_cSOPlayerStats.m_listPlayerStat[nChracter].m_nDefense,
        ins_cSOPlayerStats.m_listPlayerStat[nChracter].m_nRecovery,
        ins_cSOPlayerStats.m_listPlayerStat[nChracter].m_nCriticalHit);

        StartCoroutine(CorCoolTime());
    }

    private IEnumerator CorCoolTime()
    {
        yield return new WaitForSeconds(_fSec);
        yield return StartCoroutine(CUIManager.Inst.CorIntro());
        Close();
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
