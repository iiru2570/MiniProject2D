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

    //public AnimeController animeController;
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
        idleState = new EnemyIdleState(this);
        attackState = new EnemyAttackState(this);
        traceState = new EnemyTraceState(this);

        attackCooltime = 3f;
        canAttack = true;
        //animeController = GetComponent<AnimeController>();
        facingDir = new Vector3Int(0, -1, 0);
        moveSpeed = 3.0f;


        //초기상태
        ChangeState(idleState);
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
    public Vector3Int GetDirection()
    {
        Vector3Int EnemyPos = tilemap.WorldToCell(transform.position);
        Vector3Int PlayerPos = tilemap.WorldToCell(playertf.position);

        //x가 양수인지 음수인지 y가 양수인지 음수인지 체크
        int x = EnemyPos.x - PlayerPos.x;
        int y = EnemyPos.y - PlayerPos.y;
        if(Mathf.Abs(x) >= Mathf.Abs(y))
        {
            if (x > 0)
            {
                facingDir = new Vector3Int(-1, 0, 0);
                return facingDir;
            }
            else if(x < 0)
            {
                facingDir= new Vector3Int(1, 0, 0);
                return facingDir;
            }
            else
            {
                return new Vector3Int(0, 0, 0);
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
                return new Vector3Int(0, 0, 0);
            }
        }
    }
    //거리 계산
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
    private IEnumerator AttackCool()
    {
        yield return new WaitForSeconds(attackCooltime);
        canAttack = true;
    }

    public void Attack(Vector3Int dir)
    {
        canAttack = false;
        facingDir = GetDirection();
        Collider2D hit = Physics2D.OverlapPoint(GetWorldPos(facingDir, 0f));

        if (hit != null)
        {
            Debug.Log(hit.name + " 을(를) 공격!");
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

}
