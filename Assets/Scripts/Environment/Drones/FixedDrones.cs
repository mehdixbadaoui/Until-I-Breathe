using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedDrones : MonoBehaviour
{
    // PlayerDetection Script
    PlayerDetection playerDetectionScript;

    // Params of rotation
    [Range(0, 180)]
    public float rotation;
    [Range(0, 1)]
    public float speed;

    private void Start()
    {
        // Ref to the PlayerDetection Script
        playerDetectionScript = GetComponent<PlayerDetection>();

        //keeps track of the coroutine instantiated
        IEnumerator coOR = ObjectRotate();
        StartCoroutine(coOR);
    }

    IEnumerator ObjectRotate()
    {
        float timer = 0;
        while (playerDetectionScript.detected == false)
        {
            float angle = Mathf.Sin(timer) * rotation;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);

            timer += Time.deltaTime * speed;
            yield return null;
        }
    }
}
