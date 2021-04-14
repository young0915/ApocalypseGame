using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

// 추후 에셋번들 적용시 편하게 갈아타기 위한 매핑 클래스
public class CResourceLoader : MonoBehaviour
{
    private static Dictionary<string, Object> M_dicCache = new Dictionary<string, Object>();

    public delegate void AsyncCallBack<T>(T param);

    public static T Load<T>(string assetPath) where T : Object
    {
        if (string.IsNullOrEmpty(assetPath))
            return null;

        T res = default(T);

        Object assetObject;

        if (M_dicCache.TryGetValue(assetPath, out assetObject))
        {
            res = assetObject as T;

            if (res != null)
            {
                return res;
            }
            else if (assetObject.GetType() == typeof(GameObject))
            {
                return (assetObject as GameObject).GetComponent<T>();
            }
        }

        res = Resources.Load<T>(assetPath);

        if (res != null)
        {
            M_dicCache[assetPath] = res;
        }

        return res;
    }


    public static IEnumerator LoadAsync<T>(string assetPath, AsyncCallBack<T> callback) where T : Object
    {
        if (string.IsNullOrEmpty(assetPath))
        {
            Debug.LogError("assetPath is null or empty");
            callback?.Invoke(null);

            yield break;
        }

        Object assetObject;

        if (M_dicCache.TryGetValue(assetPath, out assetObject))
        {
            callback?.Invoke(assetObject as T);
            yield break;
        }

        T res = default(T);

        ResourceRequest req = Resources.LoadAsync<T>(assetPath);

        while (req.isDone == false)
        {
            yield return null;
        }

        Object loadedObj = req.asset;

        if (loadedObj != null)
        {
            res = (T)loadedObj;

            if (res == null)
            {
                Debug.LogFormat("(T)loadedObj is null: {0}", loadedObj.name);

                yield break;
            }
        }
        else
        {
            Debug.LogFormat("loadedObj is null: {0}", assetPath);

            yield break;
        }

        M_dicCache[assetPath] = res;
        callback?.Invoke(res);
    }

    public static IEnumerator LoadSceneAsync(string scenePath, string sceneName, bool isAdditive, System.Action callback)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("assetPath is null or empty");
            callback?.Invoke();

            yield break;
        }

        var loadSceneMode = isAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);

        while (operation != null && operation.progress < 1.0f)
            yield return null;

        callback?.Invoke();
    }

    public static void ClearCache()
    {
        M_dicCache.Clear();
    }

    
    /// </summary>
    /// <param name="callBack"></param>
    /// <returns></returns>
    public IEnumerator CorRemoveUnUsedAsset(System.Action callBack = null)
    {
        System.GC.Collect();

        AsyncOperation asynResourceUnloadTask = Resources.UnloadUnusedAssets();
        while (!asynResourceUnloadTask.isDone)
        {
            yield return null;
        }
        callBack?.Invoke();
    }

  
}
