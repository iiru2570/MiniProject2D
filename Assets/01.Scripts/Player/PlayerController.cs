
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;


public interface IPlayerState
{
    void Enter();
    void Update();
    void Exit();
}

public class PlayerController : MonoBehaviour
{
    public Tilemap tilemap;
    private Rigidbody2D rb;

    public AnimeController animeController;

    public int visionRange;
    public PlayerStat stat;

    //바라보는 방향
    public Vector3Int facingDir;

    //움직이는 속도, 시간, 움직이고 있는지 체크하는 변수
    private bool isMoving;
    WaitForSeconds moveWait;
    public float moveSpeed;
    public float moveTime;

    public bool canAttack;
    public float attackCooltime;

    public bool canSkill2;
    public float skill2Cooltime;

    public bool canSkill3;
    public float skill3Cooltime;

    private IPlayerState currentState;
    
    public PlayerIdleState idleState;
    public PlayerMoveState moveState;
    public PlayerAttackState attackState;
    public PlayerSkillState skill1State;
    public PlayerSkillState skill2State;
    public PlayerSkillState skill3State;

    public PlayerHpBar hpBar;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animeController = GetComponent<AnimeController>();
        stat = GetComponent<PlayerStat>();
        hpBar = GetComponent<PlayerHpBar>();
        facingDir = new Vector3Int(0, -1, 0);
      
