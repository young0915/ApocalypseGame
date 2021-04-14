using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUIPlayerStats : MonoBehaviour
{
    [SerializeField] private Material ins_MatRender;
    [SerializeField] private Texture2D ins_RadnerTexture2D;
    [SerializeField] private CanvasRenderer _raderMeshCanvasRenderer;

    private CUIStats _cUIStats;

    public void SetStats(CUIStats Stats)
    {
        this._cUIStats = Stats;
        Stats.m_OnStatsChanged += StatOnStatsChange;
        UpdateStatsVisual();
    }

    private void StatOnStatsChange(object objSender, System.EventArgs e)
    {
        UpdateStatsVisual();
    }

    const float SixRad = 360;
    private void UpdateStatsVisual()
    {
        Mesh mesh = new Mesh();

        Vector3[] Vertices = new Vector3[6];
        Vector2[] Uv = new Vector2[6];

        int[] Triangles = new int[3 * 5];

        float AngleIncrement = SixRad / 5;
        float RadarChartSize = 145f;                                // 라디안 크기.


        Vector3 SpeedVertex = Quaternion.Euler(0, 0, -AngleIncrement * 0) * Vector3.up *
            RadarChartSize * _cUIStats.GetStatsAmountNormalized(EmStatsType.Speed);
        int nSpeedVertexIdx = 1;

        Vector3 AttackVertex = Quaternion.Euler(0, 0, -AngleIncrement * 1) * Vector3.up *
            RadarChartSize * _cUIStats.GetStatsAmountNormalized(EmStatsType.Attack);
        int nAttackVertexIdx = 2;


        Vector3 DefensiveVertex = Quaternion.Euler(0, 0, -AngleIncrement * 2) * Vector3.up *
            RadarChartSize * _cUIStats.GetStatsAmountNormalized(EmStatsType.Defense);
        int nDefensiveVertexIdx = 3;


        Vector3 RecoveryVertex = Quaternion.Euler(0, 0, -AngleIncrement * 3) * Vector3.up *
            RadarChartSize * _cUIStats.GetStatsAmountNormalized(EmStatsType.Recovery);
        int nRecoveryVertexIdx = 4;


        Vector3 CriticalHitVertex = Quaternion.Euler(0, 0, -AngleIncrement * 4) * Vector3.up *
            RadarChartSize * _cUIStats.GetStatsAmountNormalized(EmStatsType.CriticalHit);
        int nCriticalHitVertexIdx = 5;



        Vertices[0] = Vector3.zero;
        Vertices[nSpeedVertexIdx] = SpeedVertex;
        Vertices[nAttackVertexIdx] = AttackVertex;
        Vertices[nDefensiveVertexIdx] = DefensiveVertex;
        Vertices[nRecoveryVertexIdx] = RecoveryVertex;
        Vertices[nCriticalHitVertexIdx] = CriticalHitVertex;


        Uv[0] = Vector2.zero;
        Uv[nSpeedVertexIdx] = Vector2.one;
        Uv[nAttackVertexIdx] = Vector2.one;
        Uv[nDefensiveVertexIdx] = Vector2.one;
        Uv[nRecoveryVertexIdx] = Vector2.one; 
        Uv[nCriticalHitVertexIdx] = Vector2.one;


        Triangles[0] = 0;
        Triangles[1] = nSpeedVertexIdx;
        Triangles[2] = nAttackVertexIdx;

        Triangles[3] = 0;
        Triangles[4] = nAttackVertexIdx;
        Triangles[5] = nDefensiveVertexIdx;

        Triangles[6] = 0;
        Triangles[7] = nDefensiveVertexIdx;
        Triangles[8] = nRecoveryVertexIdx;

        Triangles[9] = 0;
        Triangles[10] = nRecoveryVertexIdx;
        Triangles[11] = nCriticalHitVertexIdx;

        Triangles[12] = 0;
        Triangles[13] = nCriticalHitVertexIdx;
        Triangles[14]= nSpeedVertexIdx;


        mesh.vertices = Vertices;
        mesh.uv = Uv;
        mesh.triangles = Triangles;

        _raderMeshCanvasRenderer.SetMesh(mesh);
        _raderMeshCanvasRenderer.SetMaterial(ins_MatRender, ins_RadnerTexture2D);





    }

}
