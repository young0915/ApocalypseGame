using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

public class CZombieDataPhashing : MonoBehaviour
{
    [SerializeField] private CSOZombieInfo ins_cSOZombieInfo;

    private Dictionary<int, CZombieInfo> _dicZobieInfo = new Dictionary<int, CZombieInfo>();
    private CZombieInfo _cZombie;

    void Start()
    {
        string Path = "Texts/ZombieData";
        var Json = Resources.Load<TextAsset>(Path).text;
        var ArrZombie = JsonConvert.DeserializeObject<CZombieInfo[]>(Json);

        foreach(var data in ArrZombie)
        {
            _dicZobieInfo.Add(data.m_nId, data);
            ins_cSOZombieInfo.m_listcZombieInfo[data.m_nId] = new CZombieInfo(
                data.m_eZombieType,
                data.m_nId,
                data.m_strLevel,
                data.m_strDescript,
                data.m_nHp,
                data.m_fSpeed,
                data.m_nAttack,
                data.m_nCriticalHit
                );
        }
    }

}
