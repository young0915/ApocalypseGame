using UnityEngine;

public class CDebugLog : MonoBehaviour
{
    public enum ErrorID
    {
        Log,
        LogError,
        LogWarning
    }

    public static void Log(string strLog, ErrorID emErrorID = ErrorID.Log)
    {
#if LogError
        switch (emErrorID)
        {
            case ErrorID.Log:
                Debug.Log(strLog);
                break;
            case ErrorID.LogError:
                Debug.LogError(strLog);
                break;
            case ErrorID.LogWarning:
                Debug.LogWarning(strLog);
                break;
            default:
                break;
        }
#endif
    }

}
