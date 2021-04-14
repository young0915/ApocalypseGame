using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

public class CNpcInfoPhashing : MonoBehaviour
{

    [SerializeField] private CSONpcData ins_cSONpcInfo;

    private Dictionary<int, CNpcInfo> _dicNpcInfo = new Dictionary<int, CNpcInfo>();
    private CNpcInfo _cNpcData;


    void Start()
    {
        string Path = "Texts/NpcData";
        var Json = Resources.Load<TextAsset>(Path).text;
        var ArrNpc = JsonConvert.DeserializeObject<CNpcInfo[]>(Json);

        foreach (var data in ArrNpc)
        {
            _dicNpcInfo.Add(data.m_nId, data);
            ins_cSONpcInfo.m_listNpcData[data.m_nId] = new CNpcInfo(
                data.m_nId,
                data.m_eNpcType,
                data.m_strName,
                data.m_strDescript,
                data.m_fSpeed,
                data.m_strAniAction
                );
        }
    }

}
