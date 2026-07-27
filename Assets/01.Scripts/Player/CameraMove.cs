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
        camera = Camera.main;
        velocity = Vector3.zero;
        BoundCal();
    }

    private void BoundCal()
    {

        //실제 타일이 존재하는영역으로 압축
        tilemap.CompressBounds();

        Bounds bounds = tilemap.localBounds;

        Vector3 worldMin = tilemap.transform.TransformPoint(bounds.min);
        Vector3 worldMax = tilemap.transform.TransformPoint(bounds.max);


        //float camHeight = camera.orthographicSize;
        //float camWidth = camHeight + camera.aspect;

        //좌
        minX = worldMin.x;
        //우
        maxX = worldMax.x;
        //아래
        minY = worldMin.y;
        //위
        maxY = worldMax.y;

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

        Debug.Log($"{worldMax} / {worldMin}");
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