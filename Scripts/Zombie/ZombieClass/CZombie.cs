using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public abstract class CZombie : MonoBehaviour
{
    [SerializeField] protected CSOItem ins_cSoItem;
    [SerializeField] protected SpriteRenderer ins_spriteSign;                                          // 미니맵에 필요한 것.
    [SerializeField] protected Animator ins_AniZombie;
    [SerializeField] protected Rigidbody ins_rigZombie;
    [SerializeField] protected int ins_nZombieNum;
    [SerializeField] protected CZombieFOV cZombieFov;

    protected CZombieInfo cZombieInfo;
    protected Vector3 VecDirection;                                                     // 방향.
    private NavMeshAgent _NavMeshAgent;


    #region Zombie Animation string Variable
    protected const string strAniIdle = "Zombie Idle";
    protected const string strAniScream = "Scream";
    protected const string strAniTurn = "Turn";
    protected const string strAniWalk = "Walk";
    protected const string strAniRun = "Run";
    protected const string strAniPunching = "Punching";
    protected const string strAniAttack = "Attack";
    protected const string strAniReactionHit = "ReactionHit";
    protected const string strAniBiting = "Biting";
    protected const string strAniDeath = "Death";
    protected const string strAniAxe = "Axe";

    // EasyMode 에서 사용할 곳.
    protected const string strBitTwo = "BitTwo";
    protected const string strDying = "Dying";

    #endregion

    protected const int nMinPosX = -22;
    protected const int nMaxPosX = 175;
    protected const int nMinPosZ = -210;
    protected const int nMaxPosZ = 86;
    protected const float fAniStopSec = 5.0f;
    protected const float fAniTurnSec = 0.8f;
    protected bool bIsStop = false;
    protected bool bIsRun = false;
    protected bool bIsTurn = false;
    protected string strPrefabName;


    // _cSOZobieInfo 지정할 곳.
    private void Start()
    {
        _NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected abstract void Awake();


    protected virtual void FixedUpdate()
    {
        if (cZombieFov.IsTracePlayer())
        {
            if (bIsRun == true)
            {
                _NavMeshAgent.destination = cZombieFov.m_PlayerTr.transform.position;

            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Attack();
            CSoundManager.Inst.SetAttackSound(4);
            CSoundManager.Inst.SetSoundPlay(1, 0.4f, true);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ZombieAttack();
            return;
        }

        if (other.CompareTag("Bomb"))
        {
            Death();
        }

    }

    protected virtual void ZombieAttack()
    {

        int nAttack = Random.Range(1, cZombieInfo.m_nAttack);
        int nCritical = Random.Range(1, cZombieInfo.m_nCriticalHit);

        CUIManager.Inst.m_cUIPlayerMain.SetDamage(nAttack + (nCritical % 2));
        CSoundManager.Inst.SetSoundPlay(1, 0.4f, true);
    }


    // 무기에 맞을 충돌함수.
    protected virtual void OnCollisionEnter(Collision col)
    {
        int nItemId = 0;
        int nAttack = 0;
        int nCritical = 0;
        if (col.collider.CompareTag("bullet") || col.collider.CompareTag("arrow") || col.collider.CompareTag("Weapon"))
        {

            nItemId = CUIManager.Inst.m_cUIPhone.GetInvenSlot(16).m_cItem.m_nId;

            nAttack = Random.Range(1, ins_cSoItem.m_listItem[nItemId].m_nAttack);
            nCritical = Random.Range(1, ins_cSoItem.m_listItem[nItemId].m_nCriticalHit);
            int nRand = Random.Range(1, 2);
            cZombieInfo.m_nHp -= (nAttack*2) + nCritical;

            Debug.Log(cZombieInfo.m_nHp);

            ReactionHit();

            if (cZombieInfo.m_nHp <= 0)
            {
                Death();
            }
        }

    }

    protected virtual void OnRandomAction()
    {
        int nAction = Random.Range(0, 3);
        switch (nAction)
        {
            // Idle.
            case 0:
                ins_AniZombie.Play(strAniIdle);
                break;
            // Walk.
            case 1:

                bIsStop = true;
                bIsRun = false;
                break;
            // Run.
            case 2:
                bIsRun = true;
                break;
        }
        StartCoroutine(CorAniRandomCortol(nAction));
    }

    protected virtual void Run(int nZombieVersion)
    {
        ins_AniZombie.SetBool(strAniWalk, true);
        ins_AniZombie.SetBool(strAniRun, true);
        ins_rigZombie.MovePosition(transform.position
        + (transform.forward * ((CZombieDataManager.Inst.m_cZombielist[ins_nZombieNum].m_fSpeed) / 2) * Time.deltaTime));

    }

    protected virtual void ReactionHit()
    {
        ins_AniZombie.SetBool(strAniReactionHit, true);
        StartCoroutine(CorAniAttack(4));
    }

    // 오버로딩.
    protected virtual IEnumerator CorAniRandomCortol() { yield return null; }

    protected virtual IEnumerator CorAniRandomCortol(int nAction)
    {
        yield return new WaitForSeconds(8.0f);
        bIsStop = false;

        if (nAction == 2)
        {
            bIsRun = false;
            ins_AniZombie.SetBool(strAniRun, bIsRun);
        }

        ins_AniZombie.SetBool(strAniWalk, bIsStop);
        int RandomRot = Random.Range(0, 2);
        StartCoroutine(CorRotation(RandomRot));
        yield return new WaitForSeconds(fAniStopSec);
        OnRandomAction();
    }

    protected IEnumerator CorRotation(int nRotiona)
    {
        yield return new WaitForSeconds(fAniTurnSec);

        switch (nRotiona)
        {
            case 0:
                yield return new WaitForSeconds(fAniTurnSec);
                VecDirection.Set(0, Random.Range(0.0f, 360.0f), 0);
                yield break;
            case 1:
                int nRot = Random.Range(0, 4);
                float fangle = 0.0f;
                switch (nRot)
                {
                    case 0:
                        fangle = 90;
                        break;
                    case 1:
                        fangle = 180;
                        break;
                    case 2:
                        fangle = 270;
                        break;
                    case 3:
                        fangle = 360;
                        break;
                }
                Vector3 rot = Vector3.Lerp(transform.eulerAngles, VecDirection * fangle, 10.0f * Time.deltaTime);
                ins_rigZombie.MoveRotation(Quaternion.Euler(rot));

                yield return new WaitForSeconds(fAniTurnSec);
                yield break;
        }

    }


    protected virtual IEnumerator CorAniAttack(int nAction)
    {
        yield return new WaitForSeconds(fAniStopSec);
        string strAniName = string.Empty;
        switch (nAction)
        {
            case 0:
                strAniName = strAniAttack;
                break;
            case 1:
                strAniName = strAniPunching;
                break;
            case 2:
                strAniName = strAniAxe;
                break;
            case 3:
                strAniName = strAniScream;
                break;
            case 4:
                strAniName = strAniReactionHit;
                break;
        }
        bIsStop = true;
        ins_AniZombie.SetBool(strAniName, false);
        yield return new WaitForSeconds(fAniStopSec);
        OnRandomAction();
    }

    protected virtual IEnumerator CorAniDeath()
    {
        yield return new WaitForSeconds(5.0f);
        gameObject.SetActive(false);
    }

    // 좀비들에게 필요한 애니메이션.
    protected abstract void Walk(int nZombieVersion);                 // 기어다니는 것 도 포함.
    protected abstract void Attack();
    protected abstract void Death();
}
