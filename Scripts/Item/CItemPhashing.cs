using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

public class CItemPhashing : MonoBehaviour
{
    [SerializeField] private CSOItem _cSOItem;

    private Dictionary<int, CItemInfo> _dicItem = new Dictionary<int, CItemInfo>();
   // private CItemInfo _cItem;

    private const string _strItemImagePath = "Texture/Item/Item";
    void Start()
    {
        string Path = "Texts/ItemData";
        var Json = Resources.Load<TextAsset>(Path).text;
        var ArrItem = JsonConvert.DeserializeObject<CItemInfo[]>(Json);

        foreach (var data in ArrItem)
        {
            _dicItem.Add(data.m_nId, data);
            _cSOItem.m_listItem[data.m_nId] = new CItemInfo(
                data.m_nId,
                data.m_eItemType,
                CResourceLoader.Load<Sprite>(_strItemImagePath+data.m_nId),
                data.m_strItemName,
                data.m_strItemDescript,
                data.m_nMoney,
                data.m_nHp,
                data.m_fSpeed,
                data.m_nAttack,
                data.m_nDefense,
                data.m_nRecovery,
                data.m_nCriticalHit,
                data.m_nIron,
                data.m_nTape,
                data.m_nWirecutter
                );
        }

    }
}
