using System.Collections;
using UnityEngine;

public class EnemySkillState : IEnemyState
{

    private EnemyController enemy;
    private BossPattern1 pattern1;
    private BossPattern2 pattern2;

    private float skillDuration = 2f;


    public EnemySkillState(EnemyController enemy)
    {
        this.enemy = enemy;
        pattern1 = enemy.GetComponent<BossPattern1>();
        pattern2 = enemy.GetComponent<BossPattern2>();
    }

    public void Enter()
    {
        enemy.SetSkillUsed();
        if(pattern1 != null && pattern2 != null)
        {
            enemy.StartCoroutine(SkillRoutine());
        }
        else
        {
            enemy.ChangeState(enemy.traceState);
        }
    }

    public void Update()
    {
        
    }

    public void Exit()
    {

    }
    private IEnumerator SkillRoutine()
    {
        //bool canUse1 = enemy.canPattern1;
        //bool canUse2 = enemy.canPattern2;

        //if(!canUse1 && !canUse2)
        //{
        //    enemy.ChangeState(enemy.traceState);
        //}

        int choice = Random.Range(0, 2);

        if(choice == 0)
        {
            pattern1.PatternAttack();
            //enemy.StartCoroutine(Skill1Cool());
        }
        else
        {
            yield return enemy.StartCoroutine(pattern2.PatternAttack());
            //enemy.StartCoroutine(Skill2Cool());
        }
        yield return new WaitForSeconds(skillDuration);
        enemy.ChangeState(enemy.traceState);
    }

    private IEnumerator Skill1Cool()
    {
        enemy.canPattern1 = false;
        yield return new WaitForSeconds(5f);
        enemy.canPattern1 = true;
    }
    private IEnumerator Skill2Cool()
    {
        enemy.canPattern2 = false;
        yield return new WaitForSeconds(8f);
        enemy.canPattern2 = true;
    }

}


