using UnityEngine;
using System.Collections;
using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;

public class CUIStoreScrollView : OSA<CStoreModelParams, CStoreViewHolder>
{
    [SerializeField] private CSOItem ins_cSOItem;
    #region OSA implementation

    protected override CStoreViewHolder CreateViewsHolder(int itemIndex)
    {
        var instance = new CStoreViewHolder();
        instance.Init(_Params.ItemPrefab, _Params.Content, itemIndex);

        return instance;
    }

    protected override void UpdateViewsHolder(CStoreViewHolder newOrRecycled)
    {
        newOrRecycled.UpdeateView(_Params.Data[newOrRecycled.ItemIndex]);
    }

    #endregion

    public void SetMakeDataModel(EmItemType eItemType)
    {
        // 데이터 클리어.
        _Params.Data.Clear();

        switch(eItemType)
        {
            case EmItemType.Medical:
                StartCoroutine(CorMedicalInstall());

                break;

            case EmItemType.Weapon:
                StartCoroutine(CorWeaponInstall());

                break;

        }
    }

    private IEnumerator CorMedicalInstall()
    {
        yield return new WaitForSeconds(.5f);

        for(int i =18; i<=19; i++)
        {
            CStoreModel model = new CStoreModel((ins_cSOItem.m_listItem[i].m_nId));

            _Params.Data.Add(model);
        }

        ResetItems(_Params.Data.Count);
    }

    private IEnumerator CorWeaponInstall()
    {
        yield return new WaitForSeconds(.5f);

        for(int i =3; i<18;i++)
        {
            CStoreModel model = new CStoreModel(ins_cSOItem.m_listItem[i].m_nId);
            _Params.Data.Add(model);
        }

        ResetItems(_Params.Data.Count);
    }

}

public class CStoreModel
{
    public EmCellState m_eCellState = EmCellState.NotComplete;
    public int m_nItemId;

    public CStoreModel(int nId)
    {
        this.m_nItemId = nId;
    }
}


[System.Serializable]
public class CStoreModelParams : BaseParamsWithPrefabAndData<CStoreModel> { }

public class CStoreViewHolder : BaseItemViewsHolder
{
    private CUIStoreCell _cUIStoreCell = null;

    public override void CollectViews()
    {
        base.CollectViews();
        _cUIStoreCell = root.GetComponent<CUIStoreCell>();
    }

    public void UpdeateView(CStoreModel cModel)
    {
        _cUIStoreCell.SetData(cModel);
    }

}