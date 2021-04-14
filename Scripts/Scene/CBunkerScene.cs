using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBunkerScene : CSeneObject 
{
    [SerializeField] private CDoorAction ins_cDoorAction = null;

    protected override void OnSceneAwake()
    {
        base.OnSceneAwake();
        StartCoroutine(CUIManager.Inst.CorITweenFade());

        ins_cDoorAction.InstallBunker();

        CObjectPoolManager.Inst.GetObjcectActive(false);
        CDialogManager.Inst.InstallDialog();

       CUIManager.Inst.m_cUIPlayerMain.IsOpenMain(false, true);
        if(CUIManager.Inst.m_cUIQuest != null)
        {
            CUIManager.Inst.m_cUIQuest.OnRemoveQuest(1);                                                     // Quest1 획득.
        }
    }

    protected override void OnSceneUpdate()
    {
        base.OnSceneUpdate();
       
    }



}