        idleState = new PlayerIdleState(this);
        moveState = new PlayerMoveState(this);
        attackState = new PlayerAttackState(this);
        skill1State = new PlayerSkillState(this, 1);
        skill2State = new PlayerSkillState(this, 2);
        skill3State = new PlayerSkillState(this, 3);
    }

    void Start()
    {
        moveSpeed = 2f;
        visionRange = 5;

        canAttack = true;
        attackCooltime = 0.7f;

        canSkill2 = true;
        skill2Cooltime = 2f;

        canSkill3 = true;
        skill3Cooltime = 3f;

        //초기상태 Idle
        ChangeState(idleState);
        animeController.SetDown();

        StageManager.instance.IsUsedPos(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
       currentState.Update();
    }

    public void ChangeState(IPlayerState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }

    public Vector3 GetWorldPos(Vector3Int dir, float yf)
    {
        Vector3Int playerPos = tilemap.WorldToCell(transform.position);
        //Debug.Log(cellPos);
        Vector3Int movePos = playerPos + dir;
        Vector3 worldPos = tilemap.CellToWorld(movePos);
        worldPos.x += tilemap.cellSize.x / 2f;
        worldPos.y += tilemap.cellSize.y / 2f + yf;
        worldPos.z = transform.position.z;
        return worldPos;
    }
    //IEnumerator MovingCoolTime(Vector3Int dir)
    //{
    //    isMoving = false;
    //    moveTime = 0f;
    //    Vector3 startPos = transform.position;
    //    Vector3 targetPos = Move(dir);
    //    while (moveTime <= 1f)
    //    {
    //        moveTime += Time.deltaTime * moveSpeed;
    //        transform.position = Vector3.Lerp(startPos, targetPos, moveTime);
    //        yield return null;
    //    }

    //    transform.position = targetPos;
    //    animeController.SetRunfalse();
    //    isMoving = true;
    //}

    public IEnumerator AttackAnime()
    {
        
        animeController.SetAttacktrue();
        yield return new WaitForSeconds(0.3f);
        animeController.SetAttackfalse();
    }
    private IEnumerator AttackCool()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooltime);
        canAttack = true;
    }

    public void Attack(Vector3Int dir)
    {
        Collider2D hit = Physics2D.OverlapPoint(GetWorldPos(facingDir, 0f));

        if (hit != null)
        {
            Debug.Log(hit.name + " 을(를) 공격!");
            EnemyController enemy = hit.gameObject.GetComponent<EnemyController>();
            if (enemy != null) 
            {
                enemy.TakeDamage(stat.Damage);
            }
        }
        else
        {
            Debug.Log("적이 없음");
        }
        StartCoroutine(AttackCool());
        //animeController.SetAttackfalse();
        //Debug.Log($"{PlayerPos} 칸에 있음.");
        //Debug.Log($"{worldPos} 칸에 공격!");
    }

    public void Skill1()
    {
        Collider2D hit = Physics2D.OverlapPoint(GetWorldPos(facingDir, 0f));

        if (hit != null)
        {
            EnemyController enemy = hit.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                int damage = stat.GetSkill1Damage();
                enemy.TakeDamage(damage);
                //퍼센트가 높으면 스킬 댐지가 체력을 0으로 만들수도있음. (반올림이라)
                if (damage >= stat.NowHp)
                {
                    stat.NowHp = 1;
                }
                else
                {
                    this.TakeDamage(damage);
                }

                Debug.Log(hit.name + " 을(를) 스킬 공격!");
            }
        }
        else
        {
            Debug.Log("적이 없음");
        }
        StartCoroutine(AttackCool());
    }
    public void Skill2()
    {
        int damage = stat.GetSkill2Damage();
        List<Vector3Int> vList = Skill2Cal();
        for(int i=0; i<vList.Count; i++)
        {
            Collider2D hit = Physics2D.OverlapPoint(GetWorldPos(vList[i], 0f));

            if (hit != null)
            {
                EnemyController enemy = hit.gameObject.GetComponent<EnemyController>();
                if(enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
                Debug.Log(hit.name + " 을(를) 스킬 공격!");
            }
        }
        if (damage >= stat.NowHp)
        {
            stat.NowHp = 1;
        }
        else
        {
            this.TakeDamage(damage);
        }

        StartCoroutine(Skill2Cool());
    }

    public List<Vector3Int> Skill2Cal()
    {
        List<Vector3Int> vList = new List<Vector3Int>();
        
        if(facingDir == new Vector3Int(0, 1, 0) || facingDir == new Vector3Int(0, -1, 0))
        {
            for(int i=-1; i<=1; i++)
            {
                for(int r=1; r<=2; r++)
                {
                    Vector3Int temp = facingDir;
                    temp.x += i;
                    temp.y *= r;
                    vList.Add(temp);
                }
            }
        }
        else if (facingDir == new Vector3Int(1, 0, 0)|| facingDir == new Vector3Int(-1, 0, 0))
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int r = 1; r <= 2; r++)
                {
                    Vector3Int temp = facingDir;
                    temp.x *= r;
                    temp.y += i;
                    vList.Add(temp);
                }
            }
        }

        return vList;
    }

    public void Skill3()
    {
        int healAmount = stat.GetSkill3Heal();

        if(stat.NowHp + healAmount > stat.MaxHp)
        {
            stat.NowHp = stat.MaxHp;
        }
        else
        {
            stat.NowHp += healAmount;
        }
        Debug.Log($"{stat.NowHp} / {stat.MaxHp}");
        StartCoroutine(Skill3Cool());
    }


    private IEnumerator Skill2Cool()
    {
        canSkill2 = false;
        yield return new WaitForSeconds(skill2Cooltime);
        canSkill2 = true;
    }
    private IEnumerator Skill3Cool()
    {
        canSkill3 = false;
        yield return new WaitForSeconds(skill3Cooltime);
        canSkill3 = true;
    }

    public void TakeDamage(int damage)
    {
        stat.NowHp -= damage;
        Debug.Log($"{gameObject.name}이 {damage}만큼 대미지를 입음. 남은체력 : {stat.NowHp}");
        if (stat.NowHp <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        Debug.Log("플레이어 사망");
        //사망애니메이션 넣을 자리
        yield return new WaitForSeconds(1f);
        SceneChanger.instance.LoadScene(0);
    }

    public void UpgradeHp()
    {
        //한번 버튼 누를때마다 +20
        if(stat.Exp / 5 > 0)
        {
            stat.MaxHp += 20;
            stat.Exp -= 5;
            Debug.Log($"{stat.NowHp} / {stat.MaxHp}");
        }
        else
        {
            Debug.Log("남은경험치가 없음.");
        }
    }
    public void UpgradeMp()
    {

    }

}
