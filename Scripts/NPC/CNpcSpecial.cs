using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CNpcSpecial : MonoBehaviour
{
    private bool _bIsAction = false;

    [SerializeField] private Animator ins_AniNpc;


    private void Update()
    {
        if(_bIsAction)
        {
      
            gameObject.transform.localRotation = Quaternion.Euler(0, 230.0f, 0);
            ins_AniNpc.Play("Unarmed-Run-Forward");
        }
        else
        {
            ins_AniNpc.Play("Standing Greeting");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           // 회전하면서 돌아간다.
            _bIsAction = true;
            StartCoroutine(CorRunAction());
            return;
        }
    }

    private IEnumerator CorRunAction()
    {
        gameObject.transform.position = new Vector3(-40.42735f, 0.0f, -102.1566f);
        CUIManager.Inst.m_cUIQuest.OnAddQuest(1);
        yield return new WaitForSeconds(7.0f);

        gameObject.SetActive(false);
        yield return null;
    }



}
