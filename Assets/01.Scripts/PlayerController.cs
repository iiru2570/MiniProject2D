
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public Tilemap tilemap;
    private Rigidbody2D rb;

    private bool isMoving;
    WaitForSeconds moveWait;
    float moveSpeed;
    float moveTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveWait = new WaitForSeconds(0.5f);
        isMoving = true;
        moveSpeed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                StartCoroutine(MovingCoolTime(new Vector3Int(0, 1, 0)));
            }
            else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                StartCoroutine(MovingCoolTime(new Vector3Int(0, -1, 0)));
            }
            else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
                StartCoroutine(MovingCoolTime(new Vector3Int(-1, 0, 0)));
            }
            else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                StartCoroutine(MovingCoolTime(new Vector3Int(1, 0, 0)));
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
        worldPos.y += tilemap.cellSize.y / 2f;
        worldPos.z = transform.position.z;

        //transform.position = Vector3.Lerp(PlayerPos, worldPos, 1f);
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
        //yield return moveWait;

        //프레임 간격때문에 조금씩 캐릭터가 벗어날 수도 있어서 마지막에 다시 보완.
        transform.position = targetPos;
        isMoving = true;
    }

}
