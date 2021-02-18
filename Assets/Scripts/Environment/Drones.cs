using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drones : MonoBehaviour
{
    public Transform[] Waypoints;
    public float moveSpeed = 5f;
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Places the platform at the start of the waypoints[]
        transform.position = Waypoints[waypointIndex].transform.position;

        // Creating a mesh for the FOV of the drone
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        // Params of the FOV
        float fov = 90f;
        Vector3 origin = Vector3.zero;
        int rayCount = 10; 
        float angle = 0f;
        float angleIncrease = fov / rayCount;
        float viewDistance = 5f;

        // Coordinates for the FOV
        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount*3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance);
            if(raycastHit.collider == null)
            {
                // No hit detected
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                // Hit an object on the map
                vertex = raycastHit.point;
            }
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0; 
                triangles[triangleIndex + 1] = vertexIndex - 1; 
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    // Returns a vector from an angle (in degrees)
    public Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Move();
    }

    void Move()
    {
        // Handles moving the platform from A to B or B to A
        transform.position = Vector3.MoveTowards(transform.position, Waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

        // Increases the waypointIndex when the platform reaches the current waypoint ahead
        if (transform.position == Waypoints[waypointIndex].transform.position)
        {
            waypointIndex += 1;
        }

        // Resets the waypointIndex once the platform reaches the end of the Waypoints[]
        if (waypointIndex == Waypoints.Length)
        {
            waypointIndex = 0;
        }
    }
}
