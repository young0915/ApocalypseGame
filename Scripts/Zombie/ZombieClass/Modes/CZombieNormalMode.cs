using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CZombieNormalMode : CZombie
{
    public CZombieNormalMode(string strName)
    {
        this.strPrefabName = strName;

        var prefab = CResourceLoader.Load<CZombieNormalMode>(strName);
        CZombie obj = Instantiate(prefab) as CZombieNormalMode;

        obj.transform.position = new Vector3(Random.Range(nMinPosX, nMaxPosX), 1, Random.Range(nMinPosZ, nMaxPosZ));

        obj.transform.parent = CSceneManager.Inst.m_CurScene.transform;

    }

    protected override void Awake()
    {

        cZombieInfo = new CZombieInfo(
            (EmZombieType)CZombieDataManager.Inst.m_cZombielist[1].m_eZombieType,
            CZombieDataManager.Inst.m_cZombielist[1].m_nId,
            CZombieDataManager.Inst.m_cZombielist[1].m_strLevel,
            CZombieDataManager.Inst.m_cZombielist[1].m_strDescript,
            CZombieDataManager.Inst.m_cZombielist[1].m_nHp,
            CZombieDataManager.Inst.m_cZombielist[1].m_fSpeed,
            CZombieDataManager.Inst.m_cZombielist[1].m_nAttack,
            CZombieDataManager.Inst.m_cZombielist[1].m_nCriticalHit
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
            Walk(1);
        }
    }

    protected override void OnRandomAction()
    {
        base.OnRandomAction();

        int nAction = Random.Range(0, 2);
        switch (nAction)
        {
            // Idle.
            case 0:
                ins_AniZombie.Play(strAniIdle);
                break;

            // Walk or Run.
            case 1:
                    bIsStop = true;
                break;
        }
        StartCoroutine(CorAniRandomCortol());
    }

    protected override IEnumerator CorAniRandomCortol()
    {
        yield return new WaitForSeconds(10.0f);
        ins_AniZombie.Play(strAniIdle);
        OnRandomAction();
    }

    protected override void Walk(int nZombieVersion)
    {
        ins_AniZombie.SetBool(strAniWalk, true);
        ins_rigZombie.MovePosition(transform.position + (transform.forward * (cZombieInfo.m_fSpeed / 10) * Time.deltaTime));
    }



    protected override void Attack()
    {
        if (bIsStop)
        {
            bIsStop = false;
            ins_AniZombie.SetBool(strAniWalk, bIsStop);
        }
        bIsStop = false;
        string strAniName = string.Empty;
        strAniName = strAniAttack;
        ins_AniZombie.SetBool(strAniName, true);


        StartCoroutine((CorAniAttack(1)));
    }


    protected override void Death()
    {
        bIsStop = false;
        ins_AniZombie.SetBool(strAniDeath, true);
        StartCoroutine(CorAniDeath());
    }

}
