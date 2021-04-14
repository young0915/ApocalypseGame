
using UnityEngine;

public class CQuest : MonoBehaviour
{


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CUIManager.Inst.m_cUIQuest != null)
            {
                CUIManager.Inst.m_cUIQuest.OnRemoveQuest(0);
                return;
            }
        }
    }

}
