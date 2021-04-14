using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CUIPlayerAction : CBaseUI
{
    [SerializeField] private List<Image> ins_ImgActionlist = new List<Image>();

    private float _fCoolTime = 10.0f;
    private EmActionType _eAction;
    public EmActionType m_eAction { get { return _eAction; } }

    public void SetAction(EmActionType eAction)
    {
        this._eAction = eAction;

        int nImg = 0;
        switch (_eAction)
        {
            case EmActionType.Go:
                nImg = 0;
                break;

            case EmActionType.Complete:
                nImg = 1;
                break;

            case EmActionType.Victory:
                nImg = 2;

                break;

            case EmActionType.GameOver:
                nImg = 3;

                break;

            case EmActionType.LevelUp:
                nImg = 4;

                break;

            case EmActionType.None:
                Close(false);
                break;

        }
        ins_ImgActionlist[nImg].gameObject.SetActive(true);
        StartCoroutine(CorActionCoolTime(nImg, eAction));

    }

    private IEnumerator CorActionCoolTime(int nImg, EmActionType eAction)
    {
        int nTime = 0;

        if (eAction == EmActionType.GameOver)
        {
            yield return new WaitForSeconds(1.0f);
        }

        nTime = 20;
        ins_ImgActionlist[nImg].fillAmount = 1;
        while (ins_ImgActionlist[nImg].fillAmount > 0)
        {
            ins_ImgActionlist[nImg].fillAmount -= nTime * Time.deltaTime / _fCoolTime;

            yield return null;
        }
        yield break;
    }


    public override void Open(Action<EmClickState> callBack)
    {
        base.Open(callBack);
    }

    public override void Close(bool bDestroy = true)
    {
        base.Close(bDestroy);
    }
}
