using UnityEngine;
using System.Collections;

public class CPlayerAnimation : MonoBehaviour
{

    [SerializeField] private Animator ins_aniPlayer = null;
    [SerializeField] private ParticleSystem ins_particleSystem;

    [SerializeField] private CIKHands ins_cIKHands = null;
    public CIKHands m_cIKHands { get { return ins_cIKHands; } }

    private EmAniState _eAniState = EmAniState.Idle;
    private EmWeapon _eWeapon = EmWeapon.None;
    private float _fCoolTime = 0.0f;

    #region [code] string Variables.
    private const string _strBaseLayer = "Base Layer";
    private const string _strAction = "Action";
    private const string _strActionTrigger = "ActionTrigger";
    private const string _strAttackTrigger = "AttackTrigger";

    private const string _strWeapon = "Weapon";
    private const string _strWeaponSwitch = "WeaponSwitch";
    private const string _StrWeaponSheathTrigger = "WeaponSheathTrigger";
    private const string _strWeaponUnsheathTrigger = "WeaponUnsheathTrigger";
    private const string _strLeftWeapon = "LeftWeapon";
    private const string _strRightWeapon = "RightWeapon";
    private const string _strBlocking = "Blocking";

    private const string _strLeftRight = "LeftRight";
    private const string _strJumping = "Jumping";
    private const string _strSheathLocation = "SheathLocation";

    private const string _strUnarmed = "Unarmed";
    private const string _strUnarmedIWalkRunBlend = "Unarmed-WalkRun-Blend";

    private const string _strMove = "Moving";
    private const string _strVelocityX = "Velocity X";
    private const string _strVelocityZ = "Velocity Z";
    private const string _strCharge = "Charge";

    private const string _strArmed = "Armed";
    private const string _strArmedWalkRunBlend = "Armed-WalkRun-Blend";

    private const string _strInjured = "Injured";

    // T는 two의 약자.
    private const string _strTHandSword = "2Hand-Sword";
    private const string _strTHandSwordWeaponSwitch = "2Hand-Sword-WeaponSwitch";
    private const string _strTHandSwordWalkRunBlend = "2Hand-Sword-WalkRun-Blend";
    private const string _strTHandSwordDamage = "2Hand-Sword-Damage";

    private const string _strShooting = "Shooting";
    private const string _strTShootingWeaponSwitch = "Shooting-WeaponSwitch";
    private const string _strShootingMovementBlend = "Shooting-Movement-Blend";
    private const string _strTShootingUnsheathBackUnarmed = "Shooting-Unsheath-Back-Unarmed";
    private const string _strShootingDamage = "Shooting-Damage";
    private const string _strShootingBlocking = "Shooting-Blocking";

    private const string _strTHandCrossbow = "2Hand-Crossbow";
    private const string _strTHandCrossbowWeaponSwitch = "2Hand-Crossbow-WeaponSwitch";
    private const string _strTHandCrossbowWalkRunBlend = "2Hand-Crossbow-WalkRun-Blend";
    private const string _strTHandCrossbowDamage = " 2Hand-Crossbow-Damage";

    private const string _strShield = "Shield";

    private const string _strAiming = "Aiming";
    private const string _strAttackSide = "AttackSide";

    private const string _strBlockBreakTrigger = "BlockBreakTrigger";
    private const string _strRevive1Trigger = "Revive1Trigger";
    private const string _strInstantSwitchTrigger = "InstantSwitchTrigger";
    private const string _strGetHitTrigger = "GetHitTrigger";
    private const string _strDeath1Trigger = "Death1Trigger";

    #endregion

    #region [code] Variables.

    private string _strAniAction = string.Empty;
    private string _strAniWeaponSwitch = string.Empty;
    private string _strAniRun = string.Empty;
    private string _strAniAttack = string.Empty;
    private string _strAniDamege = string.Empty;
    private string _strAniGetHit = string.Empty;
    private string _strAniDeath = string.Empty;

    private bool _bIsMoving = false;
    private bool _bIsShield = false;
    private bool _bIsAiming = false;
    private bool _bIsBlocking = false;
    private bool _bIsInjured = false;

    private int _nAction = 0;
    private int _nWeapon = 0;
    private int _nLeftRight = 0;
    private int _nRightWeapon = 0;
    private int _nSheathLocation = 0;
    private int _nWeaponSwitch = 0;
    private int _nAttackSide = 0;


    #endregion


    // 기본변수 세팅.
    private void SetAni()
    {
        _bIsShield = false;
        _bIsAiming = false;
        _bIsInjured = false;

        _nAction = 0;
        _nWeapon = 0;
        _nLeftRight = 0;
        _nRightWeapon = 0;
        _nSheathLocation = 0;
        _nWeaponSwitch = 0;
        _nAttackSide = 0;
    }


