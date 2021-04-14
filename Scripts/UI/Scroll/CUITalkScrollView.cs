using UnityEngine;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;


public class CUITalkScrollView : OSA<CTalkParams, CTalkViewHolder>
{
    [SerializeField] private TextAsset ins_textAsset;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override CTalkViewHolder CreateViewsHolder(int itemIndex)
    {
        var instance = new CTalkViewHolder();
        instance.Init(_Params.ItemPrefab, _Params.Content, itemIndex);

        return instance;
    }

    protected override void UpdateViewsHolder(CTalkViewHolder newOrRecycled)
    {
        newOrRecycled.UpdateView(_Params.Data[newOrRecycled.ItemIndex]);
    }

    public void AddChatAt(int num)
    {

        var ArrTalkModel = JsonConvert.DeserializeObject<CTalkModel[]>(ins_textAsset.text);

        CTalkModel model = new CTalkModel(num,
            ArrTalkModel[num].m_eDialogType, 
            ArrTalkModel[num].m_strName,
            ArrTalkModel[num].m_strContent);

        _Params.Data.Add(model);
        ResetItems(_Params.Data.Count);
    }

}

public class CTalkModel
{
    public int m_nId;
    public EmDialogType m_eDialogType;
    public string m_strName = string.Empty;
    public string m_strContent = string.Empty;

    public CTalkModel(int nId, EmDialogType eDialog, string strName, string strContent)
    {
        this.m_nId = nId;
        this.m_eDialogType = eDialog;
        this.m_strName = strName;
        this.m_strContent = strContent;
    }
}

[System.Serializable]
public class CTalkParams : BaseParamsWithPrefabAndData<CTalkModel> { }

public class CTalkViewHolder :BaseItemViewsHolder
{
    private CUITalkCell _cUITalkCell;

    public override void CollectViews()
    {
        base.CollectViews();
        _cUITalkCell = root.GetComponent<CUITalkCell>();
    }

    public void UpdateView(CTalkModel cModel)
    {
        _cUITalkCell.SetData(cModel);
    }
}