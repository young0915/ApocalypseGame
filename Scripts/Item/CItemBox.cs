using UnityEngine;
using System.Collections;

public class CItemBox : MonoBehaviour
{
    [SerializeField] private SpriteRenderer ins_spriteSign;
    [SerializeField] private Material ins_Matorgin;                         // 원본.
    [SerializeField] private Material ins_MatMouseOver;                 // OnMouseOver 했을 시 발생하는 함수.

    private void Start()
    {
        if (CUIManager.Inst.m_cUICreatCharacter.m_cSoPlayerInfo.m_eProfessional == EmProfessional.Medical)
        {
            ins_spriteSign.gameObject.SetActive(true);
        }
        ins_spriteSign.gameObject.SetActive(false);
    }



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
        StartCoroutine(CUIManager.Inst.CorItemBox());

        StartCoroutine(CorCoolTime());
    }

    private IEnumerator CorCoolTime()
    {
        yield return new WaitForSeconds(2.0f);
        gameObject.SetActive(false);
    }

}
