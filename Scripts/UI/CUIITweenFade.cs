using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class CUIITweenFade : CBaseUI
{
    [SerializeField] private Image ins_Img;

    public void Initialization()
    {
        StartCoroutine(CorAlpha());
    }

    private IEnumerator CorAlpha()
    {
        SetITweenValueTo(0.0f, 1.0f, 1f);
        SetITweenValueTo(1.0f, 0.0f, 1f);
        yield return new WaitForSeconds(1.0f);
        Close(false);
    }

    public void SetITweenValueTo(float fFrom, float fTo, float fTime)
    {
        Hashtable hash = new Hashtable(){
             {"from", fFrom},
             {"to", fTo},
             {"time", fTime},
             {"speed", 1f},
             {"delay", 0f},
             {"easeType",iTween.EaseType.linear},
             {"loopType",iTween.LoopType.none},
             {"onupdate", "SetImageColor"},
             {"onupdatetarget", gameObject},
         };
        iTween.ValueTo(gameObject, hash);
    }


    void SetImageColor(float alpha)
    {
        ins_Img.color = new Color(ins_Img.color.r, ins_Img.color.g, ins_Img.color.b, alpha);

    }


    public override void Open(Action<EmClickState> callBack)
    {
        base.Open(callBack);
    }

    public override void Close(bool bDestroy = true)
    {
        Destroy(gameObject);
    }
}
