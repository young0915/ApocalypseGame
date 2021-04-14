using UnityEngine;
using UnityEngine.SceneManagement;


public class CSceneManager : CSingleton<CSceneManager>
{

    public CSeneObject m_CurScene { get; set; } = null;

    public string m_strCurSceneName { get; private set; } = string.Empty;

    public void OnSceneMovement(string strSceneName)
    {
        m_strCurSceneName = strSceneName;

        SceneManager.LoadScene(strSceneName);
    }
   
}

