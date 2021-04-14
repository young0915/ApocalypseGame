using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

public class CStatsManager : MonoBehaviour
{
    [SerializeField] private CSOPlayerStats _cSOPlayerStats;

    private Dictionary<int, CPlayerStats> _dicStats = new Dictionary<int, CPlayerStats>();
    private CPlayerStats _cPlayerStats;

    void Start()
    {
        string Path = "Texts/CreatCharacterdata";
        var Json = Resources.Load<TextAsset>(Path).text;
        var ArrStats = JsonConvert.DeserializeObject<CPlayerStats[]>(Json);

      foreach(var data in ArrStats)
        {
            _dicStats.Add(data.m_nId, data);
            _cSOPlayerStats.m_listPlayerStat[data.m_nId] = new CPlayerStats(
                data.m_nId,
                data.m_strProfessionalName,
                data.m_nHp,
                data.m_nLv,
                data.m_fSpeed,
                data.m_nAttack,
                data.m_nDefense,
                data.m_nRecovery,
                data.m_nCriticalHit
                );
        }

    }

}
