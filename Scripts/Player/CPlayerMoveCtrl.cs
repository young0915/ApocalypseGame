using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CPlayerMoveCtrl : MonoBehaviour
{
    public CPlayer m_cPlayer { get; set; } = null;
    public CPlayerAnimation m_cPlayerAni { get { return m_cPlayer.m_cPlayerAnimation; } }
    public Rigidbody m_Rigidbody { get { return m_cPlayer.m_Rigidbody; } }

    // KeyCode 값.
    private Dictionary<KeyCode, Action> _KeyAction = new Dictionary<KeyCode, Action>();

    private const string _strPlayerStat = "Data/Player/CSOPlayer";
    private const float _fRotateSpeed = 20.0f;

    private CSOPlayerInfo ins_cSoPlayerInfo;

    // Horizontal, Vertical, Rotation.
    private Vector3 _VecRotation;
    private float _fInputHorizontal = 0.0f;
    private float _fInputVertical = 0.0f;
    private float _fInputRotation = 0.0f;
    private float _fInputLeftShift = 0;

    private bool _bIsEquipWeapon = false;                                                     // 무기를 장착했는가 안했는가.
    private bool _bIsCeramVIew = false;                                                       // 삼인칭인가 일인칭인가 판단하는 변수.

    void Start()
    {
        _VecRotation = gameObject.transform.rotation.eulerAngles;
        ins_cSoPlayerInfo = CResourceLoader.Load<CSOPlayerInfo>(_strPlayerStat);
        ins_cSoPlayerInfo.m_eMouseState = EmMouseState.None;

        _KeyAction = new Dictionary<KeyCode, Action>
        {
            { KeyCode.V, OnKeyCameraView },
            { KeyCode.Z, OnKeySkillSupport},
            { KeyCode.X, OnKeyThrowingBombs},
            { KeyCode.C, OnKeyOpenPhone},
            { KeyCode.Mouse0, OnKeyMouseFire },
            { KeyCode.R, OnKeyHeathUP},
            { KeyCode.U, OnKeyTotalCharging},
        };
    }

    void FixedUpdate()
    {
        m_cPlayerAni.transform.position = transform.position;
        _fInputHorizontal = Input.GetAxisRaw("Horizontal") * ins_cSoPlayerInfo.m_fSpeed;
        _fInputVertical = Input.GetAxisRaw("Vertical") * ins_cSoPlayerInfo.m_fSpeed;
        _fInputRotation = Input.GetAxisRaw("Rotation") * ins_cSoPlayerInfo.m_fSpeed;
        _fInputLeftShift = Input.GetAxisRaw("left shift") * ins_cSoPlayerInfo.m_fSpeed;
        OnRotate();

        SetCamera(ins_cSoPlayerInfo.m_eMouseState);


        if (_fInputHorizontal == 0 && _fInputVertical == 0)
        {
            OnKeyCodeAction();

            m_cPlayerAni.SetAniState();

            if (ins_cSoPlayerInfo.m_nHp <= 50)
            {
                m_cPlayerAni.SetAniState(EmAniState.Injured, ins_cSoPlayerInfo.m_eWeapon);

                if(ins_cSoPlayerInfo.m_nHp  >= 0)
                {
                    m_cPlayerAni.SetAniState(EmAniState.Death, ins_cSoPlayerInfo.m_eWeapon);

                }
            }
        }
        else
        {
            if (_fInputLeftShift > 0)
            {
                _fInputVertical = Input.GetAxisRaw("Vertical") * ins_cSoPlayerInfo.m_fSpeed * 2;
            }

            SetPlayerCtrlMove(_fInputHorizontal, _fInputVertical, _fInputLeftShift);
        }
    }


    private void SetPlayerCtrlMove(float h, float v, float c)
    {
        m_cPlayerAni.SetPlayerMoveAnimation(ins_cSoPlayerInfo.m_eWeapon, h, v, c);
        Vector3 moveDir = ((Vector3.forward * v) + (Vector3.right * h));
        m_Rigidbody.MovePosition(m_Rigidbody.position + moveDir
            * Time.deltaTime * ins_cSoPlayerInfo.m_fSpeed);
    }


    // 회전.
    private void OnRotate()
    {
        float fTurn = _fInputRotation * Time.deltaTime * _fRotateSpeed ;
        m_Rigidbody.rotation *= Quaternion.Euler(0, fTurn, 0);
    }


    // 카메라 상태 활성화 비활성화.
    private void SetCamera(EmMouseState eMouseState)
    {

        switch (eMouseState)
        {
            case EmMouseState.Attack:
            case EmMouseState.None:
                CCameraManager.Inst.enabled = true;
            
                break;

            case EmMouseState.UI:
                CCameraManager.Inst.enabled = false;
                
                break;
        }

    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Obstacle"))
        {
            CUIManager.Inst.m_cUIPlayerMain.SetDamage(2);
        }
        if(col.CompareTag("Zombie"))
        {
            m_cPlayerAni.SetAniState(EmAniState.GetHit, ins_cSoPlayerInfo.m_eWeapon);
        }
    }



    #region [code] Player Action about KeyCode 

    private void OnKeyCodeAction()
    {
        if (Input.anyKeyDown)
        {
            foreach (var dic in _KeyAction)
            {
                if (Input.GetKeyDown(dic.Key))
                {
                    dic.Value();
                }
            }
        }
    }

    private void OnKeyCameraView()
    {
        if (_bIsCeramVIew == true)
        {
            _bIsCeramVIew = false;
            CCameraManager.Inst.SetCameraView(EmCameraType.FirstPerson);
        }
        else
        {
            _bIsCeramVIew = true;
            CCameraManager.Inst.SetCameraView(EmCameraType.ThirdPerson);
        }
    }

    private void OnKeyOpenPhone()
    {
        StartCoroutine(CUIManager.Inst.CorPhone());
    }

    private void OnKeySkillSupport()
    {
        if (CUIManager.Inst.m_cUIPlayerMain.m_cSKillSlotlist[1].m_ImgCoolTime.fillAmount == 0)
        {
            CUIManager.Inst.m_cUIPlayerMain.m_cSKillSlotlist[1].SetSkill();

            switch (ins_cSoPlayerInfo.m_eProfessional)
            {
                case EmProfessional.Orignal:
                case EmProfessional.Medical:
                    StartCoroutine(CoolSkillActive(ins_cSoPlayerInfo.m_eProfessional));

                    break;

                case EmProfessional.Soldier:
                    m_cPlayerAni.SetAniState(EmAniState.WeaponChange, EmWeapon.Shield, ins_cSoPlayerInfo.m_fCoolTime);

                    break;
            }
        }
        else
        {
            return;
        }
    }

    private IEnumerator CoolSkillActive(EmProfessional eProfessional)
    {

        if (eProfessional == EmProfessional.Medical)
        {
            for (int i = 0; i < 12; i++)
            {
                m_cPlayer.m_CamMap.cullingMask = i;
            }

            m_cPlayer.m_CamMap.cullingMask = LayerMask.GetMask("ItemBox");
        }
        else if (eProfessional == EmProfessional.Orignal)
        {
            for (int i = 0; i < 12; i++)
            {
                m_cPlayer.m_CamMap.cullingMask = i;
            }

            m_cPlayer.m_CamMap.cullingMask = LayerMask.GetMask("Zombie");
        }
        yield return new WaitForSeconds(ins_cSoPlayerInfo.m_fCoolTime);
        for (int i = 0; i < 12; i++)
        {
            m_cPlayer.m_CamMap.cullingMask = i;
        }
    }

    const string strItem = "item";
    private void OnKeyMouseFire()
    {
        if (ins_cSoPlayerInfo.m_eMouseState == EmMouseState.Attack)
        {
            if (ins_cSoPlayerInfo.m_eWeapon.Equals(EmWeapon.Crossbow) || ins_cSoPlayerInfo.m_eWeapon.Equals(EmWeapon.Rifle) || ins_cSoPlayerInfo.m_eWeapon.Equals(EmWeapon.Hybrid))
            {
                if (CUIManager.Inst.m_cUIPlayerMain.m_nBulletCnt > 0)
                {
                    int nPosNum = 0;                                                                // bullect 나오는 위치.
                    int nWeapon = 0;
                    switch (ins_cSoPlayerInfo.m_eWeapon)
                    {   // 화살.

                        case EmWeapon.Crossbow:
                            nWeapon = 4;
                            if (strItem + 14 == CUIManager.Inst.m_cUIPhone.GetInvenSlot(16).m_cItem.m_strItemName)
                            {
                                nPosNum = 7;
                            }
                            else
                            {
                                nPosNum = 6;
                            }

                            break;

                        // 총알.
                        case EmWeapon.Hybrid:
                        case EmWeapon.Rifle:
                            nWeapon = 3;
                            if (strItem + 8 == CUIManager.Inst.m_cUIPhone.GetInvenSlot(16).m_cItem.m_strItemName)
                            {
                                nPosNum = 0;
                            }
                            else if (strItem + 9 == CUIManager.Inst.m_cUIPhone.GetInvenSlot(16).m_cItem.m_strItemName)
                            {
                                nPosNum = 1;
                            }
                            else if (strItem + 10 == CUIManager.Inst.m_cUIPhone.GetInvenSlot(16).m_cItem.m_strItemName)
                            {
                                nPosNum = 2;
                            }
                            else if (strItem + 11 == CUIManager.Inst.m_cUIPhone.GetInvenSlot(16).m_cItem.m_strItemName)
                            {
                                nPosNum = 3;

                            }
                            else if (strItem + 12 == CUIManager.Inst.m_cUIPhone.GetInvenSlot(16).m_cItem.m_strItemName)
                            {
                                nPosNum = 4;

                            }
                            else if (strItem + 13 == CUIManager.Inst.m_cUIPhone.GetInvenSlot(16).m_cItem.m_strItemName)
                            {
                                nPosNum = 5;

                            }
                            break;

                    } // switch of end.

                    var bullet = CObjectPoolManager.Inst.GetBullet(nWeapon);
                    if (bullet != null)
                    {

                        bullet.transform.position = m_cPlayerAni.GetFirePos(nPosNum).transform.position;
                        bullet.transform.rotation = m_cPlayerAni.GetFirePos(nPosNum).transform.rotation;
                        bullet.SetActive(true);

                    }

                    // 사용시 총알 감소.
                    CUIManager.Inst.m_cUIPlayerMain.SetUseBullet();
                    CUIManager.Inst.m_cUIPhone.GetInvenSlot(17).SetUseItem();
                } // if of end bullet Amount. 

            } // ins_cSoPlayerInfo of end.


            m_cPlayerAni.SetAniState(EmAniState.Attack, ins_cSoPlayerInfo.m_eWeapon);
        }

        if (CUIManager.Inst.m_cUIPhone == null)
        {
            return;
        }
        else
        {
            if (CUIManager.Inst.m_cUIPhone.gameObject.activeSelf == true)
            {
                ins_cSoPlayerInfo.m_eMouseState = EmMouseState.UI;
                if (CUIManager.Inst.m_cUIPhone.m_cUIInventory == null)
                {
                    return;
                }
                else
                {
                    if (CUIManager.Inst.m_cUIPhone.m_cUIInventory.gameObject.activeSelf == true)
                    {
                        // 16칸 슬롯에 아무것도 없으면.
                        if (CUIManager.Inst.m_cUIPhone.GetInvenSlot(16).m_imgItem.color.a == 0)
                        {
                            // 플레이어는 자연스럽게 무기를 벗는 애니메이션이 작동된다.
                            m_cPlayerAni.SetAniState(EmAniState.GetOffWeapon, ins_cSoPlayerInfo.m_eWeapon);
                            ins_cSoPlayerInfo.m_eWeapon = EmWeapon.None;
                            CUIManager.Inst.m_cUIPlayerMain.m_ImgWeaponIcon.enabled = false;
                        }
                        else
                        {
                            m_cPlayerAni.SetAniState(EmAniState.WeaponChange, ins_cSoPlayerInfo.m_eWeapon);
                        }
                    }
                }
            }
            else
            {
                ins_cSoPlayerInfo.m_eMouseState = EmMouseState.Attack;
                return;
            }
        }

    }

    private void OnKeyThrowingBombs()
    {
        if (CUIManager.Inst.m_cUIPhone.GetInvenSlot(18).m_cItem != null)
        {
            CUIManager.Inst.m_cUIPhone.GetInvenSlot(18).SetUseItem();

            var bullet = CObjectPoolManager.Inst.GetBullet(5);
            if (bullet != null)
            {

                bullet.transform.position = gameObject.transform.position;
                bullet.transform.rotation = gameObject.transform.rotation;
                bullet.SetActive(true);
            }
        }
    }

    private void OnKeyHeathUP()
    {
        if(CUIManager.Inst.m_cUIPhone.GetInvenSlot(15).m_cItem != null)
        {
            ins_cSoPlayerInfo.m_nHp += CUIManager.Inst.m_cUIPhone.GetInvenSlot(15).m_cItem.m_nRecovery;
            CUIManager.Inst.m_cUIPlayerMain.SetHealth(ins_cSoPlayerInfo.m_nHp);
            CUIManager.Inst.m_cUIPhone.GetInvenSlot(15).SetUseItem();

            m_cPlayerAni.SetAniState(EmAniState.Boost, ins_cSoPlayerInfo.m_eWeapon);

            if (CUIManager.Inst.m_cUIPhone.GetInvenSlot(15).m_nItemCnt <= 0)
            {
                CUIManager.Inst.m_cUIPlayerMain.m_ImgHpPotion.enabled = false;
            }
        }

    }


    private void OnKeyTotalCharging()
    {
        CUIManager.Inst.m_cUIPlayerMain.SetBulletCnt(30);
    }



    #endregion


}
