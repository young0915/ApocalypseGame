using UnityEngine;
using System.Collections;

// 메인 게임을 관리하는 씬.

public class CMainGame : CSeneObject
{
    [SerializeField] private CSOPlayerInfo ins_cSOPlayerInfo = null;
    [SerializeField] private CDoorAction ins_cDoorAction = null;
    [SerializeField] private CQuestReward ins_cQuestReward = null;                                                          // 완다의 퀘스트.
    [SerializeField] private CNpcSpecial _cNpcSpecial = null;

    private CZombieGenerator[] _cZombieGenerateor = new CZombieGenerator[3];


     private CPlayer _cPlayer;

    protected override void OnSceneAwake()
    {
        base.OnSceneAwake();

        //(조건) 플레이어의 네임이 있는 가 없는가.
        if (ins_cSOPlayerInfo.m_strName == string.Empty)
        {
            CSoundManager.Inst.Install();
            CSoundManager.Inst.gameObject.SetActive(false);
            CWheatherManager.Inst.SetTurnOnOrOff(false);
            // 오프닝 음악 재생.
            CSoundManager.Inst.SetSoundPlay(0);

            CDBManager.Inst.OnCreateDB(EmDBType.Level);
            CDBManager.Inst.OnCreateDB(EmDBType.Quest);
            CZombieDataManager.Inst.OnCreateDB();

            OnLoadCharacter();
            StartCoroutine(CorNpcLoad());

            OnLoadObject();
            ins_cDoorAction.InstallCity();
        }
 
    }


    private void OnLoadCharacter()
    {
        StartCoroutine(CUIManager.Inst.CorMainWin());
        CCameraManager.Inst.Initialization();
        CCameraManager.Inst.SetCameraView(EmCameraType.Openning);
        CWheatherManager.Inst.SetTurnOnOrOff(true);
    }

    private IEnumerator CorNpcLoad()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(_cNpcSpecial);
        _cNpcSpecial.transform.localPosition = new Vector3(-38.6464f, 0, -102.0391f);
        _cNpcSpecial.transform.localRotation = Quaternion.Euler(0, 50.791f, 0);
        yield return null;
    }


    protected override void OnSceneStart()
    {
        base.OnSceneStart();

        if (ins_cSOPlayerInfo.m_strName != string.Empty)
        {
            OnPlayerLoad();
            OnLoadZombieTwo();
            CObjectPoolManager.Inst.GetObjcectActive(true);
        }

     
    }


    // 씬전환 될 시 생성되는 플레이어 함수.
    private void OnPlayerLoad()
    {
        CUIManager.Inst.m_cUIPlayerMain.IsOpenMain(true, true);
        string PathNum = string.Empty;
        switch (ins_cSOPlayerInfo.m_eProfessional)
        {
            case EmProfessional.Orignal:
                PathNum = "01";
                break;

            case EmProfessional.Medical:
                PathNum = "02";

                break;

            case EmProfessional.Soldier:
                PathNum = "03";

                break;
        }
        CPlayer.GetPlayerInstance(null,
            new Vector3(-38.6464f, 0, -102.0391f),
            Quaternion.identity,
            true,
        CDataManager.m_strPlayerFolder + PathNum.ToString());


        CCameraManager.Inst.SetCameraView(EmCameraType.ThirdPerson);
    }

    private void OnLoadObject()
    {
#if Error
                if(ins_cSOPlayerInfo == null)
                {
                    CDebugLog.Log("ins_cSOPlayerInfo is empty", CDebugLog.ErrorID.Log);
                }
#endif

        StartCoroutine(CorObjectActive());
    }

    // ObjcetPooling과 좀비 생성.
    private IEnumerator CorObjectActive()
    {
        yield return new WaitForSeconds(5.0f);

        if (ins_cSOPlayerInfo.m_eProfessional == EmProfessional.None)
        {
            OnLoadObject();
            yield break;
        }
        CObjectPoolManager.Inst.Install();
        Instantiate(ins_cQuestReward);
        ins_cQuestReward.gameObject.transform.localPosition = new Vector3(-30, 2, -24);
        CWheatherManager.Inst.SetRandomWeather();

        yield return new WaitForSeconds(1.5f);

        OnLoadZombie();                     // 좀비 생성.
        yield return new WaitForSeconds(10.0f);
        CUIManager.Inst.m_cUIQuest.OnAddQuest(0);

        yield return null;
    }



    protected override void OnSceneUpdate()
    {
        base.OnSceneUpdate();
        OnZombieQuest();

    }

    private void OnZombieQuest()
    {
        for (int i = 1; i < _cZombieGenerateor.Length; i++)
        {
            if (_cZombieGenerateor[i] != null)
            {
                // 10마리 죽이기.
                if (i < 10)
                {
                    if (_cZombieGenerateor[i].gameObject.activeSelf == false)
                    {
                        CUIManager.Inst.m_cUIQuest.OnRemoveQuest(4);
                        break;
                    }
                }
            }
        }
    }


    private void OnLoadZombie()
    {
            _cZombieGenerateor[0] = new CZobieLevelOnePatternGnerate();
            _cZombieGenerateor[0].CreateZombie();

        _cZombieGenerateor[1] = new CZobieLevelTwoPatternGnerate();
        _cZombieGenerateor[1].CreateZombie();

    }

    private void OnLoadZombieTwo()
    {
   
        _cZombieGenerateor[2] = new CZobieLevelThreePatternGnerate();
        _cZombieGenerateor[2].CreateZombie();
    }


    // 테스트 나중에 지우기.
    private void OnDisable()
    {
        ins_cSOPlayerInfo.m_strName = string.Empty;
        ins_cSOPlayerInfo.m_eProfessional = EmProfessional.None;
    }

}
