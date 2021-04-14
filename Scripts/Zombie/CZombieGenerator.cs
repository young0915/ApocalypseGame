using UnityEngine;
using System.Collections.Generic;

// 추상화 팩토리 사용 하는 곳.
public abstract class CZombieGenerator : MonoBehaviour
{
    private List<CZombie> _listZombie = new List<CZombie>();
    protected readonly string strPrefab = "Prefab/Zombie/";


    [HideInInspector] public List<CZombie> m_listZombie { get { return _listZombie; } }

    public abstract void CreateZombie();
}

// 1단계 출현 Easy.
public class CZobieLevelOnePatternGnerate : CZombieGenerator
{
    //CZombieNormal
    public override void CreateZombie()
    {
        m_listZombie.Add(new CZombieEasyMode(strPrefab + "Easy/CZombieEasy"));
  
        for(int i =1; i<6; i++)
        {
            m_listZombie.Add(new CZombieEasyMode(strPrefab + "Easy/CZombieEasy0" + i.ToString()));
        }

        m_listZombie.Add(new CZombieNormalMode(strPrefab + "Normal/CZombieNormal"));
        for (int i = 1; i < 10; i++)
        {
            m_listZombie.Add(new CZombieNormalMode(strPrefab + "Normal/CZombieNormal0" + i.ToString()));
        }

    }
}


// 2단계 출현 Normal.
public class CZobieLevelTwoPatternGnerate : CZombieGenerator
{
    public override void CreateZombie()
    {
        for (int i = 10; i < 13; i++)
        {
            m_listZombie.Add(new CZombieNormalMode(strPrefab + "Normal/CZombieNormal" + i.ToString()));
        }

    }
}

// 3단계 출현 Bad, Worst.
public class CZobieLevelThreePatternGnerate : CZombieGenerator
{
    public override void CreateZombie()
    {
        m_listZombie.Add(new CZombieBadMode(strPrefab + "Bad/CZombieBad"));

        for (int i = 1; i < 7; i++)
        {
            m_listZombie.Add(new CZombieBadMode(strPrefab + "Bad/CZombieBad0" + i.ToString()));
        }

        m_listZombie.Add(new CZombieWorstMode(strPrefab + "Worst/CZombieWorst"));
        for (int i = 1; i < 9; i++)
        {
            if (i < 4)
            {
                m_listZombie.Add(new CZombieWorstMode(strPrefab + "Worst/CZombieWorst0" + i.ToString()));
            }
            else
            {
                m_listZombie.Add(new CZombieWorstMode(strPrefab + "Worst/CZombieWorst0" + i.ToString(), true));
            }
        }
    }
}