using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

public class CSoundManager : CSingleton<CSoundManager>
{

    [SerializeField] private Dictionary<int, CSoundInfo> _dicSound = new Dictionary<int, CSoundInfo>();
    [SerializeField] private AudioSource ins_AudioSource = null;
    public AudioSource m_AudioSource { get { return ins_AudioSource; } }

    public void Install()
    {
        string Path = "Texts/SoundData";
        var Json = CResourceLoader.Load<TextAsset>(Path).text;
        var ArrSound = JsonConvert.DeserializeObject<CSoundInfo[]>(Json);

        for (int i =0; i<ArrSound.Length; i++)
        {
            _dicSound.Add(i, ArrSound[i]);
        }
    
    }

   public void SetSoundPlay(int num, float volume=1.0f, bool bIsOn = false)
    {
        ins_AudioSource.clip = CResourceLoader.Load<AudioClip>(_dicSound[num].m_strSoundPath);
        ins_AudioSource.volume = volume;
        ins_AudioSource.Play();
        ins_AudioSource.loop = bIsOn;
    }

    public void SetSoundPause(int num)
    {
        ins_AudioSource.clip = CResourceLoader.Load<AudioClip>(_dicSound[num].m_strSoundPath);
        ins_AudioSource.Pause();
        if (ins_AudioSource.loop == true)
        {
            ins_AudioSource.loop = false;
        }
    }

    public void SetAttackSound(int num)
    {
        ins_AudioSource.clip = CResourceLoader.Load<AudioClip>(_dicSound[num].m_strSoundPath);
        ins_AudioSource.PlayOneShot(ins_AudioSource.clip);
    }



}

public class CSoundInfo
{
    public int m_nId;
    public string m_strSoundPath;
}