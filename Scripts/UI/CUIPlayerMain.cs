using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CUIPlayerMain : CBaseUI
{
    [SerializeField] private CSOPlayerInfo ins_cSoPlayerInfo;
    public CSOPlayerInfo m_cSoPlayerInfo { get { return ins_cSoPlayerInfo; } }
    [SerializeField] private GameObject ins_objMainPanel;

    [Space]
    // Hp Variable.
    [SerializeField] private Slider ins_SliderHp;
    [SerializeField] private Gradient ins_HpGradient;
    [SerializeField] private Image ins_ImgHpFillBg;
    [Space]

    [Space]
    // Exp Variable.
    [SerializeField] private TextMeshProUGUI ins_txtLevel;
    [SerializeField] private Slider ins_SliderExp;
    [SerializeField] private Gradient ins_ExpGradient;
    [SerializeField] private Image ins_ImgExpFillBg;
    [Space]


    [Space]
    [SerializeField] private Image ins_ImgWeaponIcon;
    public Image m_ImgWeaponIcon { get { return ins_ImgWeaponIcon; } }
    [SerializeField] private Image ins_ImgHpPotion;
    public Image m_ImgHpPotion { get { return ins_ImgHpPotion; } }
    [SerializeField] private RawImage ins_RImgMap;
    [SerializeField] private GameObject ins_objPlayerMap;
    [Space]

    [SerializeField] private List<CSkillSlot> ins_listcSkillIcon = new List<CSkillSlot>();
    public List<CSkillSlot> m_cSKillSlotlist { get { return ins_listcSkillIcon; } }
    [SerializeField] private List<Image> ins_listImgBulletCnt = new List<Image>();

    [SerializeField] private Button ins_btnPreference;                                          // 환경설정.
    [SerializeField] private Button ins_btnPhone;                                                // 인벤토리.


    private EmSkillNum _eSkillNum;
    private EmSkillType _eSkillType;

    private float _fCoolTime = 0.0f;

    private int _nBulletCnt = 0;
    public int m_nBulletCnt { get { return _nBulletCnt; } }

    private int _nLevel = 1;
    private int _nLevelId = 0;

    private bool _bClickLock = false;
    private bool _bIsMove = false;


    #region const Variable
    private const string _strMapPath = "Prefab/Player/MapRenderer";
    private const string _strNumberPath = "Texture/UI/UINumber/";
    #endregion

    
    public void Initialization()
    {
        // Hp.
        SetMaxHealth(ins_cSoPlayerInfo.m_nHp);
        // Exp.
        SetExp(ins_cSoPlayerInfo.m_nLv);
        // Quest.
        StartCoroutine(CUIManager.Inst.CorQuest());

        if(_nLevel ==1)
        {
            ins_cSoPlayerInfo.m_fCoolTime = 10.0f;
        }
        else if(_nLevel ==2)
        {
            ins_cSoPlayerInfo.m_fCoolTime = 5.0f;
        }
        else
        {
            ins_cSoPlayerInfo.m_fCoolTime = 7.0f;
        }


        // 테스트 완료.
        ins_listcSkillIcon[0].SetData(EmSkillType.Run, ins_cSoPlayerInfo.m_fCoolTime);
        ins_listcSkillIcon[1].SetData((EmSkillType)ins_cSoPlayerInfo.m_eProfessional, ins_cSoPlayerInfo.m_fCoolTime);

        ins_RImgMap.texture = CResourceLoader.Load<RenderTexture>(_strMapPath);

        SetTextInfo();
    }

    private void SetTextInfo()
    {
        ins_txtLevel.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 25).ToString() + _nLevel.ToString();
    }

    #region [code] Hp ProgressBar

    private void SetMaxHealth(int nHealth)
    {
        this.ins_SliderHp.maxValue = nHealth;
        this.ins_SliderHp.value = nHealth;

        this.ins_ImgHpFillBg.color = ins_HpGradient.Evaluate(1f);
    }

    public void SetHealth(int nHealth)
    {
        ins_SliderHp.value = nHealth;
        ins_ImgHpFillBg.color = ins_HpGradient.Evaluate(ins_SliderHp.normalizedValue);
    }

    public void SetDamage(int nDamage)
    {
        ins_cSoPlayerInfo.m_nHp -= nDamage;

        SetHealth(ins_cSoPlayerInfo.m_nHp);
    }

    #endregion

    #region [code] Exp
    public void SetExp(int nExp)
    {
        ins_SliderExp.value = nExp;
        ins_ImgExpFillBg.color = ins_ExpGradient.Evaluate(ins_SliderExp.normalizedValue);

        if(ins_SliderExp.maxValue == ins_SliderExp.value)
        {
            _nLevelId++;
            _nLevel = CDBManager.Inst.m_listLevelInfo[_nLevelId].m_nLevel;
            _fCoolTime = (float)CDBManager.Inst.m_listLevelInfo[_nLevelId].m_nCoolTime;

            ins_cSoPlayerInfo.m_nHp = CDBManager.Inst.m_listLevelInfo[_nLevelId].m_nHp;
            SetMaxHealth(ins_cSoPlayerInfo.m_nHp);
        }
    }

    #endregion


    #region [code] Map

    // 네비 숨기는 버튼.
    public void OnClickMoveMap()
    {
        if (_bClickLock)
            return;
        _bClickLock = true;

        if (_bIsMove)
        {
            iTween.MoveTo(ins_objPlayerMap, iTween.Hash("y", 350));
            _bIsMove = false;

        }
        else
        {
            iTween.MoveTo(ins_objPlayerMap, iTween.Hash("y", 500));
            _bIsMove = true;
        }

        _bClickLock = false;
    }

    #endregion

    #region  [code] Bullet Number

    public void SetBulletCnt(int nBulletCnt)
    {
        this._nBulletCnt = nBulletCnt;
        int nQuotient = nBulletCnt / 10;
        int nRemainder = nBulletCnt % 10;
        
        ins_listImgBulletCnt[0].sprite = CResourceLoader.Load<Sprite>(_strNumberPath + nQuotient.ToString());
        ins_listImgBulletCnt[1].sprite = CResourceLoader.Load<Sprite>(_strNumberPath + nRemainder.ToString());
    }

    public void OnClickOpenPreference()
    {
        if (_bClickLock) return;
        _bClickLock = true;

        StartCoroutine(CUIManager.Inst.CorPreferences());

        _bClickLock = false;
    }

    public void OnClickPhone()
    {
        if (_bClickLock) return;
        _bClickLock = true;

        StartCoroutine(CUIManager.Inst.CorPhone());

        _bClickLock = false;
    }

    public void SetUseBullet()
    {
        _nBulletCnt -= 1;
        SetBulletCnt(_nBulletCnt);
    }

    #endregion

    public float GetPlayerCoolTime()
    {
        return ins_cSoPlayerInfo.m_fCoolTime;
    }

    public void IsOpenMain(bool bIsMain, bool bIsMainBg)
    {
        gameObject.SetActive(bIsMain);
        ins_btnPreference.gameObject.SetActive(bIsMainBg);
        ins_btnPhone.gameObject.SetActive(bIsMainBg);
        ins_objMainPanel.SetActive(bIsMainBg);
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
