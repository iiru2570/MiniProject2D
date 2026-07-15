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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void OnEnable()
    {
        //테스트
        maxHp = 30;
        nowHp = maxHp;
        damage = 5;
        exp = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        nowHp -= damage;
        Debug.Log($"{gameObject.name}이 {damage}만큼 대미지를 입음. 남은체력 : {nowHp}");
        if (nowHp <= 0)
        {
            //죽음
            gameObject.SetActive(false);
        }
    }

}
