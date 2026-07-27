using System.ComponentModel;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public int savedMaxHp;
    public int savedNowHp;
    public int savedExp;
    public int savedDamage;

    private void Awake()
    {

        savedMaxHp = 100;
        savedNowHp = 100;
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
        savedExp = stat.Exp;
        savedDamage = stat.Damage;
    }

    public void LoadStat(PlayerStat stat)
    {
        stat.MaxHp = savedMaxHp;
        stat.NowHp = savedNowHp;
        stat.Exp = savedExp;
        stat.Damage = savedDamage;
    }
    public void ResetStat()
    {
        savedMaxHp = 100;
        savedNowHp = 100;
        savedExp = 0;
        savedDamage = 10;
    }

}
