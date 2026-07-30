using Unity.VectorGraphics.Editor;
using UnityEngine;


public enum SFXType
{
    Attack,
    Skill1,
    Skill2,
    Skill3,
    Denided,
    Upgrade,
    Portal,
    Open,
    Close,
    BossPattern1,
    BossPattern2,
    Bomb,
    RangedAttack
}


public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] AudioSource BGMSource;
    [SerializeField] AudioSource SFXSource;
    public AudioClip audioClip; // 배경음 
    public AudioClip[] soundClip; // 효과음

    private float[] lastPlayTime;
    private float minInterval = 0.05f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        lastPlayTime = new float[System.Enum.GetValues(typeof(SFXType)).Length];
    }

    private void Start()
    {
        
    }

    public void PlaySFX(SFXType type)
    {
        int index = (int)type;
        if ((int)type > soundClip.Length)
        {
            return;
        }

        if (Time.time - lastPlayTime[index] < minInterval)
        {
            return;
        }
        lastPlayTime[index] = Time.time;
        SFXSource.PlayOneShot(soundClip[(int)type]);
    }


    public void SetBgmVolume(float volume)
    {
        BGMSource.volume = volume;
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }
    public void SetSfxVolume(float volume)
    {
        SFXSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
    public float GetBgmVolume()
    {
        return BGMSource.volume;
    }
    public float GetSfxVolume()
    {
        return SFXSource.volume;
    }
}
