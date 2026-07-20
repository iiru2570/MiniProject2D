using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class NPCController : MonoBehaviour
{

    private Camera camera;
    //public Tilemap tilemap;
    public GameObject statPanel;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            ClickNPC();
        }
    }

    public void ClickNPC()
    {
        Vector3 mousePos = new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 0);
        mousePos = camera.ScreenToWorldPoint(mousePos);

        Collider2D hit = Physics2D.OverlapPoint(mousePos);

        if (hit != null)
        {
            NPC npc = hit.GetComponent<NPC>();
            if(npc != null)
            {
                statPanel.SetActive(true);
            }
        }

    }
}
