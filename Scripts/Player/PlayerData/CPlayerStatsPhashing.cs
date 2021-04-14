using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;


public class CPlayerStatsPhashing : MonoBehaviour
{
    [SerializeField] private CSOPlayerStats ins_cSOPlayerStats;

    private Dictionary<int, CPlayerStats> _dicPlayerStats = new Dictionary<int, CPlayerStats>();
    private CPlayerStats _cPlayerStats;

    void Start()
    {
        string Path = "Texts/CreatCharacterdata";
        var Json = Resources.Load<TextAsset>(Path).text;
        var ArrPlayerStas = JsonConvert.DeserializeObject<CPlayerStats[]>(Json);

        foreach (var data in ArrPlayerStas)
        {
            _dicPlayerStats.Add(data.m_nId, data);
            ins_cSOPlayerStats.m_listPlayerStat[data.m_nId-1] = new CPlayerStats(
                data.m_nId,
                data.m_strProfessionalName,
                data.m_nHp,
                data.m_nLv,
                data.m_fSpeed,
               data.m_nAttack,
               data.m_nDefense,
               data.m_nRecovery,
               data.m_nCriticalHit);
        }
    }

}
