using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;


public class CDialogManager : CSingleton<CDialogManager>
{
    [SerializeField] private TextAsset ins_textDialog;

    private Dictionary<int, CDialogInfo> _dicDialog = new Dictionary<int, CDialogInfo>();
    public Dictionary<int, CDialogInfo> m_dicDialog { get { return _dicDialog; } }

    private CDialogInfo _cDialog;


    public void InstallDialog()
    {
        var ArrDialog = JsonConvert.DeserializeObject<CDialogInfo[]>(ins_textDialog.text);
        foreach (var data in ArrDialog)
        {
            _dicDialog.Add(data.m_nId, data);
        }

    }

    // 출력할 때 사용해야할 함수.
    public CDialogInfo GetDialog(int num)
    {
        return _dicDialog[num];
    }

}

public class CDialogInfo
{
    public int m_nId;
    public string m_strNpcName;
    public EmDialogType m_eDialogType;
    public string m_strContent;
    public bool m_bIsNextContent;

    public CDialogInfo(int nId, string strNpcName, EmDialogType eDialogType, string strContent, bool bIsNextContent)
    {
        this.m_nId = nId;
        this.m_strNpcName = strNpcName;
        this.m_eDialogType = eDialogType;
        this.m_strContent = strContent;
        this.m_bIsNextContent = bIsNextContent;
    }
}
