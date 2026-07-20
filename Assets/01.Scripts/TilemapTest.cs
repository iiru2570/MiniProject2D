using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class TilemapTest : MonoBehaviour
{

    private Camera camera;
    public Tilemap tilemap;
    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //tilemap = GetComponent<Tilemap>();
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePos = new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 0);
            mousePos = camera.ScreenToWorldPoint(mousePos);
            Vector3Int cellPos = tilemap.WorldToCell(mousePos);
            Debug.Log(cellPos);
            Vector3 worldPos = tilemap.CellToWorld(cellPos);
            worldPos.x += tilemap.cellSize.x / 2f;
            worldPos.y += tilemap.cellSize.y / 2f;
            worldPos.z = player.transform.position.z;

            player.transform.position = worldPos;
        }
    }
}
