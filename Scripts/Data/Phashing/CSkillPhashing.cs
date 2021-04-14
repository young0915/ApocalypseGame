using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

public class CSkillPhashing : MonoBehaviour
{
    [SerializeField] private CSOSkillInfo _cSOSkillInfo;

    private Dictionary<int, CSkillData> _dicSkill = new Dictionary<int, CSkillData>();
    private CSkillData _cSkillData;

    void Start()
    {
        string Path = "Texts/SkillInfoData";
        var Json = Resources.Load<TextAsset>(Path).text;
        var ArrSkill = JsonConvert.DeserializeObject<CSkillData[]>(Json);

        foreach(var data in ArrSkill)
        {
            _dicSkill.Add(data.m_nId, data);
            _cSOSkillInfo.m_cSkillDatalist[data.m_nId] = new CSkillData(
                data.m_nId,
                data.m_strSkillName,
                data.m_strSkillContent
                );
        }
    }

   
}
