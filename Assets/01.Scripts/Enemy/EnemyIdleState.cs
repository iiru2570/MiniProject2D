using System.Collections;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private EnemyController enemy;

    public EnemyIdleState(EnemyController enemy)
    {
        this.enemy = enemy;
    }
    public void Enter()
    {
        enemy.EnemyIdleAnime();
       
    }
    public void Update()
    {
        if (enemy.IsPlayerVision())
        {
            enemy.ChangeState(enemy.traceState);
        }
    }
    public void Exit()
    {
        
    }
}
