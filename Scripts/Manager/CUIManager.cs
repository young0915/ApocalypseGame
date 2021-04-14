using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CUIManager : CSingleton<CUIManager>
{
    [SerializeField]
    private RectTransform[] ins_rtraRoots = null;

    private readonly string _strDefaultPath = "Prefab/UI/";
    private List<CBaseUI> _listUI = new List<CBaseUI>();

    public CUINickNameInputField m_cUINickNameInputFiled;                                         // UI 입력창.
    public IEnumerator CorNickNameInputFiled(EmCanvasLayer eCanvas = EmCanvasLayer.Layer1, Action<EmClickState> callBack = null)
    {
        if (m_cUINickNameInputFiled == null)
        {
            m_cUINickNameInputFiled = MakeUI<CUINickNameInputField>("CUINickNameInputField", eCanvas);
        }

        m_cUINickNameInputFiled.Initialization();
        m_cUINickNameInputFiled.Open(callBack);
        yield return null;
    }

    public CUIWarning m_cUIWarning;
    public IEnumerator CorWarning(EmWarningType eWarningType = EmWarningType.NicKaNemeError,
        EmCanvasLayer eCanvas = EmCanvasLayer.Layer2, Action<EmClickState> callBack = null)
    {
        if (m_cUIWarning == null)
        {
            m_cUIWarning = MakeUI<CUIWarning>("CUIWarning", eCanvas);
        }

        m_cUIWarning.Initialization(eWarningType);
        m_cUIWarning.Open(callBack);
        yield return null;
    }
    //Initialization

    public CUICreateCharacter m_cUICreatCharacter;
    public IEnumerator CorCreateCharacter(EmCanvasLayer eCanvas = EmCanvasLayer.Overlay, Action<EmClickState> callBack = null)
    {
        if (m_cUICreatCharacter == null)
        {
            m_cUICreatCharacter = MakeUI<CUICreateCharacter>("CUICreateCharacter", eCanvas);
        }

        m_cUICreatCharacter.Initialization();
        m_cUICreatCharacter.Open(callBack);

        yield return null;
    }

    public CUIIntro m_cUIIntro;
    public IEnumerator CorIntro(EmCanvasLayer eCanvas = EmCanvasLayer.Overlay, Action<EmClickState> callBack = null)
    {
        if (m_cUIIntro == null)
        {
            m_cUIIntro = MakeUI<CUIIntro>("CUIIntro", eCanvas);
        }

        m_cUIIntro.Initialization();
        m_cUIIntro.Open(callBack);
        yield return null;
    }

    public CUIMain m_cUIMain;
    public IEnumerator CorMainWin(EmCanvasLayer eCanvas = EmCanvasLayer.Overlay, Action<EmClickState> callBack = null)
    {
        if (m_cUIMain == null)
        {
            m_cUIMain = MakeUI<CUIMain>("CUIMain", eCanvas);
        }

        m_cUIMain.Open(callBack);
        yield return null;
    }

    public CUIDialogue m_cUIDialogue;
    public IEnumerator CorDialogue(EmCanvasLayer eCanvas = EmCanvasLayer.Layer1, Action<EmClickState> callBack = null)
    {
        if (m_cUIDialogue == null)
        {
            m_cUIDialogue = MakeUI<CUIDialogue>("CUIDialogue", eCanvas);
        }
        
        m_cUIDialogue.Open(callBack);

        yield return null;
    }


    public CUIItemBox m_cUIItemBox;
    public IEnumerator CorItemBox(EmCanvasLayer eCanvas = EmCanvasLayer.Layer2, Action<EmClickState> callBack = null)
    {
        if(m_cUIItemBox == null)
        {
            m_cUIItemBox = MakeUI<CUIItemBox>("CUIItemBox", eCanvas);
        }

        m_cUIItemBox.Open(callBack);
        m_cUIItemBox.Initialization();
        yield return null;
    }


    public CUIPhone m_cUIPhone = null;
    public IEnumerator CorPhone(EmCanvasLayer eCanvas = EmCanvasLayer.Layer1, Action<EmClickState> callBack = null)
    {
        if(m_cUIPhone == null)
        {
            m_cUIPhone = MakeUI<CUIPhone>("CUIPhone", eCanvas);
        }

        m_cUIPhone.Open(callBack);
        m_cUIPhone.Initialization();
        yield return null;
    }


    public CUIStore m_cUIStore;
    public IEnumerator CorStore(EmCanvasLayer eCanvas  = EmCanvasLayer.Layer2, Action<EmClickState> callBack = null)
    {
        if(m_cUIStore == null)
        {
            m_cUIStore = MakeUI<CUIStore>("CUIStore", eCanvas);
        }

        m_cUIStore.Open(callBack);
        m_cUIStore.OnClickStoreTab(1);                          // 초기화.

        yield return null;
    }


    public CUIItemInfo m_cUIItemInfo;
    public IEnumerator CorItemInfo(Vector3 VPos, EmInfoType eInfoType = EmInfoType.Inventory, int nId =0, EmCanvasLayer ecanvas = EmCanvasLayer.Layer2, Action<EmClickState> callBack = null)
    {
        if(m_cUIItemInfo == null)
        {
            m_cUIItemInfo = MakeUI<CUIItemInfo>("CUIItemInfo", ecanvas);
        }

        m_cUIItemInfo.Open(callBack);
        m_cUIItemInfo.SetWinInfo(VPos, eInfoType, nId);
        yield return null;
    }


    public CUIQuest m_cUIQuest;
    public IEnumerator CorQuest(EmCanvasLayer eCanvas = EmCanvasLayer.Layer1, Action<EmClickState> callBack = null)
    {
        if(m_cUIQuest == null)
        {
            m_cUIQuest = MakeUI<CUIQuest>("CUIQuest", eCanvas);
        }

        m_cUIQuest.Open(callBack);

        yield return null;
    }


    public CUIPlayerAction m_cUIPlayerAction;
    public IEnumerator CorPlayerAction(EmActionType eAction =EmActionType.None, EmCanvasLayer eCanvas = EmCanvasLayer.Overlay, Action<EmClickState> callBack = null)
    {
        if(m_cUIPlayerAction == null)
        {
            m_cUIPlayerAction = MakeUI<CUIPlayerAction>("CUIPlayerAction", eCanvas);
        }

        m_cUIPlayerAction.Open(callBack);
        m_cUIPlayerAction.SetAction(eAction);

        yield return null;
    }

    public CUIPlayerMain m_cUIPlayerMain;
    public IEnumerator CorPlayerMain(EmCanvasLayer eCanvas = EmCanvasLayer.Layer1, Action<EmClickState> callBack = null)
    {
        if(m_cUIPlayerMain == null)
        {
            m_cUIPlayerMain = MakeUI<CUIPlayerMain>("CUIPlayerMain", eCanvas);
        }

        m_cUIPlayerMain.Open(callBack);
        m_cUIPlayerMain.Initialization();
        yield return null;
    }

    public CUIWeaponUpgrade m_cUIWeaponUpgrade;
    public IEnumerator CorWeaponUpgrade(int nItemId = 0, int nMoney =0,
        EmCanvasLayer eCanvas = EmCanvasLayer.Overlay, Action<EmClickState> callBack = null)
    {
        if(m_cUIWeaponUpgrade == null)
        {
            m_cUIWeaponUpgrade = MakeUI<CUIWeaponUpgrade>("CUIWeaponUpgrade", eCanvas);
        }

        m_cUIWeaponUpgrade.Open(callBack);
        m_cUIWeaponUpgrade.Initialization(nItemId, nMoney);
        yield return null;
    }

    public CUIPreferences m_cUIPreferences;
    public IEnumerator CorPreferences(EmCanvasLayer eCanvas = EmCanvasLayer.Overlay, Action< EmClickState > callBack = null)
    {
        if(m_cUIPreferences == null)
        {
            m_cUIPreferences = MakeUI<CUIPreferences>("CUIPreferences", eCanvas);
        }

        m_cUIPreferences.Open(callBack);
        m_cUIPreferences.Initialization();


        yield return null;
    }



    #region [code] ITween 함수들.

    public CUIITweenFade m_cUITweenFade;
    public IEnumerator CorITweenFade(EmCanvasLayer eCanvas = EmCanvasLayer.Overlay, Action<EmClickState> callBack = null)
    {
        if (m_cUITweenFade == null)
        {
            m_cUITweenFade = MakeUI<CUIITweenFade>("CUIITweenFade", eCanvas);
        }

        m_cUITweenFade.Initialization();
        m_cUITweenFade.Open(callBack);
        yield return null;
    }
    #endregion



    #region DefaultFunc

    private T MakeUI<T>(string strPrefabsName, EmCanvasLayer eCanvas, bool bAsync = false) where T : CBaseUI
    {
        string strPath = _strDefaultPath + strPrefabsName;
        T cPrefab = null;

        if (bAsync)
            CResourceLoader.LoadAsync<T>(strPath, (res) => cPrefab = res);
        else
            cPrefab = CResourceLoader.Load<T>(strPath);

        CDebugLog.Log(strPrefabsName + "프리팹 로더 실패");

        T instance = Instantiate<T>(cPrefab);
        instance.GetComponent<RectTransform>().SetParent(ins_rtraRoots[(int)eCanvas]);
        instance.transform.localScale = Vector3.one;
        InitRectTransform(instance.gameObject);

        return instance;
    }

    private void InitRectTransform(GameObject gameObject, bool useCanvasSize = true)
    {
        RectTransform ot = gameObject.GetComponent<RectTransform>();
        RectTransform rt = gameObject.transform.parent.GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        ot.anchorMin = new Vector2(0.5f, 0.5f);
        ot.anchorMax = new Vector2(0.5f, 0.5f);
        ot.pivot = new Vector2(0.5f, 0.5f);
        ot.localPosition = Vector3.zero;

        if (true == useCanvasSize)
            ot.sizeDelta = rt.sizeDelta;
    }

    public void AddUI(CBaseUI cBaseUI)
    {
        _listUI.Add(cBaseUI);
    }

    public void RemoveUI(CBaseUI cBaseUI)
    {
        _listUI.Remove(cBaseUI);
    }

    public void SecenChange()
    {
        for (int i = 0; i < _listUI.Count; i++)
        {
            _listUI[i].Close(true);
        }
        _listUI.Clear();
    }

    /// <summary>
    /// false 리턴시 인게임 입력 true 리턴시 인게임 입력 Block
    /// </summary>
    /// <returns></returns>
    public bool InputLock()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        //조건문을 지속적으로 추가한다.

        return false;
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && _listUI.Count > 0)
        {
            CBaseUI cCloseUI = _listUI[_listUI.Count - 1];
            cCloseUI.Close();
        }
    }

    #endregion
}
