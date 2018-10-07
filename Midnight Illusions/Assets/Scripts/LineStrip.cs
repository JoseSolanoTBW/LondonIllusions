using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class LineStrip : MonoBehaviour
{

    void Start()
    {
        GetComponent<MeshRenderer>().material = new Material(Shader.Find("Sprites/Default"));

        int n = 512;
        Vector3[] verts = new Vector3[n];
        Color[] colors = new Color[n];
        int[] indices = new int[n];

        for (int i = 0; i < n; i++)
        {
            // Indices in the verts array. First two indices form a line, 
            // and then each new index connects a new vertex to the existing line strip
            indices[i] = i;
            // Vertex colors
            colors[i] = Color.HSVToRGB((float)i / n, 1, 1);
            // Vertex positions
            verts[i] = new Vector3(i / 64f, Mathf.Sin(i / 32f), 0);
        }

        Mesh m = new Mesh
        {
            vertices = verts,
            colors = colors
        };

        m.SetIndices(indices, MeshTopology.LineStrip, 0, true);

        GetComponent<MeshFilter>().mesh = m;
    }
}