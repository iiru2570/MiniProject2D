
using System.Collections;
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

    private IPlayerState currentState;
    
    public PlayerIdleState idleState;
    public PlayerMoveState moveState;
    public PlayerAttackState attackState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animeController = GetComponent<AnimeController>();
        stat = GetComponent<PlayerStat>();
        facingDir = new Vector3Int(0, -1, 0);
        moveSpeed = 2f;
        visionRange = 3;
        idleState = new PlayerIdleState(this);
        moveState = new PlayerMoveState(this);
        attackState = new PlayerAttackState(this);

        canAttack = true;
        attackCooltime = 0.7f;

        //초기상태 Idle
        ChangeState(idleState);
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
        Vector3Int PlayerPos = tilemap.WorldToCell(transform.position);
        //Debug.Log(cellPos);
        Vector3Int MovePos = PlayerPos + dir;
        Vector3 worldPos = tilemap.CellToWorld(MovePos);
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
        yield return new WaitForSeconds(0.25f);
        animeController.SetAttackfalse();
    }
    private IEnumerator AttackCool()
    {
        yield return new WaitForSeconds(attackCooltime);
        canAttack = true;
    }

    public void Attack(Vector3Int dir)
    {
        canAttack = false;
        Collider2D hit = Physics2D.OverlapPoint(GetWorldPos(facingDir, 0f));

        if (hit != null)
        {
            Debug.Log(hit.name + " 을(를) 공격!");
            EnemyStat enemy = hit.gameObject.GetComponent<EnemyStat>();
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


}
