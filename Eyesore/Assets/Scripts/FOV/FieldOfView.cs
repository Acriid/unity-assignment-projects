using System;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class FieldOfView : MonoBehaviour
{
    [SerializeField] private MeshFilter _meshFilter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(_meshFilter == null)
            _meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = new();

        float fov = 360f;
        Vector3 origin = Vector3.zero;
        int rayCount = 8;
        float angle = 0f;
        float angleIncrease = fov / rayCount;
        float viewDistance = 0.5f;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount*3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for(int i = 0; i <= rayCount ; i++)
        {
            Vector3 vertex = origin + getVectorFromAngles(angle) * viewDistance;
            vertices[vertexIndex] = vertex;

            if(i > 0)
            {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        _meshFilter.mesh.vertices = vertices;
        _meshFilter.mesh.uv = uv;
        _meshFilter.mesh.triangles = triangles;
    }


    private Vector3 getVectorFromAngles(float angle)
    {
        float angleRad = angle * (Mathf.PI/180f);
        return new Vector3(Mathf.Cos(angleRad),Mathf.Sin(angleRad));
    }
}
