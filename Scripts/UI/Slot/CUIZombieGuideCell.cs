using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CUIZombieGuideCell : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI ins_txtWaring;

    [Space]
    [SerializeField] private TextMeshProUGUI ins_txtTitle;
    [SerializeField] private TextMeshProUGUI ins_txtZombieLevel;
    [SerializeField] private TextMeshProUGUI ins_txtZombieDescript;

    [Space]
    [SerializeField] private Image ins_ImgZombie;
    [SerializeField] private Image ins_ImgIconlevel;


    private EmZombieType _eZombieType;
    private int _nId = 0;
    private string _strLevel = string.Empty;
    private string _strDescript = string.Empty;

    private const string _strZombieImg = "Texture/Zombie/Zombie";


    private float _R = 0.0f;
    private float _G = 0.0f;
    private float _B = 0.0f;

    public void SetData(CUIZombieGideModel cModel)
    {
        #region [code] CUIZombieGuideCell of Variable.
        SetTextInfo();

        this._eZombieType = cModel.m_eZombieType;
        this._nId = cModel.m_nId;

        _R = 0.0f;
        _G = 0.0f;
        _B = 0.0f;

    
        #endregion


        switch (_eZombieType)
        {
            case EmZombieType.Easy:
                _R = 0.0f;
                _G = 0.0f;
                _B = 1.0f;

                break;

            case EmZombieType.Normal:
                _R = 0.0f;
                _G = 1.0f;
                _B = 0.0f;


                break;

            case EmZombieType.Bad:
                _R = 1.0f;
                _G = 0.5f;
                _B = 0.0f;

                break;

            case EmZombieType.Worst:
                _R = 1.0f;
                _G = 0.0f;
                _B = 0.0f;

                break;


        }
        _strLevel = CZombieDataManager.Inst.m_cZombielist[_nId].m_strLevel.ToString();
        _strDescript = CZombieDataManager.Inst.m_cZombielist[_nId].m_strDescript.ToString();

        ins_txtZombieLevel.text = _strLevel;
        ins_txtZombieDescript.text = _strDescript;

        ins_ImgIconlevel.sprite = CResourceLoader.Load<Sprite>(_strZombieImg + CZombieDataManager.Inst.m_cZombielist[_nId].m_nId.ToString());

        ins_ImgIconlevel.color = new Color(_R, _G, _B);
    }

    private void SetTextInfo()
    {
        ins_txtTitle.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 31).ToString();
        ins_txtWaring.text = CDataManager.Inst.GetDataValue(CDataManager.m_strGameDataInfo, 32).ToString();

    }
}
