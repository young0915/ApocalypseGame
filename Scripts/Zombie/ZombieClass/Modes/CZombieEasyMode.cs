using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CZombieEasyMode : CZombie
{
    public CZombieEasyMode(string strName)
    {
        this.strPrefabName = strName;

        var prefab = CResourceLoader.Load<CZombieEasyMode>(strName);
        CZombie obj = Instantiate(prefab) as CZombieEasyMode;

        obj.transform.position = new Vector3(Random.Range(nMinPosX, nMaxPosX), 1, Random.Range(nMinPosZ, nMaxPosZ));

        obj.transform.parent = CSceneManager.Inst.m_CurScene.transform;

    }

    protected override void Awake()
    {
     
        if (CUIManager.Inst.m_cUICreatCharacter.m_cSoPlayerInfo.m_eProfessional == EmProfessional.Orignal)
        {
            ins_spriteSign.gameObject.SetActive(true);
        }

        cZombieInfo = new CZombieInfo(
            (EmZombieType)CZombieDataManager.Inst.m_cZombielist[0].m_eZombieType,
            CZombieDataManager.Inst.m_cZombielist[0].m_nId,
            CZombieDataManager.Inst.m_cZombielist[0].m_strLevel,
            CZombieDataManager.Inst.m_cZombielist[0].m_strDescript,
            CZombieDataManager.Inst.m_cZombielist[0].m_nHp,
            CZombieDataManager.Inst.m_cZombielist[0].m_fSpeed,
            CZombieDataManager.Inst.m_cZombielist[0].m_nAttack,
            CZombieDataManager.Inst.m_cZombielist[0].m_nCriticalHit
            );
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        // bIsStop false일 경우.
        if (!bIsStop)
        {
            Walk(0);
        }
    }

    protected override void Walk(int nZombieVersion)
    {
        ins_rigZombie.MovePosition(transform.position + (transform.forward * (cZombieInfo.m_fSpeed / 10) * Time.deltaTime));
    }


    protected override void Attack()
    {
        bIsStop = true;
        ins_AniZombie.SetBool(strBitTwo, bIsStop);

    
        // (조건)플레이어의 체력이 0이 아닐 경우. 
        StartCoroutine(CorAniAttack());
    }

    // 마치는 것.
    private IEnumerator CorAniAttack()
    {
        yield return new WaitForSeconds(8.0f);
        bIsStop = false;
        ins_AniZombie.SetBool(strBitTwo, bIsStop);
    }

    protected override void Death()
    {
        bIsStop = true;
        ins_AniZombie.SetBool(strDying, bIsStop);
        StartCoroutine(CorAniDeath());
    }
  
}
