using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyRangedAttack : MonoBehaviour
{

    private EnemyController enemy;
    [SerializeField] private GameObject telegraph;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = GetComponent<EnemyController>();
        StartCoroutine(RangedAttack());
    }


    private IEnumerator RangedAttack()
    {
        while (true)
        {
            if(enemy == null)
            {
                break;
            }

            Vector3Int playerPos = enemy.tilemap.WorldToCell(enemy.playertf.position);
            Vector3 pPos = enemy.tilemap.GetCellCenterWorld(playerPos);

            if (enemy.IsPlayerVision())
            {
                Debug.Log("원거리 공격");
                GameObject go = Instantiate(telegraph, pPos, Quaternion.identity);
                Telegraph temp = go.GetComponent<Telegraph>();

                temp.Trigger(pPos, enemy.stat.rangedDamage);
            }


            yield return new WaitForSeconds(2f);
        }
    }

}
