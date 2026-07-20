using UnityEngine;

public class PlayerStat : MonoBehaviour
{

    private int maxHp;
    public int MaxHp { get { return maxHp; } set { maxHp = value; } }

    private int nowHp;
    public int NowHp {  get { return nowHp; } set { nowHp = value; } }

    private int maxMp;
    public int MaxMp { get { return maxMp; } set { maxMp = value; } }

    private int nowMp;
    public int NowMp { get { return nowMp; } set { nowMp = value; } }

    private int damage;
    public int Damage { get { return damage; } set { damage = value; } }

    private int exp;
    public int Exp { get { return exp; } set { exp = value; } }

    private float skill1HpPercent = 0.6f;
    private float skill2HpPercent = 0.15f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHp = 100;
        nowHp = maxHp;
        maxMp = 100;
        nowMp = maxMp;
        damage = 10;
        exp = 0;
    }

    public int GetSkill1Damage()
    {
        return Mathf.RoundToInt(nowHp * skill1HpPercent);
    }

    public int GetSkill2Damage()
    {
        return Mathf.RoundToInt(nowHp * skill2HpPercent);
    }
}
