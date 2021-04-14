using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CZombieFOV : MonoBehaviour
{

    // 적 캐릭터의 추적 사정 거리의 범위.
    private float _viewRange = 15.0f;
    public float m_ViewRange { get { return _viewRange; } }

    // 적 캐릭터의 시야각.
    private float _ViewAngle = 120.0f;
    public float m_ViewAngle { get { return _ViewAngle; } }

    private Transform _ZombieTr;
    public Transform m_ZombieTr { get { return _ZombieTr; } }
   private Transform _PlayerTr;
    public Transform m_PlayerTr { get { return _PlayerTr; } }
    private int _PlayerLayer;
    private int _ObstacleLayer;
    private int _layerMask;

    private void Start()
    {
        _ZombieTr = GetComponent<Transform>();
        _PlayerTr = GameObject.FindGameObjectWithTag("Player").transform;

        // 레이어 마스크 값 계산.
        _PlayerLayer = LayerMask.NameToLayer("Player");
        _ObstacleLayer = LayerMask.NameToLayer("Obstacle");
        _layerMask = 1 << _PlayerLayer | 1 << _ObstacleLayer;

    }

    // 주어진 각도에 의해 원의 점의 좌표값을 계산하는 함수.
    public Vector3 CirclePoint(float Angle)
    {
        Angle += transform.eulerAngles.y;

        return new Vector3(Mathf.Sin(Angle * Mathf.Deg2Rad), 0, Mathf.Cos(Angle * Mathf.Deg2Rad));
    }

    public bool IsTracePlayer()
    {
        bool IsTrace = false;


        Collider[] colls = Physics.OverlapSphere(_ZombieTr.position, _viewRange, 1 << _PlayerLayer);

        // 배열의 개수가 1일대 주인공이 범위 안에 있다고 판단.
        if (colls.Length == 1)
        {
            // 적 캐릭터와 주인공 사이의 방향 벡터를 계산.
            Vector3 dir = (_PlayerTr.position - _ZombieTr.position).normalized;

            // 적 캐릭터의 시야각에 들어왔는지를 판단.
            if (Vector3.Angle(_ZombieTr.forward, dir) < _ViewAngle * 0.5f)
            {
                IsTrace = true;

            }
        }

        return IsTrace;
    }

    public bool IsViewPlayer()
    {
        bool IsView = false;
        RaycastHit hit;

        // 적 캐릭터와 주인공 사이의 방향 벡터를 계산.
        Vector3 dir = (_PlayerTr.position - _ZombieTr.position).normalized;

        // 레이캐스트를 투사해서 장애물이 있는지 여부를 판단.
        if (Physics.Raycast(_ZombieTr.position, dir, out hit, _viewRange, _layerMask))
        {
            IsView = (hit.collider.CompareTag("Player"));
        }
        return IsView;
    }

    

}
