using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;


public class CZombieDataManager : CSingleton<CZombieDataManager>
{
    [SerializeField] private List<CZombieInfo> _listZombie = new List<CZombieInfo>();
     public List<CZombieInfo> m_cZombielist { get { return _listZombie; } }

    private readonly string _strDBNamePath = "URI=file:ZombieData.db";
    private readonly string _strDBCmdText = "SELECT * FRom ZombieData;";

   
    public void OnCreateDB()
    {
        _listZombie.Clear();
        using (var Connection = new SqliteConnection(_strDBNamePath))
        {
            Connection.Open();

            using (var cmd = Connection.CreateCommand())
            {
                cmd.CommandText = _strDBCmdText;

                using (IDataReader reader = cmd.ExecuteReader())
                {

                    while(reader.Read())
                    {

                        _listZombie.Add(new CZombieInfo(
                            (EmZombieType)reader.GetInt32(1),
                            reader.GetInt32(0),
                            reader.GetString(2),
                             reader.GetString(3),
                             reader.GetInt32(4),
                             reader.GetInt32(5),
                             reader.GetInt32(6),
                             reader.GetInt32(7)));
                    }
                    reader.Close();
                }
                Connection.Close();
            }
        }


    }
}
