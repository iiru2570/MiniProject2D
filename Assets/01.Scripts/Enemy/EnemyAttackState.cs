using System.Collections;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{

    private EnemyController enemy;

    public EnemyAttackState(EnemyController enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        enemy.animeController.SetAttacktrue();
        enemy.StartCoroutine(AttackCo());
    }
    public void Update()
    {

    }
    public void Exit()
    {
        enemy.animeController.SetAttackfalse();
    }

    IEnumerator AttackCo()
    {
        if (!enemy.CalDistance())
        {
            enemy.ChangeState(enemy.traceState);
        }
        else
        {
            enemy.Attack(enemy.facingDir);
            enemy.EnemyIdleAnime();
            yield return new WaitForSeconds(0.5f);
            enemy.animeController.SetAttackfalse();
            yield return new WaitForSeconds(1f);
            enemy.ChangeState(enemy.traceState);
        }
    }
}
