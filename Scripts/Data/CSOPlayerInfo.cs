using UnityEngine;
[CreateAssetMenu(fileName = "CSOPlayer", menuName = "ScriptableObject", order = int.MaxValue)]
public class CSOPlayerInfo : ScriptableObject
{
    public EmProfessional m_eProfessional;
    public EmMouseState m_eMouseState;
    public EmWeapon m_eWeapon;
    public string m_strName;
    public int m_nMoney;
    public int m_nHp;
    public int m_nLv;
    public float m_fCoolTime;
    public float m_fSpeed;
    public int m_nAttack;                            // 공격력.
    public int m_nDefense;                         // 방어력.
    public int m_nRecovery;                        // 회복력.
    public int m_nCriticalHit;                      // 크리티컬.

    public  void SetCSOPlayerInfo(EmProfessional eProfessional, EmWeapon eWeapon,int nHp, int nLv,float fCoolTime,float fSpeed, int nAttack, int nDefense, int nRecovery, int nCriticalHit)
    {
        this.m_eProfessional = eProfessional;
        this.m_eWeapon = eWeapon;
        this.m_nHp = nHp;
        this.m_nLv = nLv;
        this.m_fCoolTime = fCoolTime;
        this.m_fSpeed = fSpeed;
        this.m_nAttack = nAttack;
        this.m_nDefense = nDefense;
        this.m_nRecovery = nRecovery;
        this.m_nCriticalHit = nCriticalHit;
    }
}
