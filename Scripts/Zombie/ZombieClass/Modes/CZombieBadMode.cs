using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CZombieBadMode : CZombie
{


    public CZombieBadMode(string strName)
    {
        this.strPrefabName = strName;

        var prefab = CResourceLoader.Load<CZombieBadMode>(strName);
        CZombie obj = Instantiate(prefab) as CZombieBadMode;

        obj.transform.position = new Vector3(Random.Range(nMinPosX, nMaxPosX), 1, Random.Range(nMinPosZ, nMaxPosZ));

        obj.transform.parent = CSceneManager.Inst.m_CurScene.transform;
    }

    protected override void Awake()
    {

        cZombieInfo = new CZombieInfo(
            (EmZombieType)CZombieDataManager.Inst.m_cZombielist[2].m_eZombieType,
            CZombieDataManager.Inst.m_cZombielist[2].m_nId,
            CZombieDataManager.Inst.m_cZombielist[2].m_strLevel,
            CZombieDataManager.Inst.m_cZombielist[2].m_strDescript,
            CZombieDataManager.Inst.m_cZombielist[2].m_nHp,
            CZombieDataManager.Inst.m_cZombielist[2].m_fSpeed,
            CZombieDataManager.Inst.m_cZombielist[2].m_nAttack,
            CZombieDataManager.Inst.m_cZombielist[2].m_nCriticalHit);

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
            Walk(2);
        }

        if (bIsRun)
        {
            Run(2);
        }
    }


    protected override void Walk(int nZombieVersion)
    {
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
        int nActtion = Random.Range(0, 2);
        switch (nActtion)
        {
            case 0:
                strAniName = strAniAttack;
                break;
            case 1:
                strAniName = strAniPunching;
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


