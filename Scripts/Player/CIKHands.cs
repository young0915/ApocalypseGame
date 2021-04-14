using UnityEngine;
using System.Collections;

public class CIKHands : MonoBehaviour
{
    private Animator _AniPlayer;
    private CWeaponCtrl _cWeaponCtrl;
    public CWeaponCtrl m_cWeaponCtrl { get { return _cWeaponCtrl; } }
    [SerializeField] private Transform ins_TraLeftHandObj;
    [SerializeField] private Transform ins_TraAttachLeft =null;

    [SerializeField] [Range(0, 1)] private float ins_fLeftHandPositionWeight;
    [SerializeField] [Range(0, 1)] private float ins_fLeftHandRotationWeight;

    private Transform _TrablendToTransform =null;

    private const int _nBat = 5;
    private const int _nRelife = 8;
    private const int _nCrossBow = 14;

    private void Awake()
    {
        _AniPlayer = GetComponent<Animator>();
        _cWeaponCtrl = GetComponent<CWeaponCtrl>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if(ins_TraLeftHandObj != null)
        {
            _AniPlayer.SetIKPositionWeight(AvatarIKGoal.LeftHand, ins_fLeftHandPositionWeight);
            _AniPlayer.SetIKRotationWeight(AvatarIKGoal.LeftHand, ins_fLeftHandRotationWeight);
            _AniPlayer.SetIKPosition(AvatarIKGoal.LeftHand, ins_TraAttachLeft.position);
            _AniPlayer.SetIKRotation(AvatarIKGoal.LeftHand, ins_TraAttachLeft.rotation);
        }
    }


    public IEnumerator CorBlendIK(bool bIsblendOn, float fdelay, float fTimeToBlend, int nWeapon =0)
    {
        if(nWeapon ==1 || nWeapon == 5 || nWeapon ==18  )
        {
            GetCurrentWeaponAttachPoint(nWeapon);
        }
        else
        {
            if(nWeapon ==0)
            {
                ins_fLeftHandPositionWeight = 0;
                ins_fLeftHandRotationWeight = 0;
            }
            yield break;
        }

        yield return new WaitForSeconds(fdelay);
        float fTime = 0.0f;
        float fblendTo = 0.0f;
        float fblendFrom = 0.0f;

        if(bIsblendOn)
        {
            fblendTo = 1;
        }
        else
        {
            fblendFrom = 1;
        }
        while(fTime<1)
        {
            fTime += Time.deltaTime / fTimeToBlend;
            ins_TraAttachLeft = _TrablendToTransform;
            ins_fLeftHandPositionWeight = Mathf.Lerp(fblendFrom, fblendTo, fTime);
            ins_fLeftHandRotationWeight = Mathf.Lerp(fblendFrom, fblendTo, fTime);
            yield return null;
        }

        yield break;
    }

    private void GetCurrentWeaponAttachPoint(int nWeapon)
    {
        int nItemId = CUIManager.Inst.m_cUIPhone.GetInvenSlot(16).m_cItem.m_nId;
        switch (nWeapon)
        {
            case 1:
                nItemId -= _nBat;
                _TrablendToTransform = _cWeaponCtrl.m_listobjBat[nItemId].transform.GetChild(0).transform;

                break;

            case 5:
                nItemId -= _nCrossBow;
                _TrablendToTransform = _cWeaponCtrl.m_listobjTCrossBow[nItemId].transform.GetChild(0).transform;

                break;

            case 18:
                nItemId -= _nRelife;
                _TrablendToTransform = _cWeaponCtrl.m_listobjShooting[nItemId].transform.GetChild(0).transform;

                break;
        }

    }



}

