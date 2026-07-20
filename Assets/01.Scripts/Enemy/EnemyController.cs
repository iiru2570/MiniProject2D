using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;


public interface IEnemyState
{
    void Enter();
    void Update();
    void Exit();
}

public class EnemyController : MonoBehaviour
{
    public Tilemap tilemap;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private EnemyStat stat;

    public GameObject player;
    private PlayerController playerController;
    public AnimeController animeController;
    public Vector3Int facingDir;
    public Transform playertf;
    public float moveSpeed;

    public bool canAttack;
    public float attackCooltime;

    private IEnemyState currentState;

    public EnemyIdleState idleState;
    public EnemyAttackState attackState;
    public EnemyTraceState traceState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animeController = GetComponent<AnimeController>();
        sr = GetComponent<SpriteRenderer>();
        tilemap = GameObject.Find("GroundTilemap").GetComponent<Tilemap>();
        player = GameObject.Find("Player");
        playertf = player.transform;
        playerController = player.GetComponent<PlayerController>();

        stat = GetComponent<EnemyStat>();
        idleState = new EnemyIdleState(this);
        attackState = new EnemyAttackState(this);
        traceState = new EnemyTraceState(this);

        attackCooltime = 3f;
        canAttack = true;
        
        facingDir = new Vector3Int(0, -1, 0);
        moveSpeed = 2.0f;


