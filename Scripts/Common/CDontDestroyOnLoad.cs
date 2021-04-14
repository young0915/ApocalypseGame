using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDontDestroyOnLoad : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
