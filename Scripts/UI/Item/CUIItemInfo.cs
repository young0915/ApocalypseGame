using System;
using UnityEngine;
using UnityEngine.UI;

public class CUIItemInfo : CBaseUI
{
    [SerializeField] private CSOItem ins_cSoItem;
    [SerializeField] private CSOSkillInfo ins_cSOSkillInfo;                          // 스킬.
    [SerializeField] private Image ins_ImgInfoBg;                         // 아이템 설명 이미지.
    [SerializeField] private Text ins_txtItemName;                        // 아이템 제목 텍스트.
    [SerializeField] private Text ins_txtItemDescript;                        // 아이템 대사 텍스트.

    private int _nSpeed = 0;
    private int _nAttack = 0;
    private int _nDefense = 0;
    private int _nRecovery = 0;
    private int _nCritical = 0;

    private const string _strSign = " : ";


    // 정보창 셋팅    타입                         아이템 넘버     위치.
    public void SetWinInfo(Vector3 VPos,EmInfoType eInfoType, int nId)
    {
        // Layer2 중 마지막 순서로.
        gameObject.transform.SetAsLastSibling();

        gameObject.transform.position = VPos;

        string strName = string.Empty;
        string strDescript = string.Empty;

        ins_ImgInfoBg.gameObject.SetActive(false);
        ins_txtItemName.gameObject.SetActive(false);
        ins_txtItemDescript.gameObject.SetActive(false);

        switch (eInfoType)
        {
            case EmInfoType.Inventory:
            case EmInfoType.ItemBox:
                ins_ImgInfoBg.gameObject.SetActive(true);
                ins_txtItemName.gameObject.SetActive(true);
                ins_txtItemDescript.gameObject.SetActive(true);

                strName = ins_cSoItem.m_listItem[nId].m_strItemName.ToString();
                strDescript = ins_cSoItem.m_listItem[nId].m_strItemDescript.ToString();

                break;

            case EmInfoType.Shop:
                ins_ImgInfoBg.gameObject.SetActive(false);
                ins_txtItemName.gameObject.SetActive(false);
                ins_txtItemDescript.gameObject.SetActive(true);
             

                _nSpeed = (int)ins_cSoItem.m_listItem[nId].m_fSpeed;
                _nAttack = ins_cSoItem.m_listItem[nId].m_nAttack;
                _nDefense = ins_cSoItem.m_listItem[nId].m_nDefense;
                _nRecovery = ins_cSoItem.m_listItem[nId].m_nRecovery;
                _nCritical = ins_cSoItem.m_listItem[nId].m_nCriticalHit;

                strName = string.Empty;
                strDescript = string.Format(CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 16) +"\t"+ _strSign+ _nSpeed.ToString() + "\n" +
                   CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 17) + "\t" + _strSign + _nAttack.ToString() + "\n" +
                     CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 18) + "\t" + _strSign + _nDefense.ToString() + "\n" +
                     CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 19) + "\t" + _strSign + _nRecovery.ToString() + "\n" +
                     CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 20) + "\t" + _strSign + _nCritical.ToString());

                break;

            case EmInfoType.Skill:
                ins_ImgInfoBg.gameObject.SetActive(true);
                ins_txtItemName.gameObject.SetActive(true);
                ins_txtItemDescript.gameObject.SetActive(true);

                strName = ins_cSOSkillInfo.m_cSkillDatalist[nId].m_strSkillName.ToString();
                strDescript = string.Format(ins_cSOSkillInfo.m_cSkillDatalist[nId].m_strSkillContent.ToString(), 
                    CUIManager.Inst.m_cUIPlayerMain.GetPlayerCoolTime());


                break;
        }

        ins_txtItemName.text = strName.ToString();
        ins_txtItemDescript.text = strDescript.ToString();

    }

    public override void Close(bool bDestroy = true)
    {
        base.Close(bDestroy);
    }

    public override void Open(Action<EmClickState> callBack)
    {
        base.Open(callBack);
    }
}
