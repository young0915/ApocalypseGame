using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CQuestReward : MonoBehaviour
{
    [SerializeField] private CSOItem ins_cSOItem;
    [SerializeField] private Material ins_Matorgin;                         // 원본.
    [SerializeField] private Material ins_MatMouseOver;                 // OnMouseOver 했을 시 발생하는 함수.
    [SerializeField] private GameObject ins_objQuestPoint;

    private EmItemType _eItemType;

    // 마우스가 아이템 박스에 들어왔을 때.
    private void OnMouseOver()
    {
        gameObject.GetComponent<MeshRenderer>().material = ins_MatMouseOver;
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<MeshRenderer>().material = ins_Matorgin;
    }

    private void OnMouseDown()
    {
        CUIManager.Inst.m_cUIPhone.m_cUIInventory.SortItem(ins_cSOItem.m_listItem[20]);
        CUIManager.Inst.m_cUIPhone.IsOpenPhone(false, true);

        if (CUIManager.Inst.m_cUIQuest != null)
        {
             CUIManager.Inst.m_cUIQuest.OnRemoveQuest(3);
        }

        StartCoroutine(CorCoolTime());
    }

    private IEnumerator CorCoolTime()
    {
        yield return new WaitForSeconds(2.0f);
        gameObject.SetActive(false);
    }


    private void Update()
    {
        ins_objQuestPoint.gameObject.transform.Rotate(Vector3.up * Time.deltaTime *50);
    }
}
