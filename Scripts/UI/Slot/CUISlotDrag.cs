using UnityEngine;
using UnityEngine.UI;

public class CUISlotDrag : MonoBehaviour
{
    // m_in은 instance의 약자.
    static public CUISlotDrag m_in;

    [HideInInspector] public CInvenSlot m_cUIdragSlot;

    [SerializeField] private Image ins_ImgItem;
    [HideInInspector] public RectTransform m_traSlotTransform;

    void Start()
    {
        m_in = this;
        m_traSlotTransform = gameObject.GetComponent<RectTransform>();
    }

    public void SetDragImage(Image ImgItem)
    {
        this.ins_ImgItem.sprite = ImgItem.sprite;
        SetColorImg(1.0f);
    }

    public void SetColorImg(float falpha)
    {
        Color color = ins_ImgItem.color;
        color.a = falpha;
        ins_ImgItem.color = color;
    }

}
