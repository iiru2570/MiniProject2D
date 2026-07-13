
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

    //바라보는 방향
    public Vector3Int facingDir;

    //움직이는 속도, 시간, 움직이고 있는지 체크하는 변수
    private bool isMoving;
    WaitForSeconds moveWait;
    public float moveSpeed;
    public float moveTime;

    private IPlayerState currentState;
    
    public PlayerIdleState idleState;
    public PlayerRunState runState;
    public PlayerAttackState attackState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animeController = GetComponent<AnimeController>();
        facingDir = new Vector3Int(0, -1, 0);
        moveSpeed = 2f;
        idleState = new PlayerIdleState(this);
        runState = new PlayerRunState(this);
        attackState = new PlayerAttackState(this);

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

    public Vector3 Move(Vector3Int dir, float yf)
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

    //IEnumerator AttackAnime()
    //{
    //    animeController.SetAttacktrue();
    //    yield return null;
    //    animeController.SetAttackfalse();
    //    Attack(facingDir);

    //}

    public void Attack(Vector3Int dir)
    {
        Collider2D hit = Physics2D.OverlapPoint(Move(facingDir, 0f));

        if (hit != null)
        {
            Debug.Log(hit.name + " 을(를) 공격!");
        }
        else
        {
            Debug.Log("적이 없음");
        }
        //animeController.SetAttackfalse();
        //Debug.Log($"{PlayerPos} 칸에 있음.");
        //Debug.Log($"{worldPos} 칸에 공격!");

    }

}
