using UnityEngine;
using System.Collections.Generic;

//[CreateAssetMenu(fileName = "CSOItem", menuName = "ScriptableObject", order = int.MaxValue)]
public class CSOItem : ScriptableObject
{
    public List<CItemInfo> m_listItem = new List<CItemInfo>();
}
