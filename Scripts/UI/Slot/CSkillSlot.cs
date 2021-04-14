using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class CSkillSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image ins_ImgSkill = null;
    [SerializeField] private Image ins_ImgCoolTime = null;
    public Image m_ImgCoolTime { get { return ins_ImgCoolTime; } }

    private float _fCoolTime = 0.0f;

    private EmSkillType _eSkillType;
 private EmSkillNum _eSkillNum;

    private const string _strSkillIconPath = "Texture/UI/Skill/";


    // 스킬 정보           직업군에 따른 스킬타입   쿨타임          레벨.
    public void SetData(EmSkillType eSkillType, float fCoolTime)
    {
        this._eSkillType = eSkillType;
        this._fCoolTime = fCoolTime;

        int nSkill = 0;
        switch(_eSkillType)
        {
            case EmSkillType.Run:
                nSkill = 0;
                break;

            case EmSkillType.FindtheEnemy:
                nSkill = 1;
                break;

            case EmSkillType.FindItem:
                nSkill = 2; 
                break;

            case EmSkillType.Shield:
                nSkill = 3;
                break;
        }
        ins_ImgSkill.sprite = CResourceLoader.Load<Sprite>(_strSkillIconPath + nSkill.ToString());

    }

    public void SetSkill()
    {
        StartCoroutine(CorSkillCoolTimeSKill());
    }

    private IEnumerator CorSkillCoolTimeSKill()
    {
        ins_ImgCoolTime.fillAmount = 1;
        while (ins_ImgCoolTime.fillAmount > 0)
        {
            ins_ImgCoolTime.fillAmount -=1* Time.deltaTime / _fCoolTime;
            yield return null;
        }
        yield break;
    }


    #region [code] EventSystem.

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(CUIManager.Inst.CorItemInfo(transform.position, EmInfoType.Skill, (int)_eSkillType));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CUIManager.Inst.m_cUIItemInfo.Close(false);
    }

    #endregion

}
