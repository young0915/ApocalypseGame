using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CUIPreferences : CBaseUI
{
    [SerializeField] private TextMeshProUGUI ins_txtTitle;
    [SerializeField] private TextMeshProUGUI ins_txtSound;
    [SerializeField] private TextMeshProUGUI ins_txtBtnCheck;

    [SerializeField] private Slider ins_sliderSound;

    public void Initialization()
    {
        CSoundManager.Inst.m_AudioSource.volume = ins_sliderSound.value;

        SetTextInfo();
        SetSound();
    }

    private void SetTextInfo()
    {
        ins_txtTitle.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 37);
        ins_txtSound.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 39);
        ins_txtBtnCheck.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 38);
    }

    public void SetSound()
    {
        CSoundManager.Inst.m_AudioSource.volume = ins_sliderSound.value;
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
