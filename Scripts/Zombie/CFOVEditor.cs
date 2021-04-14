using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CZombieFOV))]
public class CFOVEditor : Editor
{
    private void OnSceneGUI()
    {
        //  ZombieFOV 클래스를 참조.
        CZombieFOV fov = (CZombieFOV)target;

        // 원주 위의 시작점의 좌표를 계산(주어진 각도의 1/2).
        Vector3 fromAnglePos = fov.CirclePoint(-fov.m_ViewAngle * 0.5f);

        // 원의 생상을 흰색으로 지정.
        Handles.color = new Color(1, 1, 1, 0.2f);

        // 외곽선만 표현하는 원반을 그림.
        Handles.DrawWireDisc(fov.transform.position     // 원점의 좌표.
           , Vector3.up // 노멀 벡터
           , fov.m_ViewRange // 원의 반지름.
           );


        // 부채꼴의 색상을 지정.
        Handles.DrawSolidArc(fov.transform.position // 원점 좌표.
 , Vector3.up // 노멀 벡터.
 , fromAnglePos // 부채꼴의 시작 좌표.
 , fov.m_ViewAngle // 부채꼴의 각도
 , fov.m_ViewRange);            // 부채꼴의 반직름.


        // 시야각의 텍스트를 표시.
        Handles.Label(fov.transform.position + (fov.transform.forward * 2.0f)
            , fov.m_ViewAngle.ToString());


    }
}
