using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedDrones : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(ObjectRotate());
    }

    IEnumerator ObjectRotate()
    {
        float timer = 0;
        while (true)
        {
            float angle = Mathf.Sin(timer) * 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);

            timer += Time.deltaTime;
            yield return null;
        }
    }
}
