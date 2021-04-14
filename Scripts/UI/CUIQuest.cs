using System;
using UnityEngine;

public class CUIQuest : CBaseUI
{
    [SerializeField] private CUIQuestScrollView ins_cUIQuestScrollView;

    private CQuestModel model;

    // 퀘스트 추가.
    public void OnAddQuest(int num)
    {
        model = new CQuestModel(CDBManager.Inst.m_listQuestInfo[num].m_nId);
        ins_cUIQuestScrollView.AddQuestAt(model);

    }

    // 퀘스트 삭제.
    public void OnRemoveQuest(int nId = 0)
    {
        if(model != null)
        {
            ins_cUIQuestScrollView.RemoveQuestFrom(0, 1);
            StartCoroutine(CUIManager.Inst.CorPlayerAction(EmActionType.Complete));

            CUIManager.Inst.m_cUIPlayerMain.SetExp(CDBManager.Inst.m_listQuestInfo[nId].m_nExp);
            CUIManager.Inst.m_cUIPhone.GetAddMoney(CDBManager.Inst.m_listQuestInfo[nId].m_nMoney);
        }

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
