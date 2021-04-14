using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CUIIntro : CBaseUI
{
    [SerializeField] private List<Image> ins_Imglist = new List<Image>();
    [SerializeField] private TextMeshProUGUI ins_txtIntroContent = null;
    [SerializeField] private Button ins_btnNextPage = null;
   private int nPageNum = 0;
    private float _fTypingSpeed = 0.1f;

    private bool _bClickLock = false;
    public void Initialization()
    {
        ins_Imglist[0].gameObject.SetActive(true);
        CorTextOutput(5);
        StartCoroutine(CorTextOutput(5));
    }

    // 텍스트 넘기는 버튼.
    public void OnClickNextPage()
    {
        if (_bClickLock) return;
        _bClickLock = true;

        nPageNum++;
        for(int i =6; i<10; i++)
        {
            if(nPageNum == (i-5))
            {
                StartCoroutine(CorTextOutput(i));
            }
        }
        ins_Imglist[nPageNum].gameObject.SetActive(true);
        _bClickLock = false;
    }

    // 스킵 버튼.
    public void OnClickSkip()
    {
        if (_bClickLock) return;
        _bClickLock = true;

        ins_btnNextPage.gameObject.SetActive(false);
        StartCoroutine(CorGameStart());

        _bClickLock = false;
    }

    private IEnumerator CorTextOutput(int nKey)
    {
        ins_txtIntroContent.text = string.Empty;
        foreach (char cletter in CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, nKey).ToCharArray())
        {
            ins_txtIntroContent.text += cletter;
            yield return new WaitForSeconds(_fTypingSpeed);
        }

        if (nKey == 9)
        {
            ins_btnNextPage.gameObject.SetActive(false);
            StartCoroutine(CorGameStart());
        }
    }
   

    // 인트로가 끝나면 바로 게임 시작.
    private IEnumerator CorGameStart()
    {
        CSoundManager.Inst.SetSoundPause(0);
        yield return new WaitForSeconds(3.0f);
        CWheatherManager.Inst.gameObject.SetActive(true);
        yield return StartCoroutine(CUIManager.Inst.CorITweenFade());
        yield return StartCoroutine(CUIManager.Inst.CorPlayerMain());
        yield return StartCoroutine(CUIManager.Inst.CorPlayerAction(EmActionType.Go));
        CSoundManager.Inst.SetSoundPlay(1, 0.4f, true);

        Close(false);
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
