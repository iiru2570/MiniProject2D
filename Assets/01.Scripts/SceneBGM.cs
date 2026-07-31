using UnityEngine;

public class SceneBGM : MonoBehaviour
{
    public AudioClip bgmClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SoundManager.instance.PlayBGM(bgmClip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