        //초기상태
        ChangeState(idleState);
    }
    private void OnEnable()
    {
        if(idleState != null)
        {
            ChangeState(idleState);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Update();
    }

    public void ChangeState(IEnemyState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }

    //방향 계산
    public Vector3Int GetDirectionFirst()
    {
        Vector3Int EnemyPos = tilemap.WorldToCell(transform.position);
        Vector3Int PlayerPos = tilemap.WorldToCell(playertf.position);

        //x가 양수인지 음수인지 y가 양수인지 음수인지 체크
        int x = EnemyPos.x - PlayerPos.x;
        int y = EnemyPos.y - PlayerPos.y;
        //같을 때 만약 x방향으로 못가면 y방향으로 가도록 해야함.
        if(Mathf.Abs(x) >= Mathf.Abs(y))
        {
            if (x > 0)
            {
                facingDir = new Vector3Int(-1, 0, 0);
                sr.flipX = true;
                return facingDir;
            }
            else if(x < 0)
            {
                facingDir= new Vector3Int(1, 0, 0);
                sr.flipX = false;
                return facingDir;
            }
            else
            {
                return Random.Range(0,2) == 0 ? new Vector3Int(1,0,0) : new Vector3Int(-1,0,0);
            }

        }
        else
        {
            if (y > 0)
            {
                facingDir = new Vector3Int(0, -1, 0);
                return facingDir;
            }
            else if(y < 0)
            {
                facingDir = new Vector3Int(0, 1, 0);
                return facingDir;
            }
            else
            {
                return Random.Range(0, 2) == 0 ? new Vector3Int(0, 1, 0) : new Vector3Int(0, -1, 0);
            }
        }
    }

    public Vector3Int GetDirectionSecond()
    {
        Vector3Int EnemyPos = tilemap.WorldToCell(transform.position);
        Vector3Int PlayerPos = tilemap.WorldToCell(playertf.position);

        //x가 양수인지 음수인지 y가 양수인지 음수인지 체크
        int x = EnemyPos.x - PlayerPos.x;
        int y = EnemyPos.y - PlayerPos.y;

        if (Mathf.Abs(x) >= Mathf.Abs(y))
        {
            if (y > 0)
            {
                facingDir = new Vector3Int(0, -1, 0);
                return facingDir;
            }
            else if (y < 0)
            {
                facingDir = new Vector3Int(0, 1, 0);
                return facingDir;
            }
            else
            {
                return Random.Range(0, 2) == 0 ? new Vector3Int(0, 1, 0) : new Vector3Int(0, -1, 0);
            }

        }
        else
        {
            if (x > 0)
            {
                facingDir = new Vector3Int(-1, 0, 0);
                sr.flipX = true;
                return facingDir;
            }
            else if (x < 0)
            {
                facingDir = new Vector3Int(1, 0, 0);
                sr.flipX = false;
                return facingDir;
            }
            else
            {
                return Random.Range(0, 2) == 0 ? new Vector3Int(1, 0, 0) : new Vector3Int(-1, 0, 0);
            }
        }

    }

    //플레이어와 몬스터의 거리가 플레이어 시야 내에 있을 경우.
    //visionRange는 일단 보류. 이거 몬스터가 이거 하나 때문에
    //PlayerController 전부를 가지고 있을 필요 없음. 수정 필요.
    public bool IsPlayerVision()
    {
        Vector3Int EnemyPos = tilemap.WorldToCell(transform.position);
        Vector3Int PlayerPos = tilemap.WorldToCell(playertf.position);

        int x = Mathf.Abs(EnemyPos.x - PlayerPos.x);
        int y = Mathf.Abs(EnemyPos.y - PlayerPos.y);

        if (Mathf.Max(x, y) <= playerController.visionRange)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    //거리 계산 - 플레이어랑 한칸 이내인지 확인
    public bool CalDistance()
    {
        Vector3Int EnemyPos = tilemap.WorldToCell(transform.position);
        Vector3Int PlayerPos = tilemap.WorldToCell(playertf.position);

        int x = Mathf.Abs(EnemyPos.x - PlayerPos.x);
        int y = Mathf.Abs(EnemyPos.y - PlayerPos.y);

        //대각선 1칸은 빼야함.
        if(x == 1 && y == 0)
        {
            return true;
        }
        else if(x == 0 && y == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //목표 위치 계산 - 한칸씩 이동하기 위해서 dir을 1칸씩 이동시킴
    // yf는 마지막에 위치에서 캐릭터가 타일 위에 있는 것 처럼 하기 위해서 조정
    public Vector3 GetWorldPos(Vector3Int dir, float yf)
    {
        Vector3Int PlayerPos = tilemap.WorldToCell(transform.position);
        //Debug.Log(cellPos);
        Vector3Int MovePos = PlayerPos + dir;
        Vector3 worldPos = tilemap.CellToWorld(MovePos);
        worldPos.x += tilemap.cellSize.x / 2f;
        worldPos.y += tilemap.cellSize.y / 2f + yf;
        worldPos.z = transform.position.z;
        return worldPos;
    }
    public bool detectfacingDir()
    {
        Collider2D hit = Physics2D.OverlapPoint(GetWorldPos(facingDir, 0f));
        if (hit != null) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Attack(Vector3Int dir)
    {
        //canAttack = false;
        facingDir = GetDirectionFirst();
        Collider2D hit = Physics2D.OverlapPoint(GetWorldPos(facingDir, 0f));

        if (hit != null)
        {
            Debug.Log(hit.name + " 을(를) 공격!");
            PlayerController player = hit.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(stat.Damage);
            }
        }
        else
        {
            Debug.Log("적이 없음");
        }
        //StartCoroutine(AttackCool());
        //animeController.SetAttackfalse();
        //Debug.Log($"{PlayerPos} 칸에 있음.");
        //Debug.Log($"{worldPos} 칸에 공격!");
    }
    public IEnumerator MoveAnime()
    {
        animeController.SetMovetrue();
        yield return new WaitForSeconds(0.35f);
        animeController.SetMovefalse();
    }

    public void TakeDamage(int damage)
    {
        stat.NowHp -= damage;
        Debug.Log($"{gameObject.name}이 {damage}만큼 대미지를 입음. 남은체력 : {stat.NowHp}");
        if (stat.NowHp <= 0)
        {
            //죽음
            playerController.stat.Exp += stat.Exp;
            Debug.Log($"경험치 {stat.Exp}를 얻었습니다. 누적경험치 : {playerController.stat.Exp}");
            StageManager.instance.ReturnPos(transform.position);
            EnemyPoolManager.instance.ReturnEnemy(gameObject.name, gameObject);
        }
    }

}
