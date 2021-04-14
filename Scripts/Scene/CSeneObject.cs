using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CSeneObject : MonoBehaviour
{
    private void Awake()
    {
        OnSceneAwake();
    }

    private void Start()
    {
        OnSceneStart();
    }

    private void Update()
    {
        OnSceneUpdate();
    }

    protected virtual void OnSceneAwake()
    {
        CSceneManager.Inst.m_CurScene = this;
    }

    protected virtual void OnSceneStart()
    {

    }

    protected virtual void OnSceneUpdate()
    {

    }

}
