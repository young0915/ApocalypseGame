using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CDataManager : CSingleton<CDataManager>
{
    public readonly int m_nMinPosX = -22;
    public readonly int m_nMinPosZ = -210;
    public readonly string m_strStonevote = " : ";
    public const string m_strGameDataInfo = "GameDataInfo";
    public const string m_strCityScene = "Demo_City_Standard";
    public const string m_strBunkerScene = "Demo_Bunker";
    public const string m_strPlayerFolder = "Prefab/Player/CPlayer";

    private Dictionary<int, string> _dicDataText;
    private const string _strMissing = "Excel text not found";
   

    // 데이터 입력, 파일 이름과, 키값(id) 출력.
    public string GetDataValue(string strfileName, int nKey)
    {
        _dicDataText = new Dictionary<int, string>();
        TextAsset mytxtData = CResourceLoader.Load<TextAsset>("Texts/" + strfileName);

        string strTxt = mytxtData.text;
        if (strTxt != "" && strTxt != null)
        {
            string strDataAsJson = strTxt;
            CExcelData LoadedData = JsonUtility.FromJson<CExcelData>(strDataAsJson);

            for(int i =0; i< LoadedData.m_items.Length; i++)
            {
                if(!_dicDataText.ContainsKey(i))
                {
                    _dicDataText.Add(i, LoadedData.m_items[i].m_strvalue);
                }
            }
        }
#if LogError
        else
        {
            Debug.LogError("Cannot find file!");
        }
#endif

        string strResult = _strMissing;
        if(_dicDataText.ContainsKey(nKey - 1))
        {
            strResult = _dicDataText[nKey -1 ].Replace("\\n", "\n");
        }
        return strResult;
    }
}
