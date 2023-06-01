using UnityEngine;

namespace Icarus
{
    [ExecuteInEditMode]
    public class ProceduralPlane : MonoBehaviour
    {
        public Vector2Int size;
        public int scale = 1;
        private Mesh _mesh;
        private Vector3[] _vertices;

        public void Generate()
        {
            GetComponent<MeshFilter>().mesh = _mesh = new Mesh();
            _mesh.name = "Procedural Grid";

            _vertices = new Vector3[(size.x + 1) * (size.y + 1)];
            Vector2[] uv = new Vector2[_vertices.Length];
            Vector4[] tangents = new Vector4[_vertices.Length];
            Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
            for (int i = 0, y = 0; y <= size.y; y++)
            {
                for (int x = 0; x <= size.x; x++, i++)
                {
                    _vertices[i] = new Vector3(x * scale, 0, y * scale);
                    uv[i] = new Vector2((float)x / size.x, (float)y / size.y);
                    tangents[i] = tangent;
                }
            }
            _mesh.vertices = _vertices;
            _mesh.uv = uv;
            _mesh.tangents = tangents;

            int[] triangles = new int[size.x * size.y * 6];
            for (int ti = 0, vi = 0, y = 0; y < size.y; y++, vi++)
            {
                for (int x = 0; x < size.x; x++, ti += 6, vi++)
                {
                    triangles[ti] = vi;
                    triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                    triangles[ti + 4] = triangles[ti + 1] = vi + size.x + 1;
                    triangles[ti + 5] = vi + size.x + 2;
                }
            }
            _mesh.triangles = triangles;
            _mesh.RecalculateNormals();
        }
    }
}