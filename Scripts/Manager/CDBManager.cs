using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;

public class CDBManager : CSingleton<CDBManager>
{
    private List<CLevelInfo> _listLevelInfo = new List<CLevelInfo>();
    public List<CLevelInfo> m_listLevelInfo { get { return _listLevelInfo; } }

    private List<CQuestInfo> _listQuestInfo = new List<CQuestInfo>();
    public List<CQuestInfo> m_listQuestInfo { get { return _listQuestInfo; } }

    private readonly string _strDBLevelPath = "URI=file:LevelData.db";
    private readonly string _strDBQuestPath = "URI=file:QuestData.db";


    private readonly string _strDBLevelCmdText = "SELECT * FRom CLevelData;";
    private readonly string _strDBQuestCmdText = "SELECT * FRom QuestData;";
    private EmDBType _eDBType;

    public void OnCreateDB(EmDBType eDBType)
    {
        this._eDBType = eDBType;
        _listLevelInfo.Clear();
        _listQuestInfo.Clear();
        string strDBPath = string.Empty;
        string strCmdText = string.Empty;

       switch(eDBType)
        {
            case EmDBType.Level:
                strDBPath = _strDBLevelPath;
                strCmdText = _strDBLevelCmdText;

                break;

            case EmDBType.Quest:
                strDBPath = _strDBQuestPath;
                strCmdText = _strDBQuestCmdText;

                break;
        }


        using (var Connection = new SqliteConnection(strDBPath))
        {
            Connection.Open();

            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandText = strCmdText;

                using (IDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        switch (eDBType)
                        {
                            case EmDBType.Level:
                                _listLevelInfo.Add(new CLevelInfo(
                            reader.GetInt32(0),
                            reader.GetInt32(1),
                            reader.GetInt32(2),
                            reader.GetInt32(3),
                            reader.GetInt32(4)
                            ));
                           

                                break;

                            case EmDBType.Quest:
                                _listQuestInfo.Add(new CQuestInfo(
                                   reader.GetInt32(0),
                                    reader.GetString(1),
                                    reader.GetInt32(2),
                                    reader.GetInt32(3)
                                    ));
                           

                                break;
                        }

                    }
                    reader.Close();
                }
                Connection.Close();
            }
        }
    }


}


public class CLevelInfo
{
    public int m_nId;
    public int m_nLevel;
    public int m_nHp;
    public int m_nCoolTime;
    public int m_nSpeed;

    public CLevelInfo(int nId, int nLevel, int nHp, int nCoolTime, int nSpeed)
    {
        this.m_nId = nId;
        this.m_nLevel = nLevel;
        this.m_nHp = nHp;
        this.m_nCoolTime = nCoolTime;
        this.m_nSpeed = nSpeed;
    }
}

public class CQuestInfo
{
    public int m_nId;
    public string m_strContent;
    public int m_nExp;
    public int m_nMoney;

    public CQuestInfo(int nId, string strContent, int nExp, int nMoney)
    {
        this.m_nId = nId;
        this.m_strContent = strContent;
        this.m_nExp = nExp;
        this.m_nMoney = nMoney;
    }
}