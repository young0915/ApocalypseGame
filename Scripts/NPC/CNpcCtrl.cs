using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CNpcCtrl : MonoBehaviour
{
    [SerializeField] private CSONpcData ins_cSONpcInfo;
    public CSONpcData m_cSONpcInfo { get { return ins_cSONpcInfo; } }
    [SerializeField] private int ins_nNpcNum;
    public int m_nNpcNum { get { return ins_nNpcNum; } }
    [SerializeField] protected Animator ins_AniNpc;
    [SerializeField] private bool _bIsQuestTalk = false;

    private CNpcInfo _cNpcData;
    private EventTrigger _EventTrigger;

    private bool _bIsAni = false;
    private const string _strAni = "Idle";
    private const float _fPI = 180.0f;
    private const float _fGrayRotY = -85.0f;
    private const float _fYouJinRotY = -152.0f;
    private const float _fHaDiRotY = -144.4f;
    private const float _fOlivia = 136.0f;


    private void Start()
    {
        _EventTrigger = GetComponent<EventTrigger>();

        _cNpcData = new CNpcInfo(
               ins_cSONpcInfo.m_listNpcData[ins_nNpcNum].m_nId,
                    ins_cSONpcInfo.m_listNpcData[ins_nNpcNum].m_eNpcType,
                    ins_cSONpcInfo.m_listNpcData[ins_nNpcNum].m_strName,
                    ins_cSONpcInfo.m_listNpcData[ins_nNpcNum].m_strDescript,
                    ins_cSONpcInfo.m_listNpcData[ins_nNpcNum].m_fSpeed,
                    ins_cSONpcInfo.m_listNpcData[ins_nNpcNum].m_strAniAction
            );


        EventTrigger.Entry MouseClick = new EventTrigger.Entry();
        MouseClick.eventID = EventTriggerType.PointerClick;
        MouseClick.callback.AddListener((data) => OnClickNpcCharacter());

        _EventTrigger.triggers.Add(MouseClick);
    }

    private void Update()
    {
        string strAni = string.Empty;
        if (!_bIsAni)
        {
            strAni = ins_cSONpcInfo.m_listNpcData[ins_nNpcNum].m_strAniAction;
        }
        else
        {
            strAni = _strAni;
        }
        ins_AniNpc.Play(strAni);

        if (CUIManager.Inst.m_cUIDialogue == null)
        {
            _bIsAni = false;
            float fRotY = 0.0f;
            switch (ins_nNpcNum)
            {
                case 0:
                    fRotY = _fYouJinRotY;
                    break;
                case 3:
                    fRotY = _fOlivia;
                    break;
                case 5:
                    fRotY = _fHaDiRotY;
                    break;
                case 7:
                    fRotY = _fGrayRotY;
                    break;
                default:
                    return;
            }
            transform.rotation = Quaternion.Euler(0, fRotY, 0);
        }
    }


    public void OnClickNpcCharacter()
    {

        StartCoroutine(CUIManager.Inst.CorDialogue());

        // 대화 시도가 가능한 Npc  일경우.
        if (_bIsQuestTalk)
        {
            if (ins_cSONpcInfo.m_listNpcData[ins_nNpcNum].m_strName == "유진")
            {
                StartCoroutine(CorDialog(0,7));
            }
            else if(ins_cSONpcInfo.m_listNpcData[ins_nNpcNum].m_strName == "하디")
            {
                StartCoroutine(CorDialog(9, 14));
            }
                return;
        }


        CUIManager.Inst.m_cUIDialogue.SetNpcDialog(ins_cSONpcInfo.m_listNpcData[ins_nNpcNum].m_strName,
           ins_cSONpcInfo.m_listNpcData[ins_nNpcNum].m_strDescript);

        if (ins_cSONpcInfo.m_listNpcData[ins_nNpcNum].m_nId == 6)
        {
            CUIManager.Inst.m_cUIQuest.OnAddQuest(4);
        }

        if (ins_cSONpcInfo.m_listNpcData[ins_nNpcNum].m_eNpcType != EmNpcType.None)
        {
            _bIsAni = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);

            if (ins_cSONpcInfo.m_listNpcData[ins_nNpcNum].m_eNpcType == EmNpcType.Store)
            {
                CCameraManager.Inst.SetNpcInteraction(EmNpcType.Store);

                CUIManager.Inst.m_cUIDialogue.SetBtnActive(0);
            }

            if (ins_cSONpcInfo.m_listNpcData[ins_nNpcNum].m_eNpcType == EmNpcType.Quest)
            {
                CUIManager.Inst.m_cUIDialogue.SetBtnActive(1);
            }

            if (ins_cSONpcInfo.m_listNpcData[ins_nNpcNum].m_nId == 0)
            {
                Debug.Log(ins_cSONpcInfo.m_listNpcData[ins_nNpcNum].m_strName);
            }
        }
        if(ins_nNpcNum == ins_cSONpcInfo.m_listNpcData[ins_nNpcNum].m_nId)
        {
            ins_cSONpcInfo.m_listNpcData[ins_nNpcNum].m_bIsBow = true;
        }

   
    }


    private IEnumerator CorDialog(int nFrontNum =0, int nBackNum =0)
    {
        string strName = string.Empty;
        string strContent = string.Empty;

        // 대화 생성.
        CUIManager.Inst.m_cUIDialogue.SetBtnActive(1);
        CUIManager.Inst.m_cUIDialogue.GetNumDialog(nFrontNum, nBackNum);

        strName = CDialogManager.Inst.GetDialog(nFrontNum).m_strNpcName.ToString();
        strContent = CDialogManager.Inst.GetDialog(nFrontNum).m_strContent.ToString();

        CUIManager.Inst.m_cUIDialogue.SetNpcDialog(strName, strContent);

        _bIsQuestTalk = false;

        yield return null;
    }

}
