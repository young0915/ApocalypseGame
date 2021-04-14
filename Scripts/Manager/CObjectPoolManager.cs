using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[System.Serializable]
public class CObjectInfo
{
    public EmObjectPoolType m_eObjectPoolType;                      // 오브젝트 타입.
    public int m_nAmount = 0;                                               // 오븍제특 타입 수.
    public GameObject m_objPrefab;                                       // 프리팹.
    public GameObject m_objPos;                                          // 생성된 Obj소속 우치.
    [SerializeField] private Vector3 _VecPos;                              // ObjectPool Manager를 사용할 위치(영역 범위).
    [HideInInspector] public Vector3 m_VecPos { get { return _VecPos; } }

    [HideInInspector] public List<GameObject> m_Poollist = new List<GameObject>();
}


public class CObjectPoolManager : CSingleton<CObjectPoolManager>
{
    [SerializeField] private List<CObjectInfo> ins_ObjectPoollist = new List<CObjectInfo>();

    public void Install()
    {
        for (int i = 0; i < ins_ObjectPoollist.Count; i++)
        {
            CreateObject(ins_ObjectPoollist[i]);
        }
    }

    public void CreateObject(CObjectInfo cInfo)
    {
        GameObject obj = null;

        for (int i = 0; i < cInfo.m_nAmount; i++)
        {
            obj = Instantiate(cInfo.m_objPrefab, cInfo.m_objPos.transform);
            obj.name = cInfo.m_objPrefab.name + i.ToString("00");
            obj.SetActive(false);

            cInfo.m_Poollist.Add(obj);
        }

        if (cInfo.m_eObjectPoolType == EmObjectPoolType.ItemBox || cInfo.m_eObjectPoolType == EmObjectPoolType.Obstacle)
        {
            StartCoroutine(CorSpawn());
        }
    }

    private IEnumerator CorSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);

            for (int j = 0; j < ins_ObjectPoollist.Count; j++)
            {
                if (ins_ObjectPoollist[j].m_eObjectPoolType == EmObjectPoolType.ItemBox || ins_ObjectPoollist[j].m_eObjectPoolType == EmObjectPoolType.Obstacle)
                {

                    for (int i = 0; i < ins_ObjectPoollist[j].m_Poollist.Count; i++)
                    {
                        if (ins_ObjectPoollist[j].m_Poollist[i].activeSelf == true)
                            continue;

                        float fPosX = ins_ObjectPoollist[j].m_Poollist[i].transform.position.x;
                        float fPosZ = ins_ObjectPoollist[j].m_Poollist[i].transform.position.z;

                        ins_ObjectPoollist[j].m_Poollist[i].transform.position = new Vector3(
                            Random.Range(CDataManager.Inst.m_nMinPosX, (fPosX + ins_ObjectPoollist[j].m_VecPos.x)),
                            ins_ObjectPoollist[j].m_VecPos.y,
                            Random.Range(CDataManager.Inst.m_nMinPosZ, (fPosZ + ins_ObjectPoollist[j].m_VecPos.z))
                            );

                        ins_ObjectPoollist[j].m_Poollist[i].SetActive(true);

                        if (ins_ObjectPoollist[j].m_eObjectPoolType == EmObjectPoolType.Obstacle || ins_ObjectPoollist[j].m_eObjectPoolType == EmObjectPoolType.Fog)
                        {
                            StartCoroutine(GetRandomObstacle(ins_ObjectPoollist[j]));
                        }
                        break;
                    }
                }
            }
        }
    }

    public GameObject GetBullet(int num)
    {
            for (int j = 0; j < ins_ObjectPoollist[num].m_Poollist.Count; j++)
            {
                if (ins_ObjectPoollist[num].m_Poollist[j].activeSelf == false)
                {
                    return ins_ObjectPoollist[num].m_Poollist[j];
                }
            }
        return null;
    }

    // Fog, Obstacle 전용 코루틴 함수.
    private IEnumerator GetRandomObstacle(CObjectInfo cObjectInfo)
    {
        if (cObjectInfo.m_eObjectPoolType == EmObjectPoolType.Fog)
        {
            yield return new WaitForSeconds(10.0f);

        }
        yield return new WaitForSeconds(5.0f);
        int nObject = Random.Range(0, cObjectInfo.m_nAmount);
        for (int i = 0; i < cObjectInfo.m_Poollist.Count; i++)
        {
            if (cObjectInfo.m_Poollist[i].activeSelf == true)
            {
                cObjectInfo.m_Poollist[nObject].SetActive(false);
                yield break;
            }
            yield break;
        }
    }


    public void GetObjcectActive(bool bIsOn)
    {
        CObjectPoolManager.Inst.gameObject.SetActive(bIsOn);
    }


}
