using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CCameraManager : CSingleton<CCameraManager>
{
    [SerializeField] private Camera ins_Cam;
    [SerializeField] private MeshRenderer ins_BgMesh;                                                                            // 벙커 배경화면 길이.
    [HideInInspector] public Transform m_traTarget = null;                                                                      // 플레이어의 방향.
    private EmCameraType _eCameraType;                                                                                           // 카메라 타입.

    private Vector3 _dragOrigin;

    private float _fdist = 4f;
    private float _fRotation = 1f;

    private float _fMouseX = 0.0f;
    private float _fMouseY = 0.0f;

    private float BunkerMinPosX, BunkerMaxPosX, BunkerMinPosY, BunkerMaxPosY;
    private float _fPosX = 0.0f;
    private float _fPosY = 0.0f;
    private float _fPosZ = 0.0f;
    private float _fRotX = 0.0f;
    private float _fRotY = 0.0f;
    private float _fRotZ = 0.0f;


    private bool _bIsBunker = false;
    private bool _bIsThirdPerson = false;
    private bool _bIsFirstPerson = false;
    private bool _bIsFirstOrThrid = false;                                                                                           // 삼인칭인가 일인칭인가 판단하는 함수.

    private Transform _tr;

    #region [code] const 변수들.
    // Zoom  Max, Min  변수.
    private const float _fZoomMax = 20.0f;
    private const float _fZoomMin = 5.0f;



    // Openning 변수.
    private const float _fOpeningPosX = 1.8f;
    private const float _fOpeningPosY = 63.085f;
    private const float _fOpeningPosZ = 163.2f;
    private const float _fOpeningRotX = 30.977f;
    private const float _fOpeningRotY = 146.8f;
    private const float _fOpeningRotZ = 0.0f;

    // 벙커씬 변수.
    private const float _fBunkerPosX = -3.89f;
    private const float _fBunkerPosY = -2.5f;
    private const float _fBunkerPosZ = 20.0f;
    private const float _fBunkerRotX = 0.0f;
    private const float _fBunkerRotY = 180f;
    private const float _fBunkerRotZ = -0.0f;


    // 3인칭 시점.
    private const float _fMinY = 20.0f;
    private const float _fMaxY = 80.0f;


    // Wall Obstacle Setting.
    private float _fHeightAboveWall = 7.0f;                            // 카메라가 올라갈 높이.
    private float _fColliderRadius = 1.8f;                                // 충돌체의 반지름.
    private float _fOverDamping = 5.0f;                                 // 이동속도 계수.
    private float _fOriginHeight;                                           // 최초 높이를 보관할 변수;


    // Etc Obstacle Setting.
    // 카메라 올라갈 높이.
    private float _fheightAboveObstacle = 12.0f;
    private float _fCastOffset = 1.0f;

    private float _fmoveDamping = 20.0f;
    private float _frotateDamping = 15f;
    private float _fdistance = 5.0f;
    private float _ftargetOffset = 2.0f;

    private float _fheight = 4.0f;





    #endregion


    public void Initialization()
    {
        BunkerMinPosX = ins_BgMesh.transform.position.x - ins_BgMesh.bounds.size.x / 6.0f;
        BunkerMaxPosX = ins_BgMesh.transform.position.x + ins_BgMesh.bounds.size.x / 6.0f;

        BunkerMinPosY = ins_BgMesh.transform.position.y - ins_BgMesh.bounds.size.y / 6.0f;
        BunkerMaxPosY = ins_BgMesh.transform.position.y + ins_BgMesh.bounds.size.y / 6.0f;

        _tr = GetComponent<Transform>();
        _fOriginHeight = _fheight;
    }

    private void FixedUpdate()
    {
        if (_bIsBunker == true)
        {
            SetBunkerCameraMove();
        }
        if (_bIsFirstPerson == true)
        {
            SetCameraFristPerson();
        }

        if (_bIsThirdPerson == true)
        {

            SetCameraThirdPerson();

            // 구체 형태의 충돌 체크로 충돌 여부 검사.
            //if (Physics.CheckSphere(gameObject.transform.position, _fColliderRadius))
            //{
            //    // 보간 함수를 사용하여 카메라의 높이를 부드럽게 상승시킴.
            //    _fheight = Mathf.Lerp(_fheight,
            //        _fHeightAboveWall,
            //        Time.deltaTime * _fOverDamping);
            //}
            //else
            //{
            //    _fheight = Mathf.Lerp(_fheight,
            //        _fOriginHeight,
            //        Time.deltaTime * _fOverDamping);
            //}
            //Vector3 castTarget = m_traTarget.position + (m_traTarget.up * _fCastOffset);

            //// castTarget 좌표로의 방향 벡터를 계산.
            //Vector3 castDir = (castTarget - _tr.position).normalized;

            //RaycastHit hit;

            //// 레이캐스트를 투사해 장애물 여부를 검사.
            //if (Physics.Raycast(_tr.position, castDir, out hit, Mathf.Infinity))
            //{
            //    // 주인공이 레이캐스텡 맞지 않았을 경우.
            //    if (!hit.collider.CompareTag("Player"))
            //    {
            //        _fheight = Mathf.Lerp(_fheight,
            //            _fheightAboveObstacle,
            //            Time.deltaTime * _fOverDamping);
            //    }
            //}
        }

    }



    //// 거리의 최소, 최대 제한.
    public void SetCameraView(EmCameraType eCameraType)
    {
        _bIsBunker = false;
        //_bIsZoom = false;
        _bIsThirdPerson = false;
        _bIsFirstPerson = false;
        switch (eCameraType)
        {
            case EmCameraType.Openning:
                // 만약 이상하면 밑에 있는 주석 풀 것.
                // 시점.
                _fPosX = _fOpeningPosX;
                _fPosY = _fOpeningPosY;
                _fPosZ = _fOpeningPosZ;

                //각도.
                _fRotX = _fOpeningRotX;
                _fRotY = _fOpeningRotY;
                _fRotZ = _fOpeningRotZ;

                break;

            case EmCameraType.Bunker:
                _fPosX = _fBunkerPosX;
                _fPosY = _fBunkerPosY;
                _fPosZ = _fBunkerPosZ;

                _fRotX = _fBunkerRotX;
                _fRotY = _fBunkerRotY;
                _fRotZ = _fBunkerRotZ;
                _bIsBunker = true;
                //_bIsZoom = true;

                break;

            case EmCameraType.FirstPerson:
                _bIsFirstPerson = true;

                break;

            case EmCameraType.ThirdPerson:
                _bIsThirdPerson = true;

                break;
        }
        ins_Cam.transform.position = new Vector3(_fPosX, _fPosY, _fPosZ);
        ins_Cam.transform.rotation = Quaternion.Euler(_fRotX, _fRotY, _fRotZ);

    }


    #region [code] Bunker씬 (NPC 모여 있는곳.)
    // 벙커일때 이동할 수 있는 카메라 전환.
    private void SetBunkerCameraMove()
    {
        if (Input.GetMouseButtonDown(0))
            _dragOrigin = ins_Cam.ScreenToViewportPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            Vector3 Direction = _dragOrigin - ins_Cam.ScreenToViewportPoint(Input.mousePosition);
        }
    }





    #region [code] NpcPosition 변수들.
    const float _fBunkerRewardNpcPosX = -0.3f;
    const float _fBunkerRewardNpcPosY = 1.74f;
    const float _fBunkerRewardNpcPosZ = 4.0f;

    const float _fBunkerStoreNpcPosX = -14.5f;
    const float _fBunkerStoreNpcPosY = -4.2f;
    const float _fBunkerStoreNpcPosZ = 4.5f;
    #endregion
    public void SetNpcInteraction(EmNpcType eNpcType)
    {
        _bIsBunker = false;
        switch (eNpcType)
        {
            case EmNpcType.Store:
                _fPosX = _fBunkerStoreNpcPosX;
                _fPosY = _fBunkerStoreNpcPosY;
                _fPosZ = _fBunkerStoreNpcPosZ;
                break;

            case EmNpcType.Reward:
                _fPosX = _fBunkerRewardNpcPosX;
                _fPosY = _fBunkerRewardNpcPosY;
                _fPosZ = _fBunkerRewardNpcPosZ;

                break;

        }
        ins_Cam.transform.position = new Vector3(_fPosX, _fPosY, _fPosZ);
    }

    private Vector3 ClampCamera(Vector3 targetPostion)
    {
        float CamHeight = ins_Cam.orthographicSize;
        float CamWidth = ins_Cam.orthographicSize * ins_Cam.aspect;

        float fMinX = BunkerMinPosX + CamWidth;
        float fMaxX = BunkerMaxPosX - CamWidth;

        float fMinY = BunkerMinPosY + CamHeight;
        float fMaxY = BunkerMaxPosY - CamHeight;

        float newX = Mathf.Clamp(targetPostion.x, fMinX, fMaxX);
        float newY = Mathf.Clamp(targetPostion.y, fMinY, fMaxY);

        return new Vector3(newX, newY, targetPostion.z);
    }


    #endregion

    #region [code] EmCameraType.FirstPerson 함수들.



    private void SetCameraFristPerson()
    {
        if (m_traTarget)
        {

            float fMouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * 90;
            float fMouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * 90;

            _fRotX -= fMouseX;
            _fRotX = ClampAngle(_fRotX, -90.0f, 90f);

            transform.localEulerAngles = new Vector3(0f, _fRotX, 0.0f);

            // 포지션.
           transform.position = new Vector3(m_traTarget.position.x, m_traTarget.position.y + 1.5f, m_traTarget.position.z);

            m_traTarget.rotation = Quaternion.Euler(0, _fRotX, 0);
        }
    }


    #endregion


    #region [code]  EmCameraType.ThirdPerson 함수들.

    private IEnumerator CorCameraThirdStart()
    {
        yield return new WaitForSeconds(0.2f);
        Vector3 angles = transform.eulerAngles;

        _fMouseX = angles.y;
        _fMouseY = angles.x;

        _bIsThirdPerson = true;
      //  _bIsZoom = true;
    }

    private void SetCameraThirdPerson()
    {
        if (m_traTarget)
        {

            _fdist -= 1 * Input.mouseScrollDelta.y;

            _fMouseX += Input.GetAxis("Mouse X") * _fRotation;
            _fMouseY -= Input.GetAxis("Mouse Y") * _fRotation;

            _fMouseY = ClampAngle(_fMouseY, _fMinY, _fMaxY);
            _fdist = ClampAngle(_fdist, _fZoomMin, _fZoomMax);


            Quaternion rotation = Quaternion.Euler(_fMouseY, _fMouseX, 0);
            Vector3 postion = rotation * new Vector3(0, 0.0f, -_fdist) + m_traTarget.position + new Vector3(0.0f, 0, 0.0f);

            transform.rotation = rotation;
            transform.position = postion;
        }

    }


    //private void SetCameraThirdPerson()
    //{
    //    if (m_traTarget)
    //    {

    //        var camPos = m_traTarget.position - (m_traTarget.forward * _fdistance) + (m_traTarget.up * _fheight);

    //        _tr.position = Vector3.Slerp(_tr.position, camPos, Time.deltaTime * _fmoveDamping);
    //        _tr.rotation = Quaternion.Slerp(_tr.rotation, m_traTarget.rotation, Time.deltaTime * _frotateDamping);
    //        _tr.LookAt(m_traTarget.position + (m_traTarget.up * _ftargetOffset));

    //    }
    //}
    #endregion



    private float ClampAngle(float fAngle, float fMin, float fMax)
    {
        // 3인칭 시점이면.
        if (_bIsThirdPerson == true)
        {
            if (fAngle < 360)
                fAngle += 360;
            if (fAngle > 360)
                fAngle -= 360;
        }

        return Mathf.Clamp(fAngle, fMin, fMax);
    }
}