using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingFog : MonoBehaviour
{
    public float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, Mathf.Lerp(0, 1, .5f), 0) * speed / 10);
    }
}
