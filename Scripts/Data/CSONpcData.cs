using UnityEngine;
using System.Collections.Generic;

//[CreateAssetMenu(fileName = "CSONpcData", menuName = "ScriptableObject", order = int.MaxValue)]
public class CSONpcData : ScriptableObject
{
    public List<CNpcInfo> m_listNpcData = new List<CNpcInfo>();
}

[System.Serializable]
public class CNpcInfo
{
    public int m_nId;
    public EmNpcType m_eNpcType;
    public string m_strName;
    public string m_strDescript;
    public float m_fSpeed;
    public string m_strAniAction;
    public bool m_bIsBow = false;

    public CNpcInfo(int nId, EmNpcType eNpcType, string strName, string strDescript, float fSpeed, string strAniAction)
    {
        this.m_nId = nId;
        this.m_eNpcType = eNpcType;
        this.m_strName = strName;
        this.m_strDescript = strDescript;
        this.m_fSpeed = fSpeed;
        this.m_strAniAction = strAniAction;

    }
}