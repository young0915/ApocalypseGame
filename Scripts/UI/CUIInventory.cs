using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// 인벤토리에서는 아이템을 보관할 수 있는 것 뿐만 아니라.
// 플레이어 상태와 프로필을 볼 수 있다.
public class CUIInventory : MonoBehaviour
{
    // 내부적으로 관리.
    [SerializeField] private List<CInvenSlot> _listInvenSlot = new List<CInvenSlot>();
    public List<CInvenSlot> m_listInvenSlot { get { return _listInvenSlot; } }

    [SerializeField] private CUIPlayerStats ins_cUIPlayerStats;

    [SerializeField] private Transform ins_traInvenSlotParent = null;                                  // 슬롯(자식객체)이 부모 객체로 들어갈 것.
    [SerializeField] private Transform ins_traPlayerStat = null;                                         //  스태미너를 만들기 위한 부모객체.
    [SerializeField] private RawImage ins_imgRendererTexture = null;                               // 플레이어 프로필에 들어가는 것.
    [SerializeField] private Toggle ins_btnDeleteItem;
    public Toggle m_btnDeletItem { get { return ins_btnDeleteItem; } }

    [SerializeField] private CSOPlayerInfo ins_cPlayerIfno;
    [SerializeField] private CSOItem ins_cSoItemtest;
    public CSOPlayerInfo m_cPlayerInfo { get { return ins_cPlayerIfno; } }
    private EmInvenType _eInvenType;
    private EmWeapon _eWeapon;

    #region [code] const.
    private const int _nMaxAllSLot = 19;                                                                        // 전체 슬롯.
    private const int _nInvenSlot = 14;                                                                           // 총 19개지만 4개는 메인으로 들어갈 InvenSlot. 
    private const int _nMaxItemCnt = 30;                                                                       // 아이템 수량.
    private const string _strInvenPath = "Prefab/UI/Slot/CInvenSlot";
    private const string _strProfilePath = "Prefab/Player/PlayerProfile";
    #endregion



    public IEnumerator CorMakeInvenSlot()
    {
        if (ins_traInvenSlotParent)
        {
            if (_listInvenSlot.Count >= _nMaxAllSLot)
            {
                yield break;
            }
            else
            {
                for (int i = 0; i < _nMaxAllSLot; i++)
                {
                    CInvenSlot cInven = new CInvenSlot();
                    cInven = CResourceLoader.Load<CInvenSlot>(_strInvenPath);
                    cInven.name = "Inven" + i.ToString("00");

                    cInven = Instantiate(cInven);
                    cInven.transform.parent = ins_traInvenSlotParent;
                    _listInvenSlot.Add(cInven);

                    #region [code] _nInvenSlot의 칸 타입에 따라 이미지 설정.
                    _listInvenSlot[i].m_imgInveIcon.gameObject.SetActive(false);
                    _listInvenSlot[i].SetItemInfoImg(EmInvenType.Empty);
                    if (i > _nInvenSlot)
                    {
                        _listInvenSlot[i].m_imgInveIcon.gameObject.SetActive(true);
                        if (i == 15)
                        {
                            _listInvenSlot[i].SetItemInfoImg(EmInvenType.Medical);
                        }
                        else if (i == 16)
                        {
                            _listInvenSlot[i].SetItemInfoImg(EmInvenType.Weapon);
                        }
                        else if (i == 17)
                        {
                            _listInvenSlot[i].SetItemInfoImg(EmInvenType.Bullet);
                        }
                        else
                        {
                            _listInvenSlot[i].SetItemInfoImg(EmInvenType.Armor);
                        }
                    }
                    #endregion

                } // for of end.
            }
        }// if (ins_traInvenSlotParent) of end.


        ins_imgRendererTexture.texture = CResourceLoader.Load<RenderTexture>(_strProfilePath);

        CUIStats stat = new CUIStats((int)ins_cPlayerIfno.m_fSpeed, ins_cPlayerIfno.m_nAttack,
            ins_cPlayerIfno.m_nDefense, ins_cPlayerIfno.m_nRecovery,
            ins_cPlayerIfno.m_nCriticalHit);

        ins_cUIPlayerStats.SetStats(stat);

        yield return new WaitForSeconds(0.0f);
    }// CorMakeInvenSlot of end.


    // 빈슬롯에 아이템 넣기.
    public void SortItem(CItemInfo cItem, int nCnt = 1)
    {
        if (_listInvenSlot[_nInvenSlot].m_eInvenType != EmInvenType.Empty)
        {
            Debug.Log(_listInvenSlot[_nInvenSlot].m_eInvenType);
            // 인벤토리에 아이템이 꽉차서 공간 부족.
            StartCoroutine(CUIManager.Inst.CorWarning(EmWarningType.Inventory));
        }

        for (int i = 0; i <= _nInvenSlot; i++)
        {

            if (_listInvenSlot[i].m_cItem != null && (_listInvenSlot[i].m_cItem.m_strItemName == cItem.m_strItemName) &&
                (_listInvenSlot[i].m_nItemCnt < _nMaxItemCnt))
            {
                if (_listInvenSlot[i].m_cItem.m_eItemType == EmItemType.Weapon)
                {
                    _listInvenSlot[i + 1].AddItem(cItem, nCnt);
                }
                else
                {
                    _listInvenSlot[i].SetItemCnt(nCnt);
                }
                return;
            }
            else if ((_listInvenSlot[i].m_nItemCnt > _nMaxItemCnt) || _listInvenSlot[i].m_cItem == null)
            {
                _listInvenSlot[i].AddItem(cItem, nCnt);
                return;
            }
        }// for of end.

    }


    // 아이템 삭제.
    public void OnClickDeleteItem()
    {

        for (int i = 0; i < _nMaxAllSLot; i++)
        {
            if (ins_btnDeleteItem.isOn == true)
            {
                if (_listInvenSlot[i].m_bIsClickItem == true)
                {
                    _listInvenSlot[i].ClearSlot();
                }
            }
            _listInvenSlot[i].SeteleteResetSlot();
        }
    }

    public bool GetMaterialCheck(string strItemName, int nItemCnt)
    {
        // 전체를 슬롯을 확인하고.
        for (int i = 0; i < _listInvenSlot.Count; i++)
        {
            if (_listInvenSlot[i].m_cItem != null)
            {
                // 물건이 있는지 확인.
                if (_listInvenSlot[i].m_cItem.m_strItemName
                    == strItemName)
                {
                    if (_listInvenSlot[i].m_nItemCnt >= nItemCnt)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void GetUseMaterial(string strName, int nItemCnt)
    {
        for (int i = 0; i < _listInvenSlot.Count; i++)
        {
            if (_listInvenSlot[i].m_cItem != null)
            {
                // 물건이 있는지 확인.
                if (_listInvenSlot[i].m_cItem.m_strItemName
                    == strName)
                {
                    _listInvenSlot[i].SetUseItem(nItemCnt);
                }
            }
        }
    }

}

