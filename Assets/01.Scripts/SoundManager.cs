using Unity.VectorGraphics.Editor;
using UnityEngine;


public enum SFXType
{
    Skill,
    Heal,
    Attack,
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
    }

    private void Start()
    {
        
    }

    public void PlaySFX(SFXType type)
    {
        if ((int)type > soundClip.Length)
        {
            return;
        }

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
