using UnityEngine;
using System.Collections.Generic;

//[CreateAssetMenu(fileName = "CSOSkillInfo", menuName = "ScriptableObject", order = int.MaxValue)]
public class CSOSkillInfo : ScriptableObject
{
    public List<CSkillData> m_cSkillDatalist = new List<CSkillData>();
}

[System.Serializable]
public class CSkillData
{
    public int m_nId;
    public string m_strSkillName;
    public string m_strSkillContent;

   public CSkillData(int nId, string strSkillName, string strSkillContent)
    {
        this.m_nId = nId;
        this.m_strSkillName = strSkillName;
        this.m_strSkillContent = strSkillContent;
    }

}