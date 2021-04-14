using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;

public class CUIQuestScrollView : OSA<CQuestModelParams, CQuestViewHolder>
{

    #region OSA Implementation

    protected override void Awake()
    {
        base.Awake();
        SetMakeDataMode();
    }

    protected override CQuestViewHolder CreateViewsHolder(int itemIndex)
    {
        var instance = new CQuestViewHolder();
        instance.Init(_Params.ItemPrefab, _Params.Content, itemIndex);
        return instance;
    }

    protected override void UpdateViewsHolder(CQuestViewHolder newOrRecycled)
    {
        newOrRecycled.UpdateView(_Params.Data[newOrRecycled.ItemIndex]);
 
    }

    #endregion

     // 퀘스트 추가했을 때 사용하는 함수.
    public void AddQuestAt(CQuestModel Items)
    {
        //_Params.Data.InsertRange(nidx, Items);
    //    _Params.Data.Clear();

        _Params.Data.Add(Items);
        ResetItems(_Params.Data.Count);
    }

    // 퀘스트 완료시 셀 제거하는 함수.
    public void RemoveQuestFrom(int nIdx, int count)
    {
        _Params.Data.RemoveAt(nIdx);
        RemoveItems(nIdx, count);
   
    }
  
    public void SetMakeDataMode()
    {
        _Params.Data.Clear();

        StartCoroutine(CorQuestInstall());
    }

    private IEnumerator CorQuestInstall()
    {
        yield return new WaitForSeconds(.2f);

        //for(int i =0; i<CDBManager.Inst.m_listQuestInfo.Count; i++)
        //{
        //    CQuestModel model = new CQuestModel(CDBManager.Inst.m_listQuestInfo[i].m_nId);
        //    _Params.Data.Add(model);
        //}
        //ResetItems(_Params.Data.Count);
    }

}

public class CQuestModel
{
    public EmCellState m_eCellSate = EmCellState.NotComplete;          // 중복값을 알리기 위한 타입.
    public int m_nId;                                                                   // 퀘스트 아이디 넘버.

    public CQuestModel(int nId)
    {
        this.m_nId = nId;
    }

}

[System.Serializable]
public class CQuestModelParams : BaseParamsWithPrefabAndData<CQuestModel> { }

public class CQuestViewHolder :BaseItemViewsHolder
{
    private CUIQuestCell _cUIQuestCell;

    public override void CollectViews()
    {
        base.CollectViews();
        _cUIQuestCell = root.GetComponent<CUIQuestCell>();
    }

    public void UpdateView(CQuestModel cModel)
    {
        _cUIQuestCell.SetData(cModel);
    }
}