    // 걷기, 뛰기.
    public void SetPlayerMoveAnimation(EmWeapon eWeapon, float fVelocityX = 0.0f,
        float fVelocityZ = 0.0f, float fCharge = 0.0f)
    {
        if (_bIsInjured)
        {
            ins_aniPlayer.SetBool(_strInjured, false);
        }
        ins_aniPlayer.SetBool(_strMove, true);
        // 무기를 장착 안한 상태에서 Charge 고민중.

        switch (eWeapon)
        {
            case EmWeapon.Sword:
                _strAniAction = _strArmed;
                _strAniRun = _strArmedWalkRunBlend;

                break;

            case EmWeapon.Bat:
                _strAniAction = _strTHandSword;
                _strAniRun = _strTHandSwordWalkRunBlend;

                break;

            case EmWeapon.Hybrid:
            case EmWeapon.Rifle:
                _strAniAction = _strShooting;
                _strAniRun = _strShootingMovementBlend;

                break;

            case EmWeapon.Crossbow:
                _strAniAction = _strTHandCrossbow;
                _strAniRun = _strTHandCrossbowWalkRunBlend;

                break;

            case EmWeapon.None:
                _strAniAction = _strUnarmed;
                _strAniRun = _strUnarmedIWalkRunBlend;
                break;
        }

        ins_aniPlayer.Play(_strAniAction);
        ins_aniPlayer.Play(_strAniRun);
        ins_aniPlayer.SetBool(_strMove, true);
        ins_aniPlayer.SetFloat(_strVelocityX, fVelocityX);
        ins_aniPlayer.SetFloat(_strVelocityZ, fVelocityZ);

        if (fCharge >= 0)
        {
            ins_aniPlayer.SetFloat(_strCharge, fCharge);
        }
    }

    public void SetAniState(EmAniState eAniState = EmAniState.Idle, EmWeapon eWeapon = EmWeapon.None, float fCoolTime = 0.0f)
    {
        this._eAniState = eAniState;
        this._eWeapon = eWeapon;
        this._fCoolTime = fCoolTime;

        SetAni();                                                                   // 변수 초기화.

        switch (_eAniState)
        {
            case EmAniState.Idle:
                ins_aniPlayer.SetBool(_strMove, false);

                break;
            case EmAniState.WeaponChange:

                StartCoroutine(CorChangeWeapon(_eWeapon, fCoolTime));

                break;

            case EmAniState.GetOffWeapon:
                OnGetOffWeapon(eWeapon);

                break;

            case EmAniState.Attack:
                OnAttack(_eWeapon);

                break;

            case EmAniState.Injured:
                this._bIsInjured = true;
                ins_aniPlayer.SetBool(_strInjured, _bIsInjured);

                break;

            case EmAniState.Boost:
                OnBoost(_eWeapon);

                break;

            case EmAniState.GetHit:
                OnGetHit(_eWeapon);

                break;

            case EmAniState.Death:
                OnDeath(_eWeapon);

                break;
        }
    }

    //  무기 체인지.
    private IEnumerator CorChangeWeapon(EmWeapon eWeapon, float fCoolTime = 0.0f)
    {
        _bIsMoving = false;

        switch (eWeapon)
        {
            case EmWeapon.Sword:
                _strAniAction = _strUnarmed;

                _nLeftRight = 2;
                _nSheathLocation = 1;
                _nWeaponSwitch = 7;

                ins_aniPlayer.SetInteger(_strLeftRight, _nLeftRight);
                break;

            case EmWeapon.Bat:

                _strAniAction = _strTHandSword;
                _strAniWeaponSwitch = _strTHandSwordWeaponSwitch;
                _nWeapon = 1;
                _nSheathLocation = 0;
                _nWeaponSwitch = 0;

                break;

            case EmWeapon.Hybrid:
            case EmWeapon.Rifle:

                _strAniAction = _strShooting;
                _strAniWeaponSwitch = _strTShootingWeaponSwitch;
                _nWeapon = 18;

                break;

            case EmWeapon.Crossbow:
                _strAniAction = _strTHandCrossbow;

                _strAniWeaponSwitch = _strTHandCrossbowWeaponSwitch;
                _nWeapon = 5;
                break;

            case EmWeapon.Shield:
                _bIsShield = true;
                _nWeapon = 0;
                _nLeftRight = 2;
                _nWeaponSwitch = 7;

                ins_aniPlayer.SetBool(_strShield, _bIsShield);
                ins_aniPlayer.SetInteger(_strLeftWeapon, 7);
                ins_aniPlayer.SetInteger(_strLeftRight, _nLeftRight);

                break;

            case EmWeapon.None:
                _bIsMoving = true;

                break;
        }

        ins_aniPlayer.Play(_strAniAction);
        ins_aniPlayer.SetTrigger(_strWeaponUnsheathTrigger);
        ins_aniPlayer.SetBool(_strMove, _bIsMoving);
        ins_aniPlayer.SetInteger(_strWeapon, _nWeapon);
        ins_aniPlayer.SetInteger(_strSheathLocation, _nSheathLocation);
        ins_aniPlayer.SetInteger(_strWeaponSwitch, _nWeaponSwitch);


        if (eWeapon == EmWeapon.None || eWeapon == EmWeapon.Shield || eWeapon == EmWeapon.Sword)
        {
            StartCoroutine(ins_cIKHands.CorBlendIK(false, 0, 0.2f, _nWeapon));
        }

        if (eWeapon == EmWeapon.Shield)
        {
            ins_cIKHands.m_cWeaponCtrl.OnShied(true);
            yield return new WaitForSeconds(CUIManager.Inst.m_cUIPlayerMain.m_cSoPlayerInfo.m_fCoolTime);
            OnGetOffWeapon(EmWeapon.Shield);

        }
        else
        {
            ins_cIKHands.m_cWeaponCtrl.SetWeaponCreate(eWeapon);
        }


        StartCoroutine(ins_cIKHands.CorBlendIK(true, 0.5f, 1, _nWeapon));
        ins_aniPlayer.SetBool(_strMove, false);

        yield return null;
    }

