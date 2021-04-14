using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CInvenSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private CItemInfo _cItem;
    public CItemInfo m_cItem { get { return _cItem; } }                                          // 아이템 정보.

    [SerializeField] private Button ins_btnInvenSlot;

    [SerializeField] private Image ins_imgItem;
    public Image m_imgItem { get { return ins_imgItem; } }                               // 아이템 이미지.

    [SerializeField] private Image ins_imgInvenIcon;                                                                // 타입에 따른 아이콘.
    public Image m_imgInveIcon { get { return ins_imgInvenIcon; } }

    [SerializeField] private TextMeshProUGUI ins_txtItemCnt;

    private int _nItemCnt;                             // 슬롯에 담긴 아이템 수.
    public int m_nItemCnt { get { return _nItemCnt; } }

    private EmItemType _eItemType;                 // 아이템 타입.
    public EmItemType m_eItemType { get { return _eItemType; } }

    [SerializeField] private RectTransform ins_tra;

    private EmInvenType _eInvenType;
    public EmInvenType m_eInvenType { get { return _eInvenType; } }

    private EmWeapon _eWeapon = EmWeapon.None;
    public EmWeapon m_eWeapon { get { return _eWeapon; } }

    private bool _bIsClickItem = false;
    public bool m_bIsClickItem { get { return _bIsClickItem; } }

    private Vector3 _VecOriginPos;

    private void Start()
    {
        _VecOriginPos = transform.position;
    }

    #region [code] const Variable.
    private const string _strHealthPotion = "Texture/UI/InvenIcon/Icon_Drug";
    private const string _strWeapon = "Texture/UI/InvenIcon/Icon_Gun";
    private const string _strWeaponsAccessories = "Texture/UI/InvenIcon/Icon_Bullet";
    private const string _strArmor = "Texture/UI/InvenIcon/Icon_Tool";
    private const string _strHeathPotionInvenName = "Inven15(Clone)";
    private const string _strWeaponInvenName = "Inven16(Clone)";
    private const string _strBulletInvenName = "Inven17(Clone)";
    private const string _strbombInvenName = "Inven18(Clone)";
    private const string _strSword = "총칼";
    private const string _strBat = "배트";
    private const string _strHybrid = "하이브리드";
    private const string _strRifle = "소총";
    private const string _strCrossbow = "크로스보우";
    #endregion

    /// <summary>
    ///  16칸 HealthPotion, 17칸  Weapon, 18칸 WeaponsAccessories, 19칸 Armor.
    /// </summary>
    /// <param eInvenType="eInvenType">15칸 까지는 None으로 설정.</param>
    public void SetItemInfoImg(EmInvenType eInvenType)
    {
        this._eInvenType = eInvenType;
        string strIconPath = string.Empty;
        switch (eInvenType)
        {
            case EmInvenType.Medical:
                strIconPath = _strHealthPotion;
                break;

            case EmInvenType.Weapon:
                strIconPath = _strWeapon;
                break;

            case EmInvenType.Bullet:

                strIconPath = _strWeaponsAccessories;

                break;

            case EmInvenType.Armor:
                strIconPath = _strArmor;

                break;

            case EmInvenType.Empty:
                break;

        }
        // ins_imgInvenIcon.gameObject.SetActive(bIsenabled);
        ins_imgInvenIcon.sprite = CResourceLoader.Load<Sprite>(strIconPath);
    }

    // 아이템 추가          아이템            아이템 수량. 
    public void AddItem(CItemInfo cItem, int nItemCnt = 1)
    {
        this._cItem = cItem;
        this._nItemCnt = nItemCnt;

        // 세팅.
        SetItemState(this._cItem);
        SetColor(1);
        _eInvenType = (EmInvenType)_cItem.m_eItemType;
    }

    private void SetItemState(CItemInfo cItem)
    {
        if (_cItem == null)
        {
            this.ins_imgItem.enabled = false;
        }
        else
        {
            this.ins_imgItem.enabled = true;
            this.ins_imgItem.sprite = cItem.m_ItemSprite;
            this._cItem.m_eItemType = cItem.m_eItemType;

            if (_nItemCnt > 1)
            {
                this.ins_txtItemCnt.gameObject.SetActive(true);
                this.ins_txtItemCnt.text = _nItemCnt.ToString();
            }
        }
    }

    // 아이템 수량.
    public void SetItemCnt(int nCnt)
    {
        this.ins_txtItemCnt.gameObject.SetActive(true);
        this._nItemCnt += nCnt;
        this.ins_txtItemCnt.text = _nItemCnt.ToString();
        if (nCnt <= 0)
        {
            // 클리어.
            ClearSlot();
        }
    }

    // 아이템 클리어.
    public void ClearSlot()
    {
        this._cItem = null;
        this._nItemCnt = 0;
        this.ins_imgItem.sprite = null;
        this._eInvenType = EmInvenType.Empty;
        SetColor(0);

        this.ins_txtItemCnt.text = string.Empty;
        this.ins_txtItemCnt.gameObject.SetActive(false);

    }

    // 이미지 투명도 조절.
    public void SetColor(float alpha)
    {
        Color color = ins_imgItem.color;
        color.a = alpha;
        ins_imgItem.color = color;
    }

    //deleteReset
    public void SeteleteResetSlot()
    {
        _bIsClickItem = false;
        ins_btnInvenSlot.interactable = true;
    }

    public void SetUseItem(int num =1)
    {
        _nItemCnt -= num;

        if(_nItemCnt <0)
        {
            ClearSlot();
        }
    }


    #region  [code] EventSystems

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (_cItem != null)
            {
                _bIsClickItem = true;
                ins_btnInvenSlot.interactable = false;
            }
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log(_eInvenType);
            if (_cItem == null)
            {
                SetItemCnt(-1);
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (_cItem != null)
            {
                CUISlotDrag.m_in.m_cUIdragSlot = this;
                CUISlotDrag.m_in.SetDragImage(ins_imgItem);
                CUISlotDrag.m_in.transform.position = eventData.position;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_cItem != null)
        {
            CUISlotDrag.m_in.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CUISlotDrag.m_in.SetColorImg(0);
        CUISlotDrag.m_in.m_cUIdragSlot = null;
    }


    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (CUISlotDrag.m_in.m_cUIdragSlot != null && _eInvenType == EmInvenType.Empty ||
              CUISlotDrag.m_in.m_cUIdragSlot.m_cItem.m_eItemType == (EmItemType)_eInvenType)
            {
                ChangeSlot();
                if (gameObject.name == _strWeaponInvenName)
                {
                    OnWeaponSlot();
                }
                else if (gameObject.name == _strHeathPotionInvenName)
                {
                    OnHeathPotionSlot();
                }
                else if (gameObject.name == _strBulletInvenName)
                {
                    OnBulletSlot();
                }
            }
        }
    }

    private void OnHeathPotionSlot()
    {
        CUIManager.Inst.m_cUIPlayerMain.m_ImgHpPotion.sprite = _cItem.m_ItemSprite;
        CUIManager.Inst.m_cUIPlayerMain.m_ImgHpPotion.enabled = true;
    }

    private void OnWeaponSlot()
    {
        this._eWeapon = EmWeapon.None;

        if (_cItem.m_strItemName.Contains(_strSword))
        {
            this._eWeapon = EmWeapon.Sword;
        }
        else if (_cItem.m_strItemName.Contains(_strBat))
        {
            this._eWeapon = EmWeapon.Bat;
        }
        else if (_cItem.m_strItemName.Contains(_strCrossbow))
        {
            this._eWeapon = EmWeapon.Crossbow;
        }
        else if (_cItem.m_strItemName.Contains(_strHybrid))
        {
            this._eWeapon = EmWeapon.Hybrid;
        }
        else if (_cItem.m_strItemName.Contains(_strRifle))
        {
            this._eWeapon = EmWeapon.Rifle;
        }

        CUIManager.Inst.m_cUIPlayerMain.m_ImgWeaponIcon.sprite = _cItem.m_ItemSprite;
        CUIManager.Inst.m_cUIPlayerMain.m_ImgWeaponIcon.enabled = true;
        CUIManager.Inst.m_cUIPlayerMain.m_cSoPlayerInfo.m_eWeapon = this._eWeapon;

    }

    private void OnBulletSlot()
    {
        Debug.Log(_nItemCnt);
        CUIManager.Inst.m_cUIPlayerMain.SetBulletCnt(_nItemCnt);
    }

    /// <summary>
    /// 교환하는 함수.
    /// 0칸과 n칸에 다른 아이템들로 존재한다.
    ///  둘중하나의 칸을 옮겨 다른 아이템이 존재하는 칸에 넣었을떄.
    ///  그 칸에 존재한 아이템은 전에 두었던 칸으로 이동.
    /// </summary>
    private void ChangeSlot()
    {
        // 임시 아이템 만들기.
        CItemInfo TempcItem = _cItem;

        // 하나씩 옮기도록 처리.
        AddItem(CUISlotDrag.m_in.m_cUIdragSlot.m_cItem, _nItemCnt);

        if (TempcItem != null)
        {
            // 그 슬롯칸에 위치한 아이템 이름과 같다면.
            if (TempcItem.m_strItemName == CUISlotDrag.m_in.m_cUIdragSlot._cItem.m_strItemName)
            {
                int TempCnt = CUISlotDrag.m_in.m_cUIdragSlot._nItemCnt;
                AddItem(CUISlotDrag.m_in.m_cUIdragSlot.m_cItem, _nItemCnt + TempCnt);
                CUISlotDrag.m_in.m_cUIdragSlot.ClearSlot();
            }
            else
            {
                CUISlotDrag.m_in.m_cUIdragSlot.AddItem(TempcItem, _nItemCnt);
            }
        }
        else
        {
            SetItemCnt(CUISlotDrag.m_in.m_cUIdragSlot._nItemCnt);
            CUISlotDrag.m_in.m_cUIdragSlot.ClearSlot();

        }


    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_cItem != null)
        {
            StartCoroutine(CUIManager.Inst.CorItemInfo(new Vector3(eventData.position.x + 30, eventData.position.y + 30, 0), EmInfoType.Inventory, _cItem.m_nId));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_cItem != null)
        {
            CUIManager.Inst.m_cUIItemInfo.Close(false);
        }
    }
    #endregion

}
