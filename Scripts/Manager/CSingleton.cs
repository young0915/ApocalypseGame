using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T In;

    public static T Inst
    {
        get
        {
            if (In == null)
            {
                In = FindObjectOfType<T>();
            }

            if (In == null)
            {
                GameObject obj;

                obj = new GameObject(typeof(T).Name);

                In = obj.AddComponent<T>();
            }

            return In;
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}