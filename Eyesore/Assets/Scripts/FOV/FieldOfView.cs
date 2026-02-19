using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class FieldOfView : MonoBehaviour
{
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private LayerMask _raycastLayer;
    Mesh mesh;
    private float fov = 90f;
    private Vector3 origin = Vector3.zero;
    private int rayCount = 50;
    private float angle = 0f;
    private float angleIncrease;
    private float viewDistance = 20f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        makeMesh();
        updateMesh();
        //StartCoroutine(waitplease());
    }


    private Vector3 getVectorFromAngles(float angle)
    {
        float angleRad = angle * (Mathf.PI/180f);
        return new Vector3(Mathf.Cos(angleRad),Mathf.Sin(angleRad));
    }

    private IEnumerator waitplease()
    {
        yield return new WaitForSeconds(4f);
        makeMesh();

    }

    private void makeMesh()
    {
        if(_meshFilter == null)
            _meshFilter = GetComponent<MeshFilter>();
        mesh = new();

        angleIncrease = fov / rayCount;
    }
    private void updateMesh()
    {
        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount*3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for(int i = 0; i <= rayCount ; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position,getVectorFromAngles(angle),viewDistance,_raycastLayer);
            


            if(raycastHit2D.collider == null)
            {
                vertex = origin + getVectorFromAngles(angle) * viewDistance;
            }
            else
            {
                vertex = raycastHit2D.point;
            }

            if(raycastHit2D.collider != null)
            {
                Debug.Log(raycastHit2D.point);
                
            }
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

        _meshFilter.mesh = mesh;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;       
    }
}
