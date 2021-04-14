using System;
using UnityEngine;

public class CUIStats 
{
    [HideInInspector] public EventHandler m_OnStatsChanged;

    [HideInInspector] public static int _nStatsMin = 0;
    [HideInInspector] public static int _nStatsMax = 20;

    private CSingeStats _cSpeedStat;
    private CSingeStats _cAttckStat;
    private CSingeStats _cDefenseStat;
    private CSingeStats _cRecoveryStat;
    private CSingeStats _cCriticalHitStat;

    public CUIStats(int nSpeedStatAmount, int nAttackStatAmount, int nDefenseStatAmount,
        int nRecoveryStatAmount, int nCriticalHitStatAmount)
    {
        _cSpeedStat = new CSingeStats(nSpeedStatAmount);
        _cAttckStat = new CSingeStats(nAttackStatAmount);
        _cDefenseStat = new CSingeStats(nDefenseStatAmount);
        _cRecoveryStat = new CSingeStats(nRecoveryStatAmount);
        _cCriticalHitStat = new CSingeStats(nCriticalHitStatAmount);
    }

    private CSingeStats GetSingleStat(EmStatsType eStatType)
    {
        switch(eStatType)
        {
            default:

            case EmStatsType.Speed:
                return _cSpeedStat;

            case EmStatsType.Attack:
                return _cAttckStat;

            case EmStatsType.Defense:
                return _cDefenseStat;

            case EmStatsType.Recovery:
                return _cRecoveryStat;

            case EmStatsType.CriticalHit:
                return _cCriticalHitStat;

        }
    }


    public void SetStatAmount(EmStatsType eStatsType, int nStatAmount)
    {
        GetSingleStat(eStatsType).SetStatAmount(nStatAmount);
        if (m_OnStatsChanged != null) m_OnStatsChanged(this, EventArgs.Empty);
    }

    public void InCreaseStatAmount(EmStatsType eStatsType)
    {
        SetStatAmount(eStatsType, GetStatAmount(eStatsType) - 1);
    }

    public int GetStatAmount(EmStatsType eStatsType)
    {
        return GetSingleStat(eStatsType).GetStatAmount();
    }

    public float GetStatsAmountNormalized(EmStatsType eStatType)
    {
        return GetSingleStat(eStatType).GetStatAmountNormalized();
    }

    // 한 스탯당 표시할 클래스.
    private class CSingeStats
    {
        private int _nStat;

        public CSingeStats(int nStatAmount)
        {
            SetStatAmount(nStatAmount);
        }

        public void SetStatAmount(int nStatAmount)
        {
            _nStat = Mathf.Clamp(nStatAmount, _nStatsMin, _nStatsMax);
        }

        public int GetStatAmount()
        {
            return _nStat;
        }

        public float GetStatAmountNormalized()
        {
            return (float)_nStat / _nStatsMax;
        }
    }

}
