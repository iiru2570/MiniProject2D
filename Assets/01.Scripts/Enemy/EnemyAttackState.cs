using System.Collections;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{

    private EnemyController enemy;

    private float attackTime;
    private float attackDuration;

    public EnemyAttackState(EnemyController enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
        enemy.animeController.SetAttacktrue();
        //attackTime = 0f;
        //attackDuration = 3f;
        //enemy.Attack(enemy.facingDir);
        enemy.StartCoroutine(AttackCo());
    }
    public void Update()
    {
        //attackTime += Time.deltaTime;
        //if (attackTime >= attackDuration)
        //{
        //    if (!enemy.CalDistance())
        //    {
        //        enemy.ChangeState(enemy.traceState);
        //    }
        //    else
        //    {
        //        enemy.ChangeState(enemy.attackState);
        //    }
           
        //}
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
            enemy.ChangeState(enemy.attackState);
        }
    }
}
