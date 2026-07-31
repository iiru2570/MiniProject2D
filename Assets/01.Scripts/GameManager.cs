using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public int savedMaxHp;
    public int savedNowHp;
    public int savedMaxMp;
    public int savedNowMp;
    public int savedExp;
    public int savedDamage;

    public TextMeshProUGUI console;
    public TextMeshProUGUI gametime;
    [SerializeField] private GameObject optionPanel;
    public Button optionBtn;
    private Coroutine consoleCo;

    private float currentTime;
    private bool isTimer;

    private void Awake()
    {
        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
        savedMaxHp = 100;
        savedNowHp = 100;
        savedMaxMp = 100;
        savedNowMp = 100;
        savedExp = 0;
        savedDamage = 10;

        currentTime = 0f;
        isTimer = false;

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
        optionBtn.onClick.AddListener(OpenOption);
        
    }
    private void Update()
    {
        if (isTimer)
        {
            currentTime += Time.deltaTime;
            gametime.text = GetTime();
        }
    }

    public void SaveStat(PlayerStat stat)
    {
        savedMaxHp = stat.MaxHp;
        savedNowHp = stat.NowHp;
        savedMaxMp = stat.MaxMp;
        savedNowMp = stat.NowMp;
        savedExp = stat.Exp;
        savedDamage = stat.Damage;
    }

    public void LoadStat(PlayerStat stat)
    {
        stat.MaxHp = savedMaxHp;
        stat.NowHp = savedNowHp;
        stat.MaxMp = savedMaxMp;
        stat.NowMp = savedNowMp;
        stat.Exp = savedExp;
        stat.Damage = savedDamage;
    }
    public void ResetStat()
    {
        savedMaxHp = 100;
        savedNowHp = 100;
        savedMaxMp = 100;
        savedNowMp = 100;
        savedExp = 0;
        savedDamage = 10;
    }


    public void ShowConsoleText(string str)
    {
        if (consoleCo != null)
        {
            StopCoroutine(consoleCo);
        }
        console.DOKill();
        consoleCo = StartCoroutine(ConsoleText(str));
    }

    public IEnumerator ConsoleText(string str)
    {
        console.text = str;
        console.gameObject.SetActive(true);
        Color color = console.color;
        color.a = 1f;
        console.color = color;

        console.DOFade(0f, 1f);
        yield return new WaitForSeconds(1f);
        console.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        ResetStat();
        StopTimer();
        gametime.text = "";
        SceneChanger.instance.LoadScene(0);
    }


    public void OpenOption()
    {
        SoundManager.instance.PlaySFX(SFXType.Open);
        optionPanel.SetActive(true);
    }

    public void StartTimer()
    {
        currentTime = 0f;
        isTimer = true;
    }
    public void StopTimer()
    {
        isTimer = false;
    }
    public string GetTime()
    {
        int minute = Mathf.FloorToInt(currentTime / 60f);
        int second = Mathf.FloorToInt(currentTime % 60f);

        return $"{minute:00} : {second:00}";
    }

}
