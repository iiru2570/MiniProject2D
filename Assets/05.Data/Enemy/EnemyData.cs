using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int maxHp;
    public int damage;
    public int exp;


    public int rangedDamage;
}
