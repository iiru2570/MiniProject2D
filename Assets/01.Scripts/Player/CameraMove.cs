using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Tilemap tilemap;
    Camera camera;

    Vector3 velocity;

    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;


    private void Start()
    {
        velocity = Vector3.zero;
        BoundCal();
    }

    private void BoundCal()
    {
        BoundsInt cellBounds = tilemap.cellBounds;

        Vector3 worldMin = tilemap.CellToWorld(new Vector3Int(cellBounds.xMin, cellBounds.yMin, 0));
        Vector3 worldMax = tilemap.CellToWorld(new Vector3Int(cellBounds.xMax, cellBounds.yMax, 0));

        //ÁÂ
        minX = worldMin.x + 11f;
        //¿ì
        maxX = worldMax.x - 9f;
        //¾Æ·¡
        minY = worldMin.y + 8f;
        //À§
        maxY = worldMax.y - 7f;

        if(minX > maxX)
        {
            minX = (minX + maxX) / 2f;
            maxX = minX;
        }
        if(minY > maxY)
        {
            minY = (minY + maxY) / 2f;
            maxY = minY;
        }


    }


    void LateUpdate()
    {
        if (target != null)
        {
            float x = Mathf.Clamp(target.position.x, minX, maxX);
            float y = Mathf.Clamp(target.position.y, minY, maxY);


            Vector3 targetPos = new Vector3(x,y,transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 0.2f);

        }        
    }
}