[System.Serializable]
public class CExcelData
{
    public CExcelDataInfo[] m_items;
}

[System.Serializable]
public class CExcelDataInfo
{
    public CExcelDataInfo(string key, string value)
    {
        m_strkey = key;
        m_strvalue = value;
    }
    public string m_strkey;
    public string m_strvalue;
}