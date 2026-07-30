using System.Collections;
using System.ComponentModel;
using TMPro;
using UnityEngine;

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

    private void Awake()
    {

        savedMaxHp = 100;
        savedNowHp = 100;
        savedMaxMp = 100;
        savedNowMp = 100;
        savedExp = 0;
        savedDamage = 10;

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
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

    public IEnumerator ConsoleText(string str)
    {
        console.text = str;
        console.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        console.gameObject.SetActive(false);
    }
}
