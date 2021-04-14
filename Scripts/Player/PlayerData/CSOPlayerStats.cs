using UnityEngine;
using System.Collections.Generic;

//[CreateAssetMenu(fileName = "CSOPlayerStats", menuName = "ScriptableObject", order = int.MaxValue)]
public class CSOPlayerStats : ScriptableObject
{
    public List<CPlayerStats> m_listPlayerStat = new List<CPlayerStats>();
}

// 정보.
[System.Serializable]
public class CPlayerStats
{
    public int m_nId;
    public string m_strProfessionalName;
    public int m_nHp;
    public int m_nLv;
    public float m_fSpeed;
    public int m_nAttack;                            // 공격력.
    public int m_nDefense;                         // 방어력.
    public int m_nRecovery;                        // 회복력.
    public int m_nCriticalHit;                      // 크리티컬.

    public CPlayerStats(int nId,string strProfessionName, int nHp, int nLv,float fSpeed, int nAttack, int nDefense, int nRecovery, int nCriticalHit)
    {
        this.m_nId = nId;
        this.m_strProfessionalName = strProfessionName;
        this.m_nHp = nHp;
        this.m_nLv = nLv;
        this.m_fSpeed = fSpeed;
        this.m_nAttack = nAttack;
        this.m_nDefense = nDefense;
        this.m_nRecovery = nRecovery;
        this.m_nCriticalHit = nCriticalHit;
    }
}