    // 공격 상태                                                            군인만 사용할 수 있는 것.
    private void OnAttack(EmWeapon eWeapon)
    {
        int nSound = 0;
        _bIsMoving = false;
        _strAniAttack = _strAttackTrigger;

        switch (eWeapon)
        {
            case EmWeapon.Sword:
                _nAction = Random.Range(8, 14);
                _nWeapon = 7;
                _nRightWeapon = 9;
                _nAttackSide = 2;

                ins_aniPlayer.SetInteger(_strRightWeapon, _nRightWeapon);
                ins_aniPlayer.SetBool(_strShield, false);

                nSound = 7;

                break;
            case EmWeapon.Bat:
                _nAction = Random.Range(1, 4);
                _nWeapon = 1;

                nSound = 5;

                break;

            case EmWeapon.Crossbow:
                _nAction = Random.Range(4, 7);
                _nWeapon = 5;
                _bIsAiming = false;

                ins_aniPlayer.SetBool(_strAiming, false);

                nSound = 8;

                break;

            case EmWeapon.Hybrid:
            case EmWeapon.Rifle:
                _nWeapon = 18;
                _nAttackSide = 2;

                ins_aniPlayer.SetBool(_strAiming, false);

                nSound = 6;

                   break;
            case EmWeapon.Shield:
                break;

            case EmWeapon.None:
                _nWeapon = 7;
                _nAction = Random.Range(1, 4);
                break;

        }


        ins_aniPlayer.SetTrigger(_strAniAttack);

        ins_aniPlayer.SetBool(_strMove, _bIsMoving);
        ins_aniPlayer.SetInteger(_strJumping, 0);
        ins_aniPlayer.SetInteger(_strAttackSide, _nAttackSide);

        // 방패만 이 함수가  쓰임.
        ins_aniPlayer.SetInteger(_strAction, _nAction);
        ins_aniPlayer.SetInteger(_strWeapon, _nWeapon);

        CSoundManager.Inst.SetAttackSound(nSound);
        CSoundManager.Inst.SetSoundPlay(1, 0.4f, true);
    }

    // 맞기.
    private void OnGetHit(EmWeapon eWeapon)
    {
        _bIsMoving = false;
        _strAniGetHit = _strGetHitTrigger;
        _bIsBlocking = false;
        switch (eWeapon)
        {
            case EmWeapon.Sword:
            case EmWeapon.Shield:
            case EmWeapon.None:
                _nAction = Random.Range(1, 3);
                _nWeapon = 7;

                break;

            case EmWeapon.Bat:
                _nAction = Random.Range(2, 4);
                _nWeapon = 1;

                break;

            case EmWeapon.Hybrid:
            case EmWeapon.Rifle:
                _strAniDamege = _strShootingBlocking;
                _nAction = 2;
                ins_aniPlayer.SetTrigger(_strBlockBreakTrigger);

                break;

            case EmWeapon.Crossbow:
                _nAction = Random.Range(2, 5);
                _nWeapon = 5;

                break;
        }


        ins_aniPlayer.Play(_strAniDamege);
        ins_aniPlayer.SetTrigger(_strAniGetHit);
        ins_aniPlayer.SetInteger(_strAction, _nAction);
        ins_aniPlayer.SetInteger(_strWeapon, _nWeapon);
        ins_aniPlayer.SetBool(_strBlocking, _bIsBlocking);
        ins_aniPlayer.SetBool(_strAniDeath, _bIsMoving);
    }

