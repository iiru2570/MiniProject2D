using System.Collections;
using UnityEngine;

public class EnemyTraceState : IEnemyState
{
    private EnemyController enemy;

    private Vector3Int dir;
    private Vector3 enemyPos;
    private Vector3 movePos;
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

            //문제점 겹칩 - 몬스터가 두마리가 서로 앞이 비어있다고 했을때 겹침.
            //if (enemy.detectfacingDir())
            //{
            //    enemy.ChangeState(enemy.idleState);
            //    yield return null;
            //    break;
            //}

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
