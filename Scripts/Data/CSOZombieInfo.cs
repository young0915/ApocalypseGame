using UnityEngine;
using System.Collections.Generic;

//[CreateAssetMenu(fileName = "CSOZombieInfo", menuName = "ScriptableObject", order =int.MaxValue)]
public class CSOZombieInfo : ScriptableObject
{
     public List<CZombieInfo> m_listcZombieInfo = new List<CZombieInfo>();
   
}


[System.Serializable]
public class CZombieInfo 
{
    public EmZombieType m_eZombieType;
    public int m_nId;
    public string m_strLevel;
    public string m_strDescript;
    public int m_nHp;
    public float m_fSpeed;
    public int m_nAttack;
    public int m_nCriticalHit;

    public CZombieInfo(EmZombieType eZombieType, int nId, string strLevel, string strDescript,
        int nHp, float Speed, int nAttack, int nCriticalHit)
    {
        this.m_eZombieType = eZombieType;
        this.m_nId = nId;
        this.m_strLevel = strLevel;
        this.m_strDescript = strDescript;
        this.m_nHp = nHp;
        this.m_fSpeed = Speed;
        this.m_nAttack = nAttack;
        this.m_nCriticalHit = nCriticalHit;
    }

}
