
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public Tilemap tilemap;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            Move(new Vector3Int(0, 1, 0));
        }
        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            Move(new Vector3Int(0, -1, 0));
        }
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            Move(new Vector3Int(-1, 0, 0));
        }
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            Move(new Vector3Int(1, 0, 0));
        }
        
    }

    private void Move(Vector3Int dir)
    {
        Vector3Int PlayerPos = tilemap.WorldToCell(transform.position);
        //Debug.Log(cellPos);
        Vector3Int MovePos = PlayerPos + dir;
        Vector3 worldPos = tilemap.CellToWorld(MovePos);
        worldPos.x += tilemap.cellSize.x / 2f;
        worldPos.y += tilemap.cellSize.y / 2f;
        worldPos.z = transform.position.z;

        transform.position = worldPos;
    }
}
