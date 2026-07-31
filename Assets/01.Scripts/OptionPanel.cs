using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Button lobbyBtn;
    public Button quitBtn;
    public Button exitBtn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bgmSlider.onValueChanged.AddListener(BGMVolumeChanged);
        sfxSlider.onValueChanged.AddListener(SFXVolumeChanged);
        lobbyBtn.onClick.AddListener(toLobby);
        quitBtn.onClick.AddListener(Quit);
        exitBtn.onClick.AddListener(ExitBtnClick);
    }

    public void ExitBtnClick()
    {
        SoundManager.instance.PlaySFX(SFXType.Close);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        bgmSlider.value = SoundManager.instance.GetBgmVolume();
        sfxSlider.value = SoundManager.instance.GetSfxVolume();
    }
    public void BGMVolumeChanged(float vol)
    {
        SoundManager.instance.SetBgmVolume(vol);
    }

    public void SFXVolumeChanged(float vol)
    {
        SoundManager.instance.SetSfxVolume(vol);
    }


    public void toLobby()
    {
        SceneChanger.instance.LoadScene(0);
        GameManager.instance.StopTimer();
        GameManager.instance.gametime.text = "";
        gameObject.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
