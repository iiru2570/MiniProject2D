
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public Tilemap tilemap;
    private Rigidbody2D rb;

    private AnimeController animeController;

    //바라보는 방향
    private Vector3Int facingDir;

    //움직이는 속도, 시간, 움직이고 있는지 체크하는 변수
    private bool isMoving;
    WaitForSeconds moveWait;
    private float moveSpeed;
    private float moveTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animeController = GetComponent<AnimeController>();
        moveWait = new WaitForSeconds(0.5f);
        isMoving = true;
        moveSpeed = 2f;
        //기본 방향 아래
        facingDir = new Vector3Int(0, -1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                facingDir = new Vector3Int(0, 1, 0);
                animeController.SetUp();
                animeController.SetRuntrue();
                StartCoroutine(MovingCoolTime(facingDir));
            }
            else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                facingDir = new Vector3Int(0, -1, 0);
                animeController.SetDown();
                animeController.SetRuntrue();
                StartCoroutine(MovingCoolTime(facingDir));
            }
            else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
                facingDir = new Vector3Int(-1, 0, 0);
                animeController.SetLeft();
                animeController.SetRuntrue();
                StartCoroutine(MovingCoolTime(facingDir));
            }
            else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                facingDir = new Vector3Int(1, 0, 0);
                animeController.SetRight();
                animeController.SetRuntrue();
                StartCoroutine(MovingCoolTime(facingDir));
            }
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                StartCoroutine(AttackAnime());

            }
        }

    }

    private Vector3 Move(Vector3Int dir)
    {
        Vector3Int PlayerPos = tilemap.WorldToCell(transform.position);
        //Debug.Log(cellPos);
        Vector3Int MovePos = PlayerPos + dir;
        Vector3 worldPos = tilemap.CellToWorld(MovePos);
        worldPos.x += tilemap.cellSize.x / 2f;
        worldPos.y += tilemap.cellSize.y / 2f + 0.3f;
        worldPos.z = transform.position.z;
        return worldPos;
    }
    IEnumerator MovingCoolTime(Vector3Int dir)
    {
        isMoving = false;
        moveTime = 0f;
        Vector3 startPos = transform.position;
        Vector3 targetPos = Move(dir);
        while (moveTime <= 1f)
        {
            moveTime += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(startPos, targetPos, moveTime);
            yield return null;
        }
        //프레임 간격때문에 조금씩 캐릭터가 벗어날 수도 있어서 마지막에 다시 보완.
        transform.position = targetPos;
        animeController.SetRunfalse();
        isMoving = true;
    }

    IEnumerator AttackAnime()
    {
        animeController.SetAttacktrue();
        yield return null;
        animeController.SetAttackfalse();
        Attack(facingDir);

    }

    private void Attack(Vector3Int dir)
    {
        Vector3Int PlayerPos = tilemap.WorldToCell(transform.position);
        Vector3Int AttackPos = PlayerPos + dir;
        Vector3 worldPos = tilemap.CellToWorld(AttackPos);
        worldPos.x += tilemap.cellSize.x / 2f;
        worldPos.y += tilemap.cellSize.y / 2f;
        worldPos.z = transform.position.z;

        Collider2D hit = Physics2D.OverlapPoint(worldPos);

        if(hit != null)
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
