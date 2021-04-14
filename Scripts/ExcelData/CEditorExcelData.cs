using System.IO;
using UnityEditor;
using System.Text;
using UnityEngine;

#if UNITY_EDITOR
public class CEditorExcelData : EditorWindow
{
    public CExcelData m_cExcelData;

    [MenuItem("Window/Excel Data Text Editor")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(CEditorExcelData)).Show();
    }

    private Vector2 scrollPos = Vector2.zero;

    private void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, true, true, GUILayout.Width(GetWindow(typeof(CEditorExcelData)).position.width), GUILayout.Height(GetWindow(typeof(CEditorExcelData)).position.height));
        if (m_cExcelData != null)
        {

            if (GUILayout.Button("Save data"))
            {
                SaveGameData();
            }
        }

        if (GUILayout.Button("Load data"))
        {
            LoadGameData();
        }

        if (GUILayout.Button("Load CSV File"))
        {
            LoadCSVFile();
        }
        EditorGUILayout.EndScrollView();
    }

    private void LoadGameData()
    {
        string filePath = EditorUtility.OpenFilePanel("Select Excel data file", Application.dataPath, "txt");

        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);

            m_cExcelData = JsonUtility.FromJson<CExcelData>(dataAsJson);
        }
    }

    private void SaveGameData()
    {
        string filePath = EditorUtility.SaveFilePanel("Save Excel data file", Application.dataPath, "", "txt");

        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = JsonUtility.ToJson(m_cExcelData);
            File.WriteAllText(filePath, dataAsJson);
        }
    }
    private void LoadCSVFile()
    {
        string filePath = EditorUtility.OpenFilePanel("Select Excel data file", Application.dataPath, "csv");

        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath, Encoding.UTF8);
            string[] stringBigList = dataAsJson.Split('\n');
            m_cExcelData = new CExcelData();
            m_cExcelData.m_items = new CExcelDataInfo[stringBigList.Length];
            for (var i = 1; i < stringBigList.Length; i++)
            {
                string[] stringList = stringBigList[i].Split(',');
                for (var j = 0; j < stringList.Length; j++)
                {
                    m_cExcelData.m_items[i - 1] = new CExcelDataInfo(stringList[0], stringList[1].ToString());
                }
            }
        }
    }
}
#endif