    // 죽음.
    private void OnDeath(EmWeapon eWeapon)
    {
        _strAniDeath = _strDeath1Trigger;
        _bIsMoving = false;
        _bIsShield = false;
        switch (eWeapon)
        {
            case EmWeapon.Sword:
            case EmWeapon.None:
            case EmWeapon.Hybrid:
            case EmWeapon.Rifle:
                break;

            case EmWeapon.Bat:
                _nWeapon = 1;
                break;

            case EmWeapon.Crossbow:
                _nWeapon = 5;

                break;

            case EmWeapon.Shield:
                _bIsShield = true;

                break;

        }

        // effect.
        StartCoroutine(CorBloodeffect());

        ins_aniPlayer.SetBool(_strAniDeath, _bIsMoving);
        ins_aniPlayer.SetTrigger(_strAniDeath);
        ins_aniPlayer.SetBool(_strShield, _bIsShield);
        ins_aniPlayer.SetInteger(_strWeapon, _nWeapon);


        StartCoroutine(CUIManager.Inst.CorPlayerAction(EmActionType.GameOver));
        StartCoroutine(CUIManager.Inst.CorITweenFade());
       
    }

    private IEnumerator CorBloodeffect()
    {
        ins_particleSystem.gameObject.SetActive(true);
        ins_particleSystem.Play();
        yield return new WaitForSeconds(1.0f);
        ins_particleSystem.gameObject.SetActive(false);
    }

    private void OnBoost(EmWeapon eWeapon)
    {
        _nAction = 9;
        Debug.Log("넘어옴");
        switch (eWeapon)
        {
            case EmWeapon.Sword:

                _nAction = 7;

                break;

            case EmWeapon.Bat:
                _nWeapon = 1;
                break;

            case EmWeapon.Hybrid:
            case EmWeapon.Rifle:
                _nWeapon = 18;
                break;

            case EmWeapon.Crossbow:
                _nWeapon = 5;

                break;

            case EmWeapon.None:
                _nWeapon = 0;


                break;
        }

        ins_aniPlayer.SetTrigger(_strActionTrigger);
        ins_aniPlayer.SetInteger(_strWeapon, _nWeapon);
        ins_aniPlayer.SetInteger(_strAction, _nAction);

    }

    private void OnGetOffWeapon(EmWeapon eWeapon)
    {
        _bIsShield = false;
        _bIsMoving = false;

        switch (eWeapon)
        {
            case EmWeapon.Sword:
                _nWeapon = 7;
                _nLeftRight = 2;
                _nSheathLocation = 0;
                _nWeaponSwitch = -1;

                ins_aniPlayer.SetBool(_strShield, _bIsShield);
                ins_aniPlayer.SetInteger(_strLeftRight, _nLeftRight);

                break;

            case EmWeapon.Bat:
                _nWeapon = 1;
                _nSheathLocation = 0;
                _nWeaponSwitch = 0;

                break;

            case EmWeapon.Hybrid:
            case EmWeapon.Rifle:
                _nWeapon = 18;
                _nSheathLocation = 0;
                _nWeaponSwitch = -1;

                break;

            case EmWeapon.Crossbow:
                _nWeapon = 5;
                _nSheathLocation = 1;
                _nWeaponSwitch = 0;

                break;

            case EmWeapon.Shield:

                _nWeapon = 7;
                _nLeftRight = 1;
                _nSheathLocation = 0;
                _nWeaponSwitch = 7;

                ins_aniPlayer.SetBool(_strShield, _bIsShield);
                ins_aniPlayer.SetInteger(_strLeftRight, _nLeftRight);

                break;

        }
        ins_aniPlayer.SetTrigger(_StrWeaponSheathTrigger);
        ins_aniPlayer.SetBool(_strMove, _bIsMoving);
        ins_aniPlayer.SetInteger(_strWeapon, _nWeapon);
        ins_aniPlayer.SetInteger(_strSheathLocation, _nSheathLocation);
        ins_aniPlayer.SetInteger(_strWeaponSwitch, _nWeaponSwitch);

        if (eWeapon == EmWeapon.Shield)
        {
            ins_cIKHands.m_cWeaponCtrl.OnShied(false);
        }
        else
        {
            ins_cIKHands.m_cWeaponCtrl.SetWeaponCreate(EmWeapon.None);
        }

        ins_aniPlayer.Play(_strBaseLayer);
        StartCoroutine(ins_cIKHands.CorBlendIK(false, 0.2f, 0));
    }


    // 총알 위치.
    public GameObject GetFirePos(int num)
    {
        return ins_cIKHands.m_cWeaponCtrl.m_listFriePos[num];
    }
}
