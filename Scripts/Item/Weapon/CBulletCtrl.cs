using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBulletCtrl : MonoBehaviour
{
    [SerializeField] private CSOItem ins_cSOItem;
    [SerializeField]  private int ins_nId = 0;
    private int _nBullectAttack = 0;
    public int m_nbullectAttack { get { return _nBullectAttack; } }
    private float _fBullectSpeed = 0.0f;

    private Transform _traBullet;
    private Rigidbody _rdBullet;

    private void Awake()
    {
        _traBullet = GetComponent<Transform>();
        _rdBullet = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        if (gameObject.tag.Equals("bullet"))
        {
            ins_nId = 16;
        }
        else if (gameObject.tag.Equals("arrow"))
        {
            ins_nId = 17;
        }

        _nBullectAttack = ins_cSOItem.m_listItem[ins_nId].m_nAttack;
        _fBullectSpeed = ins_cSOItem.m_listItem[ins_nId].m_fSpeed;

        GetComponent<Rigidbody>().AddForce(Vector3.forward * _fBullectSpeed);
       
        StartCoroutine(CorBullectActive());
    }

    private void OnDisable()
    {
        _traBullet.position = Vector3.zero;
        _traBullet.rotation = Quaternion.identity;
        _rdBullet.Sleep();
    }


    private IEnumerator CorBullectActive()
    {
        yield return new WaitForSeconds(5.0f);
        gameObject.SetActive(false);
    }
}
