
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float camHeight;
    public Vector3 zoom_offset;

    void Start()
    {
        player = GameObject.Find("Camera Follow").transform;
        zoom_offset = Vector3.zero;
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = player.position + offset + zoom_offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        //transform.LookAt(player);
    }
}
