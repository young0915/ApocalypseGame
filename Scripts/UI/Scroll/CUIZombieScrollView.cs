using UnityEngine;
using System.Collections;
using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;

public class CUIZombieScrollView : OSA<CZombieParams, CZombieViewHolder>
{
    #region OSA impelment

    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(CorZombieGuideInstall());
    }

    protected override CZombieViewHolder CreateViewsHolder(int itemIndex)
    {
        var instance = new CZombieViewHolder();
        instance.Init(_Params.ItemPrefab, _Params.Content, itemIndex);
        return instance;
    }

    protected override void UpdateViewsHolder(CZombieViewHolder newOrRecycled)
    {
        newOrRecycled.UpdateView(_Params.Data[newOrRecycled.ItemIndex]);
    }

    #endregion


    private IEnumerator CorZombieGuideInstall()
    {
        yield return new WaitForSeconds(0.2f);

        for(int i = 0; i<4; i++)
        {
            CUIZombieGideModel model = new CUIZombieGideModel(
                CZombieDataManager.Inst.m_cZombielist[i].m_eZombieType
                ,CZombieDataManager.Inst.m_cZombielist[i].m_nId);

            _Params.Data.Add(model);
        }

        ResetItems(_Params.Data.Count);
    }

}


public class CUIZombieGideModel
{
    public EmZombieType m_eZombieType;
    public int m_nId=0; 

    public CUIZombieGideModel(EmZombieType eZombieType,int nId)
    {
        this.m_eZombieType = eZombieType;
        this.m_nId = nId;
    }

}

[System.Serializable]
public class CZombieParams : BaseParamsWithPrefabAndData<CUIZombieGideModel> { }

public class CZombieViewHolder : BaseItemViewsHolder
{
    private CUIZombieGuideCell _cUIZombieGuideCell;

    public override void CollectViews()
    {
        base.CollectViews();
        _cUIZombieGuideCell = root.GetComponent<CUIZombieGuideCell>();
    }

    public void UpdateView(CUIZombieGideModel model)
    {
        _cUIZombieGuideCell.SetData(model);
    }


}