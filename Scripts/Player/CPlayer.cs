using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CPlayer : MonoBehaviour
{
    public CPlayerMoveCtrl m_cPlyaerMoveCtrl { get; private set; } = null;

    public CPlayerAnimation m_cPlayerAnimation { get; private set; } = null;

    public Camera m_CamMap { get; private set; } = null;

    public Rigidbody m_Rigidbody { get; private set; } = null;

    private const string _strProfilePath = "Prefab/Player/PlayerProfile";

    private const string _strMapPath = "Prefab/Player/MapRenderer";

    private const string _strPlayer = "Player";



    public static CPlayer GetPlayerInstance(Transform TransParent, Vector3 VecPos, Quaternion quaternion, bool bIsCtrl, string strCharacterPath)
    {
        GameObject objPlayer = new GameObject("CPlayer");
        objPlayer.layer = 11;

        CPlayer cPlayer = objPlayer.AddComponent<CPlayer>();
        cPlayer.tag = _strPlayer;

        // 카메라 타겟 포지션 설정.
        CCameraManager.Inst.m_traTarget = cPlayer.gameObject.transform;
        CCameraManager.Inst.SetCameraView(EmCameraType.ThirdPerson);

        // 날씨 포지션 설정.
        CWheatherManager.Inst.m_traPlayer = objPlayer.transform;


        cPlayer.InitPlayer(TransParent, VecPos, quaternion, bIsCtrl, strCharacterPath);

      
        return cPlayer;
    }

    public void InitPlayer(Transform TransParent, Vector3 VecPos, Quaternion quaternion, bool bIsCtrl, string strCharacterPath)
    {
        GameObject objprefab = CResourceLoader.Load<GameObject>(strCharacterPath);

        transform.parent = TransParent;
        transform.position = VecPos;
        transform.localRotation = quaternion;

        m_Rigidbody = gameObject.AddComponent<Rigidbody>();
        m_Rigidbody.isKinematic = true;
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        CapsuleCollider capsuleCollider = gameObject.AddComponent<CapsuleCollider>();

        capsuleCollider.center = new Vector3(0, 0.85f, 0);
        capsuleCollider.isTrigger = true;
        capsuleCollider.radius = 0.3f;
        capsuleCollider.height = 2.0f;

        if (bIsCtrl)
        {
            m_cPlyaerMoveCtrl = gameObject.AddComponent<CPlayerMoveCtrl>();
            m_cPlyaerMoveCtrl.m_cPlayer = this;
        }

        objprefab = Instantiate(objprefab);

        // 플레이어의 프리팹(캐릭터)이 생겼으면?.
        if (objprefab != null)
        {
            objprefab.transform.parent = transform;
            objprefab.transform.localPosition = Vector3.zero;
            objprefab.transform.localRotation = Quaternion.identity;
            objprefab.transform.localScale = Vector3.one;

            m_cPlayerAnimation = objprefab.GetComponent<CPlayerAnimation>();



            GameObject objCam = new GameObject("CPlayerCamera");
            Camera ProfileCam = objCam.AddComponent<Camera>();
            objCam.transform.parent = gameObject.transform;
            ProfileCam.transform.localPosition = new Vector3(0, 1.0f, 2.0f);
            ProfileCam.transform.rotation = new Quaternion(0, 180.0f, 0,0);
            ProfileCam.cullingMask = LayerMask.GetMask("Player");

            ProfileCam.targetTexture = CResourceLoader.Load<RenderTexture>(_strProfilePath);

            // Map Camera.
            GameObject MapCam = new GameObject("MapCamera");
            m_CamMap = MapCam.AddComponent<Camera>();
            MapCam.transform.parent = gameObject.transform;
            m_CamMap.transform.localPosition = new Vector3(0, 10, 0);
            m_CamMap.transform.eulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
            m_CamMap.targetTexture = CResourceLoader.Load<RenderTexture>(_strMapPath);
            m_CamMap.cullingMask = 1;
            m_CamMap.cullingMask = 2;
            m_CamMap.cullingMask = 11;

        }

    }


}
