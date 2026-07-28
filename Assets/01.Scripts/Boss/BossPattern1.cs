using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossPattern1 : MonoBehaviour
{
    private EnemyController enemy;
    [SerializeField] private GameObject telegraph;
    private List<Vector3Int> patternPos = new List<Vector3Int>();

    void Start()
    {
        enemy = GetComponent<EnemyController>();

        patternPos.Add(new Vector3Int(0, 0, 0));
        patternPos.Add(new Vector3Int(0, 1 ,0));
        patternPos.Add(new Vector3Int(0, -1 ,0));
        patternPos.Add(new Vector3Int(1, 0 ,0));
        patternPos.Add(new Vector3Int(-1, 0 ,0));

        StartCoroutine(PatternAttack());
    }


    private IEnumerator PatternAttack()
    {
        while (true)
        {
            if (enemy == null)
            {
                break;
            }           
            for(int i=0; i<patternPos.Count; i++)
            {
                Vector3Int playerPos = enemy.tilemap.WorldToCell(enemy.playertf.position);
                playerPos += patternPos[i];

                Vector3 pPos = enemy.tilemap.GetCellCenterWorld(playerPos);

                if (enemy.IsPlayerVision())
                {
                    GameObject go = Instantiate(telegraph, pPos, Quaternion.identity);
                    Telegraph temp = go.GetComponent<Telegraph>();

                    temp.Trigger(pPos, enemy.stat.rangedDamage);
                }
            }
            yield return new WaitForSeconds(7f);
        }
    }
}
