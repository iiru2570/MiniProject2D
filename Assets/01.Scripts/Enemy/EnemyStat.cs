using UnityEngine;

public class EnemyStat : MonoBehaviour
{

    private int maxHp;
    public int MaxHp { get { return maxHp; } set { maxHp = value; } }

    private int nowHp;
    public int NowHp { get { return nowHp; } set { nowHp = value; } }

    private int damage;
    public int Damage { get { return damage; } set { damage = value; } }

    private int exp;
    public int Exp { get { return exp; } set { exp = value; } }

    public int rangedDamage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void OnEnable()
    {
        //Å×½ºÆ®
        maxHp = 30;
        nowHp = maxHp;
        damage = 5;
        exp = 10;
        rangedDamage = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

}
