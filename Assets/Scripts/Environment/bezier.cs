using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(LineRenderer))]

public class bezier : MonoBehaviour
{
    public int numberOfPoints = 20;
    LineRenderer lineRenderer;

    public GameObject p0, p1, p2, p3;

    private Vector3 tomove = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
    }

    private void Update()
    {
        curve(p0, p1, p2, p3);
    }

    private void curve(GameObject p0, GameObject p1, GameObject p2, GameObject p3)
    {

        if (numberOfPoints > 0)
        {
            lineRenderer.positionCount = numberOfPoints;
        }


        float t;
        Vector3 position;

        for(int i = 0; i < numberOfPoints; i++)
        {
            t = i / (numberOfPoints - 1.0f);
            position = p0.transform.position * bernstein(0, 3, t) +
                       p1.transform.position * bernstein(1, 3, t) +
                       p2.transform.position * bernstein(2, 3, t) +
                       p3.transform.position * bernstein(3, 3, t);

            lineRenderer.SetPosition(i, position);
        }

    }
    float bernstein(int i, int n, float t)
    {
        return fact(n) / (fact(i) * fact(n - i)) * Mathf.Pow(t, i) * Mathf.Pow(1 - t, n - i);
    }

    private int fact(int n)
    {
        if (n == 0 || n == 1) return 1;
        else return fact(n - 1);
    }


}

