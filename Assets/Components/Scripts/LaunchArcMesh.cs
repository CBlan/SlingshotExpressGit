using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class LaunchArcMesh : MonoBehaviour
{
    Mesh mesh;

    public SlingController sling;

    public float meshWidth;

    public float velocity;
    public float angle;
    public int resolution = 10;

    float g; //force of gravity on the y axis.
    float radianAngle;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        g = Mathf.Abs(Physics.gravity.y);
    }

    private void OnValidate()
    {
        //check that lr is not null and that the game is playing
        if(mesh != null && Application.isPlaying)
        {
            MakeArcMesh(CalculateArcArray());
        }
    }

    private void Start()
    {
        MakeArcMesh(CalculateArcArray());
    }

    private void Update()
    {
        transform.LookAt(new Vector3(sling.slingMiddle.position.x, 0,sling.slingMiddle.position.z));
        velocity = sling.slingPower;
        angle = sling.slingAngle;
        MakeArcMesh(CalculateArcArray());
    }


    void MakeArcMesh(Vector3[] arcVerts)
    {
        mesh.Clear();

        Vector3[] vertices = new Vector3[(resolution + 1) * 2];
        int[] triangles = new int[resolution * 6 * 2];

        for (int i = 0; i <= resolution; i++)
        {
            //set vertices
            vertices[i * 2] = new Vector3(meshWidth * 0.5f, arcVerts[i].y, arcVerts[i].x);
            vertices[i * 2 + 1] = new Vector3(meshWidth * -0.5f, arcVerts[i].y, arcVerts[i].x);

            //set triangles
            if (i != resolution)
            {
                triangles[i * 12] = i * 2;
                triangles[i * 12 + 1] = triangles[i * 12 + 4] = i * 2 + 1;
                triangles[i * 12 + 2] = triangles[i * 12 + 3] = (i + 1) * 2;
                triangles[i * 12 + 5] = (i + 1) * 2 + 1;

                triangles[i * 12 + 6] = i * 2;
                triangles[i * 12 + 7] = triangles[i * 12 + 10] = (i + 1) * 2;
                triangles[i * 12 + 8] = triangles[i * 12 + 9] = i * 2 + 1;
                triangles[i * 12 + 11] = (i + 1) * 2 + 1;
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
        }
        mesh.RecalculateBounds();
    }

    //create an array of vector 3 positions for arc
    Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];

        radianAngle = Mathf.Deg2Rad * angle;
        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / g;

        for(int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance);
        }

        return arcArray;
    }

    //calcu
    Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(radianAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));
        return new Vector3(x, y);
    }


}
