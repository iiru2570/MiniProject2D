using System.Collections;
using UnityEngine;

public class EnemyTraceState : IEnemyState
{
    private EnemyController enemy;

    Vector3Int dir;
    Vector3 enemyPos;
    Vector3 movePos;
    private float moveTime;

    public EnemyTraceState(EnemyController enemy)
    {
        this.enemy = enemy;
    }
    public void Enter()
    {
        moveTime = 0f;
        enemy.animeController.SetMovetrue();
        enemy.StartCoroutine(Trace());
    }
    public void Update()
    {

    }
    public void Exit()
    {
        enemy.animeController.SetMovefalse();
    }
    private IEnumerator Trace()
    {
        while (true)
        {
            if(enemy.CalDistance() == true)
            {
                //공격전 대기시간
                //yield return new WaitForSeconds(1f);
                enemy.ChangeState(enemy.attackState);
                break;
            }
            if (!enemy.IsPlayerVision())
            {
                enemy.ChangeState(enemy.idleState);
                break;
            }

            dir = enemy.GetDirection();
            enemyPos = enemy.transform.position;
            movePos = enemy.GetWorldPos(dir, 0.3f);

            moveTime = 0f;
            while(moveTime <= 1f)
            {
                moveTime += Time.deltaTime * enemy.moveSpeed;
                enemy.transform.position = Vector3.Lerp(enemyPos, movePos, moveTime);
                yield return null;
            }

            enemy.transform.position = movePos;

            yield return new WaitForSeconds(1f);
        }
    } 
}
