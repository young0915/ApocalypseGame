using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class CWeaponCtrl : MonoBehaviour
{
    // T는 Two의 약자.
    public GameObject ins_objBaton;
    public GameObject ins_objSword;
    [SerializeField] private List<GameObject> ins_listobjShooting = new List<GameObject>();
    public List<GameObject> m_listobjShooting { get { return ins_listobjShooting; } }

    [SerializeField] private List<GameObject> ins_listobjTCrossBow = new List<GameObject>();
    public List<GameObject> m_listobjTCrossBow { get { return ins_listobjShooting; } }

    [SerializeField] private List<GameObject> ins_listobjBat = new List<GameObject>();
    public List<GameObject> m_listobjBat { get { return ins_listobjBat; } }

    [SerializeField] private List<GameObject> ins_listFriePos = new List<GameObject>();
    public List<GameObject> m_listFriePos { get { return ins_listFriePos; } }

    private const int _nBat = 5;
    private const int _nRelife = 8;
    private const int _nCrossBow = 14;

    private int nItemId = 0;
    // 무기 생성                                                  아이템 넘버.
    public void SetWeaponCreate(EmWeapon eWeapon)
    {
        if(CUIManager.Inst.m_cUIPhone.GetInvenSlot(16).m_cItem != null)
        {
            nItemId = CUIManager.Inst.m_cUIPhone.GetInvenSlot(16).m_cItem.m_nId;
        }

        ins_objBaton.SetActive(false);
        ins_objSword.SetActive(false);

        for (int i = 0; i < ins_listobjShooting.Count; i++)
        {
            ins_listobjShooting[i].SetActive(false);
        }

        for (int i = 0; i < ins_listobjTCrossBow.Count; i++)
        {
            ins_listobjTCrossBow[i].SetActive(false);
        }

        for (int i = 0; i < ins_listobjBat.Count; i++)
        {
            ins_listobjBat[i].SetActive(false);
        }

        switch (eWeapon)
        {
            case EmWeapon.Sword:
                ins_objSword.SetActive(true);
                break;

            case EmWeapon.Bat:
                nItemId -= _nBat;
                ins_listobjBat[nItemId].SetActive(true);

                break;

            case EmWeapon.Hybrid:
            case EmWeapon.Rifle:
                nItemId -= _nRelife;
                ins_listobjShooting[nItemId].SetActive(true);

                break;

            case EmWeapon.Crossbow:
                nItemId -= _nCrossBow;
                ins_listobjTCrossBow[nItemId].SetActive(true);

                break;

            case EmWeapon.None:
                break;
        }
    }

    // 군인은 따로 무기를 소지하고 있기 때문에 함수는 따로.ㄴ
    public void OnShied(bool _bIsOn)
    {
        ins_objBaton.SetActive(_bIsOn);
    }

}
