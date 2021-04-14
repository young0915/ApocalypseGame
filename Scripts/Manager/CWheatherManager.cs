using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MainScene에서만 사용할텐데 Mono로 바꿔야할 것 같음 상황 보고 수정 ㄱㄱ.
public class CWheatherManager : CSingleton<CWheatherManager>
{
    [SerializeField] private Light ins_DirectionalLight = null;
    [SerializeField] private List<ParticleSystem> ins_RainLayer = new List<ParticleSystem>();
    [HideInInspector] public Transform m_traPlayer = null;                                                                                         // 이걸로 이용해서 플레이 위치와 ins_WheatherLayer 포지션 일치하게 하기.

    // 밤 낮을 알아주는 시간 단위.
    [SerializeField, Range(0, 24)] private float ins_fTimeOfDay;

    // 프리팹 경로.
    private const string _strRainParticleSystemPath = "Prefab/FX/FX_Rain_01";
    private const string _strRainSplashParticleSystemPath = "Prefab/FX/FX_Rain_SplashLayer_01";

    private const float _fTwoRadian = 360;
    private const float _fVecX = 90f;
    private const float _fVecY = 130f;
    private const float _fVecZ = -25f;
    private const float _fHour = 24f;
    // Update is called once per frame

    private bool _bIsRain = false;

    void Update()
    {

        if (Application.isPlaying)
        {
            ins_fTimeOfDay += Time.deltaTime / 15;
            ins_fTimeOfDay %= _fHour;
            SetLighting(ins_fTimeOfDay / _fHour);
        }
        else
        {
            SetLighting(ins_fTimeOfDay / _fHour);
        }

        if (_bIsRain)
        {
            SetRaining(_bIsRain);
        }

    }



    private void SetLighting(float fTimePercent)
    {

        if (ins_DirectionalLight != null)
        {
            ins_DirectionalLight.transform.localRotation
                = Quaternion.Euler(new Vector3((fTimePercent * _fTwoRadian) - _fVecX, _fVecY, _fVecZ));
        }
    }


    private void OnValidate()
    {
        if (ins_DirectionalLight != null)
        {
            return;
        }

        if (RenderSettings.sun != null)
        {
            ins_DirectionalLight = RenderSettings.sun;
        }
    }

    public void SetTurnOnOrOff(bool bIsOn)
    {
        ins_DirectionalLight.gameObject.SetActive(bIsOn);
    }

    /// <summary>
    /// 비가 내리는 지 또는 해가 뜨는지 랜덤으로 출력되는 함수.
    /// </summary>
    public void SetRandomWeather()
    {
        int nRanWeather = Random.Range(0, 2);
        Debug.Log(" CWheatherManager  호출 : SetRandomWeather의 nRanWeather  : " + nRanWeather);
        SetWeatherType(nRanWeather);
    }

    private void SetWeatherType(int nWeather)
    {
        if (nWeather < 1)
        {
            _bIsRain = true;
        }
        else
        {
            _bIsRain = false;
        }
    }

    private void SetRaining(bool bIsRain = false)
    {
        if (m_traPlayer)
        {
            for (int i = 0; i < ins_RainLayer.Count; i++)
            {
                ins_RainLayer[i].gameObject.SetActive(bIsRain);
                ins_RainLayer[i].transform.position = m_traPlayer.transform.position;
            }

            if (bIsRain == true)
            {
                // (조건) 벙커씬이라면.
                if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Demo_City_Standard")
                {
                    bIsRain = false;
                }
                for (int i = 0; i < ins_RainLayer.Count; i++)
                {
                    ins_RainLayer[i].Play(bIsRain);
                }
            }
            else
            {
                for (int i = 0; i < ins_RainLayer.Count; i++)
                {
                    ins_RainLayer[i].Stop();
                }
            }
        }
#if Error
        else
        {
            CDebugLog.Log("플레이어의 m_traPlayer 없음. 고칠 것.");
        }
#endif
    }
}



