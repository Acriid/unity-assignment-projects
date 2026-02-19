using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class TestMeshMaker : MonoBehaviour
{
    [SerializeField] private MeshFilter _meshFilter;
    void Awake()
    {
        if(_meshFilter == null)
            _meshFilter = GetComponent<MeshFilter>();

        Vector3[] vertices = new Vector3[3];
        Vector2[] uv = new Vector2[3];
        int[] triangles = new int[3];

        Mesh mesh = new();
    }
}
