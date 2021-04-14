using UnityEngine;

[System.Serializable]
public class CItemInfo 
{
    public int m_nId;
    public EmItemType m_eItemType;
    public string m_strItemName;                    // 아이템 이름.
    public string m_strItemDescript;                 // 아이템 설명.
    public Sprite m_ItemSprite;                       // 아이팀 이미지(인벤토리에서 보여줄 것).

    public int m_nMoney;
    public int m_nHp;
    public float m_fSpeed;
    public int m_nAttack;                            // 공격력.
    public int m_nDefense;                         // 방어력.
    public int m_nRecovery;                        // 회복력.
    public int m_nCriticalHit;                      // 크리티컬.

    //재료들.
    public int m_nIron;
    public int m_nTape;
    public int m_nWirecutter;

    public CItemInfo(int m_nId, EmItemType eItemType, Sprite ItemSprite, string strItemName, string strItemDescript, int nMoney, int nHp, float fSpeed,
        int nAttack, int nDefense, int nRecovery, int nCriticalHit, int nIron, int nTape, int nWirecutter)
    {
        this.m_nId = m_nId;
        this.m_eItemType = eItemType;
        this.m_ItemSprite = ItemSprite;
        this.m_strItemName = strItemName;
        this.m_strItemDescript = strItemDescript;
        this.m_nMoney = nMoney;
        this.m_nHp = nHp;
        this.m_fSpeed = fSpeed;
        this.m_nAttack = nAttack;
        this.m_nDefense = nDefense;
        this.m_nRecovery = nRecovery;
        this.m_nCriticalHit = nCriticalHit;
        this.m_nIron = nIron;
        this.m_nTape = nTape;
        this.m_nWirecutter = nWirecutter;
    }

}
