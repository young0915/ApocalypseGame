using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// 슬롯쪽에서 인벤토리에 넣도록 한다.
public class CItemBoxSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CSOItem ins_cSOItem;                                   // 아이템.
    [SerializeField] private Image ins_imgItem;                                       // 아이템 이미지.

    private int _nItemId;
    [HideInInspector] public int m_nItemId { get { return _nItemId; } }

    private CItemInfo _cItem;

    private void Start()
    {
        CUIManager.Inst.m_cUIPhone.IsOpenPhone(true, false);

        _nItemId = UnityEngine.Random.Range(0, 6);
        if (_nItemId > 2)
        {
            _nItemId = 14 + _nItemId;
        }
        SetItem();
        CUIManager.Inst.m_cUIPhone.m_cUIInventory.SortItem(ins_cSOItem.m_listItem[_nItemId]);
        CUIManager.Inst.m_cUIPhone.IsOpenPhone(false, true);
    }

    private void SetItem()
    {
        ins_imgItem.sprite = ins_cSOItem.m_listItem[_nItemId].m_ItemSprite;
    }

    public void OnClickItemInfo()
    {
        StartCoroutine(CUIManager.Inst.CorItemInfo(transform.position, EmInfoType.ItemBox, (_cItem.m_nId)));
    }

    #region [code] EventSystems
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(CUIManager.Inst.CorItemInfo(transform.position, EmInfoType.ItemBox, _nItemId));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CUIManager.Inst.m_cUIItemInfo.Close(false);
    }

    #endregion
}
