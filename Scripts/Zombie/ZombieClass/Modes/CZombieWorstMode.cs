using System.Collections;
using UnityEngine;

public class CZombieWorstMode : CZombie
{
    private bool _bEquip = false;                            // 무기 장착 여부.

    public CZombieWorstMode(string strName, bool bEquip = false)
    {
        this.strPrefabName = strName;

        var prefab = CResourceLoader.Load<CZombieWorstMode>(strName);
        CZombie obj = Instantiate(prefab) as CZombieWorstMode;
        this._bEquip = bEquip;

        obj.transform.position = new Vector3(Random.Range(nMinPosX, nMaxPosX), 1, Random.Range(nMinPosZ, nMaxPosZ));

        obj.transform.parent = CSceneManager.Inst.m_CurScene.transform;
    }

    protected override void Awake()
    {

        cZombieInfo = new CZombieInfo(
            (EmZombieType)CZombieDataManager.Inst.m_cZombielist[3].m_eZombieType,
            CZombieDataManager.Inst.m_cZombielist[3].m_nId,
            CZombieDataManager.Inst.m_cZombielist[3].m_strLevel,
            CZombieDataManager.Inst.m_cZombielist[3].m_strDescript,
            CZombieDataManager.Inst.m_cZombielist[3].m_nHp,
            CZombieDataManager.Inst.m_cZombielist[3].m_fSpeed,
            CZombieDataManager.Inst.m_cZombielist[3].m_nAttack,
            CZombieDataManager.Inst.m_cZombielist[3].m_nCriticalHit
);


        if (CUIManager.Inst.m_cUICreatCharacter.m_cSoPlayerInfo.m_eProfessional == EmProfessional.Orignal)
        {
            ins_spriteSign.gameObject.SetActive(true);
        }
        OnRandomAction();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (bIsStop)
        {
            Walk(3);
        }

        if (bIsRun)
        {
            Run(3);
        }
    }

    protected override void Walk(int nZombieVersion)
    {
        bIsRun = false;
        ins_AniZombie.SetBool(strAniWalk, true);
        ins_rigZombie.MovePosition(transform.position + (transform.forward * (cZombieInfo.m_fSpeed / 10) * Time.deltaTime));
    }


    protected override void Attack()
    {
        if (bIsRun || bIsStop)
        {
            bIsStop = false;
            bIsRun = false;
            ins_AniZombie.SetBool(strAniWalk, bIsStop);
            ins_AniZombie.SetBool(strAniRun, bIsRun);
        }
        string strAniName = string.Empty;
        int nActtion = Random.Range(0, 4);
        switch (nActtion)
        {
            case 0:
                strAniName = strAniAttack;
                break;
            case 1:
                strAniName = strAniPunching;
                break;
            case 2:
                if (_bEquip == true)
                {
                    strAniName = strAniAxe;
                }
                break;
            case 3:
                strAniName = strAniScream;
                break;
        }
        ins_AniZombie.SetBool(strAniName, true);


        StartCoroutine((CorAniAttack(nActtion)));
    }


    protected override void Death()
    {
        bIsStop = false;
        ins_AniZombie.SetBool(strAniDeath, true);
        StartCoroutine(CorAniDeath());
    }


}
