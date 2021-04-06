using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Vector3 zoom;
    private CameraFollow cameraFollow;
    private float initialSpped;
    public float zoom_speed = 0f;
    private Vector3 cameraInitialPos;
    private Transform cameraZoom;
    GameObject cameraFollow_obj;
    // Start is called before the first frame update
    void Start()
    {
        cameraFollow = FindObjectOfType<CameraFollow>();
        initialSpped = cameraFollow.smoothSpeed;
        cameraInitialPos = cameraFollow.transform.position;

        cameraZoom = transform.Find("Camera Zoom");
        cameraFollow_obj = GameObject.Find("Camera Follow");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            if(transform.Find("Camera Zoom") /*&& !other.GetComponent<LedgeLocator>().camChanged*/)
            {
                cameraFollow.zoom_offset = Vector3.Lerp(cameraFollow.zoom_offset, zoom, zoom_speed);
                cameraZoom.position = Vector3.Lerp(cameraZoom.position, new Vector3(cameraFollow_obj.transform.position.x, cameraZoom.position.y, cameraFollow_obj.transform.position.z) + cameraFollow.zoom_offset, 1f);

                cameraFollow.player = cameraZoom; 
            }
            else
            {
                cameraFollow.player = GameObject.Find("Camera Follow").transform;
                cameraFollow.zoom_offset = Vector3.Lerp(cameraFollow.zoom_offset, zoom, zoom_speed);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("uni"))
        {
            StartCoroutine(ResetCamera());
            cameraFollow.player = GameObject.Find("Camera Follow").transform;
        }


    }

    IEnumerator ResetCamera()
    {
        float startTime = Time.time;
        while (Time.time < startTime + 1f)
        {
            cameraFollow.zoom_offset = Vector3.Lerp(cameraFollow.zoom_offset, Vector3.zero, (Time.time - startTime) / 1);
            yield return null;
        }
        cameraFollow.zoom_offset = Vector3.zero;

    }

}
