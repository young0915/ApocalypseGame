using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBombCtrl : MonoBehaviour
{
    [SerializeField] private ParticleSystem ins_NukeLight;
    private float _delay = 3.0f;

    private float _explosionForce = 10.0f;
    private float _radius = 2f;


    private void Start()
    {
         Launch();
    }

    private void Launch()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 3.0f, ForceMode.Impulse);
        Explode();
    }

    // 포물선을 이용해서 잘 던지기.
    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

        foreach(Collider near in colliders)
        {
            Rigidbody rig = near.GetComponent<Rigidbody>();

            if(rig != null)
            rig.AddExplosionForce(_explosionForce, transform.position, _radius, 1f, ForceMode.Impulse);
        }

        StartCoroutine(CorExplosionEvent());
    }

    private IEnumerator CorExplosionEvent()
    {
        gameObject.transform.localRotation = Quaternion.identity;
        yield return new WaitForSeconds(5.0f);
        ins_NukeLight.transform.localRotation = Quaternion.identity;
        ins_NukeLight.gameObject.SetActive(true);
        ins_NukeLight.Play();
        yield return new WaitForSeconds(5.0f);
        gameObject.SetActive(false);
        ins_NukeLight.gameObject.SetActive(false);

    }

}
