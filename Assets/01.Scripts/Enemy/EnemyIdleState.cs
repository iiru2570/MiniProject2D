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
        
    }
    public void Update()
    {
        enemy.ChangeState(enemy.traceState);

    }
    public void Exit()
    {
        
    }

    
}
