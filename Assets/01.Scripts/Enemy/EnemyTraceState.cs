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
            //공격 사거리안에 있을 경우
            if(enemy.CalDistance() == true)
            {
                //공격전 대기시간
                //yield return new WaitForSeconds(1f);
                enemy.ChangeState(enemy.attackState);
                break;
            }
            //몬스터가 플레이어 시야 밖에 있을 경우
            if (!enemy.IsPlayerVision())
            {
                enemy.ChangeState(enemy.idleState);
                break;
            }
           
            //첫번째 x방향 확인
            dir = enemy.GetDirectionFirst();
            enemyPos = enemy.transform.position;
            movePos = enemy.GetWorldPos(dir, 0.3f);

            //막혀있다면
            if(StageManager.instance.IsUsedPos(movePos))
            {
                //두번째 y방향 확인
                dir = enemy.GetDirectionSecond();
                movePos = enemy.GetWorldPos(dir, 0.3f);
                if (StageManager.instance.IsUsedPos(movePos))
                {
                    yield return new WaitForSeconds(1f);
                    continue;
                }
            }

            StageManager.instance.ReturnPos(enemyPos);
           
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

            //1초뒤 다시 움직임
            yield return new WaitForSeconds(1f);
        }
    } 
}
