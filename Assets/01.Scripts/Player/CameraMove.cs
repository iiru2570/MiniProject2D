using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] Transform target;
    Vector3 velocity;


    private void Start()
    {
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null)
        {
            float x = Mathf.Clamp(target.position.x, -5f, 5f);
            float y = Mathf.Clamp(target.position.y, -5f, 5f);


            Vector3 targetPos = new Vector3(x,y,transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 0.2f);

        }
        
    }
}