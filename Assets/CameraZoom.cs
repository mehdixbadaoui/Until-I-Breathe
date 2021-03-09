using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoom = 0f;
    private CameraFollow camera_follow;
    private float initial_spped;
    public float zoom_speed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        camera_follow = FindObjectOfType<CameraFollow>();
        initial_spped = camera_follow.smoothSpeed;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            //camera_follow.smoothSpeed = initial_spped - zoom_speed;
            camera_follow.zoom_offset = Vector3.Lerp(camera_follow.zoom_offset, new Vector3(-zoom, 0, 0), zoom_speed);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            StartCoroutine(ResetCamera());
        }
        

    }

    IEnumerator ResetCamera()
    {
        float startTime = Time.time;
        while (Time.time < startTime + zoom_speed)
        {
            camera_follow.zoom_offset = Vector3.Lerp(camera_follow.zoom_offset, Vector3.zero, (Time.time - startTime) / zoom_speed);
            yield return null;
        }
        camera_follow.zoom_offset = Vector3.zero;

    }

}
