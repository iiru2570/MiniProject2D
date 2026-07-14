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
        attackTime = 0f;
        attackDuration = 1f;
        enemy.Attack(enemy.facingDir);
    }
    public void Update()
    {
        attackTime += Time.deltaTime;
        if (attackTime >= attackDuration)
        {
            if (!enemy.CalDistance())
            {
                enemy.ChangeState(enemy.idleState);
            }
            else
            {
                enemy.ChangeState(enemy.attackState);
            }
           
        }
    }
    public void Exit()
    {
        
    }

    
}
