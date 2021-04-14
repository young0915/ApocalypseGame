using TMPro;
using UnityEngine;

public class CUIQuestCell : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ins_txtContent;

    private int _nId;
    private string _strQuestContent = string.Empty;

    public void SetData(CQuestModel cModel)
    {
        _nId = cModel.m_nId;

        _strQuestContent = CDBManager.Inst.m_listQuestInfo[_nId].m_strContent;
        ins_txtContent.text = _strQuestContent.ToString();

    }
}
