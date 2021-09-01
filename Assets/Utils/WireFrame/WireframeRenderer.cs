using UnityEngine;

public class WireframeRenderer : MonoBehaviour
{
    Mesh mesh;
    MeshFilter meshFilter;

    int[] meshTriangles;
    Vector3[] meshVertices;
    Vector3[] meshNormals;
    int meshTrianglesLength;

    Vector3[] processedVertices;
    Vector2[] processedUVs;
    int[] processedTriangles;
    Vector3[] processedNormals;

    Vector2 uv00;
    Vector2 uv01;
    Vector2 uv10;

    void Start()
    {
        uv00 = new Vector2(0, 0);
        uv01 = new Vector2(0, 1);
        uv10 = new Vector2(1, 0);

        mesh = GetComponent<MeshFilter>().sharedMesh;
    }

    void LateUpdate()
    {
        MeshUpdate();
    }

    private void MeshUpdate()
    {
        if (mesh == null) return;

        meshTriangles = mesh.triangles;
        meshVertices = mesh.vertices;
        meshNormals = mesh.normals;
        meshTrianglesLength = meshTriangles.Length;
        if (meshTrianglesLength == 0) return;

        processedVertices = new Vector3[meshTrianglesLength];
        processedUVs = new Vector2[meshTrianglesLength];
        processedTriangles = new int[meshTrianglesLength];
        processedNormals = new Vector3[meshTrianglesLength];


        for (int i = 0; i < meshTrianglesLength; i += 3)
        {
            processedVertices[i] = meshVertices[meshTriangles[i]];
            processedVertices[i + 1] = meshVertices[meshTriangles[i + 1]];
            processedVertices[i + 2] = meshVertices[meshTriangles[i + 2]];

            processedUVs[i] = uv00;
            processedUVs[i + 1] = uv10;
            processedUVs[i + 2] = uv01;

            processedTriangles[i] = i;
            processedTriangles[i + 1] = i + 1;
            processedTriangles[i + 2] = i + 2;

            processedNormals[i] = meshNormals[meshTriangles[i]];
            processedNormals[i + 1] = meshNormals[meshTriangles[i + 1]];
            processedNormals[i + 2] = meshNormals[meshTriangles[i + 2]];
        }

        mesh.Clear();
        // Debug.Log($"({processedVertices.Length}, {processedTriangles.Length})");
        mesh.vertices = processedVertices;
        mesh.uv = processedUVs;
        mesh.triangles = processedTriangles;
        mesh.normals = processedNormals;
    }
}
