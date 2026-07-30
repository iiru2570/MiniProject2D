using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class BossPattern2 : MonoBehaviour
{
    private EnemyController enemy;
    [SerializeField] private GameObject telegraph;
    private List<Vector3Int> patternPos1 = new List<Vector3Int>();
    private List<Vector3Int> patternPos2 = new List<Vector3Int>();

    void Start()
    {
        enemy = GetComponent<EnemyController>();

        patternPos1.Add(new Vector3Int(0, 1, 0));
        patternPos1.Add(new Vector3Int(1, 1, 0));
        patternPos1.Add(new Vector3Int(-1, 1, 0));
        patternPos1.Add(new Vector3Int(0, -1, 0));
        patternPos1.Add(new Vector3Int(1, -1, 0));
        patternPos1.Add(new Vector3Int(-1, -1, 0));
        patternPos1.Add(new Vector3Int(1, 0, 0));
        patternPos1.Add(new Vector3Int(-1, 0, 0));


        patternPos2.Add(new Vector3Int(-2, 2, 0));
        patternPos2.Add(new Vector3Int(-1, 2, 0));
        patternPos2.Add(new Vector3Int(0, 2, 0));
        patternPos2.Add(new Vector3Int(1, 2, 0));
        patternPos2.Add(new Vector3Int(2, 2, 0));

        patternPos2.Add(new Vector3Int(-2, -1, 0));
        patternPos2.Add(new Vector3Int(2, -1, 0));

        patternPos2.Add(new Vector3Int(-2, 0, 0));
        patternPos2.Add(new Vector3Int(2, 0, 0));

        patternPos2.Add(new Vector3Int(-2, 1, 0));
        patternPos2.Add(new Vector3Int(2, 1, 0));

        patternPos2.Add(new Vector3Int(-2, -2, 0));
        patternPos2.Add(new Vector3Int(-1, -2, 0));
        patternPos2.Add(new Vector3Int(0, -2, 0));
        patternPos2.Add(new Vector3Int(1, -2, 0));
        patternPos2.Add(new Vector3Int(2, -2, 0));
    }


    public IEnumerator PatternAttack()
    {
        if (enemy.IsPlayerVision())
        {
            Vector3Int playerPos = enemy.tilemap.WorldToCell(gameObject.transform.position);
            Vector3Int tempPos = playerPos;
            SoundManager.instance.PlaySFX(SFXType.BossPattern1);
            for (int i = 0; i < patternPos1.Count; i++)
            {
                tempPos = playerPos;
                tempPos += patternPos1[i];

                Vector3 pPos = enemy.tilemap.GetCellCenterWorld(tempPos);

                GameObject go = Instantiate(telegraph, pPos, Quaternion.identity);
                Telegraph temp = go.GetComponent<Telegraph>();

                temp.Trigger(pPos, enemy.stat.rangedDamage);
            }
            yield return new WaitForSeconds(1f);
            SoundManager.instance.PlaySFX(SFXType.BossPattern2);
            for (int i = 0; i < patternPos2.Count; i++)
            {
                tempPos = playerPos;
                tempPos += patternPos2[i];

                Vector3 pPos = enemy.tilemap.GetCellCenterWorld(tempPos);

                GameObject go = Instantiate(telegraph, pPos, Quaternion.identity);
                Telegraph temp = go.GetComponent<Telegraph>();

                temp.Trigger(pPos, enemy.stat.rangedDamage);

            }
        }
    }
}
