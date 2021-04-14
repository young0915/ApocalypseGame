using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CUIUpgradeSlot : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public enum EmWeaponUpgrade
    {
        Iron,
        Tape,
        Wirecutter
    }


    [SerializeField] private CSOItem ins_cSoItem;
    private CItemInfo _cItemInfo;
    public CItemInfo m_cItemInfo { get { return _cItemInfo; } }
    [SerializeField] private Image ins_ImgItem;
    [SerializeField] private TextMeshProUGUI ins_txtItemCnt;
   [SerializeField] private EmWeaponUpgrade _eWeaponUpgrade;

    private int _nId = 0;                               // 아이템 아이디.
    private int _nItemCnt = 0;
    private string _strItemName = string.Empty;

  

    public void SetItem(int nId)
    {
        this._nId = nId;


        int nImg = 0;
        int nItemCnt = 0;
        int nItemData = 0;
        switch (_eWeaponUpgrade)
        {
            case EmWeaponUpgrade.Iron:
                nImg = 0;
                nItemCnt = ins_cSoItem.m_listItem[_nId].m_nIron;
                nItemData = 33;
                break;

            case EmWeaponUpgrade.Tape:
                nImg = 1;
                nItemCnt = ins_cSoItem.m_listItem[_nId].m_nTape;
                nItemData = 34;
                break;

            case EmWeaponUpgrade.Wirecutter:
                nImg = 2;
                nItemCnt = ins_cSoItem.m_listItem[_nId].m_nWirecutter;
                nItemData = 35;

                break;
        }

         this.ins_ImgItem.sprite = ins_cSoItem.m_listItem[nImg].m_ItemSprite;

        SetTextInfo(nImg, nItemCnt, nItemData);
    }

    private void SetTextInfo(int nImg, int nItemcnt, int nItemData)
    {
        this._strItemName = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, nItemData).ToString();
        ins_txtItemCnt.text = string.Format("{0}   {1}   {2}", _strItemName, CDataManager.Inst.m_strStonevote, nItemcnt);
    }


    #region [code] UnityEngine EventSystem
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(CUIManager.Inst.CorItemInfo(gameObject.transform.position, EmInfoType.ItemBox, _nId));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CUIManager.Inst.m_cUIItemInfo.Close(false);
    }
    #endregion
}
