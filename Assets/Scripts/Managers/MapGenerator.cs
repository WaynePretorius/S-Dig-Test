
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MapGenerator : MonoBehaviour
{

    //cached variables
    private Vector3[] vertex;
    
    private Color[] colors;
    [SerializeField] private Color color1;
    [SerializeField] private Color color2;
    [SerializeField] private Color color3;
    [SerializeField] private Color color4;
    private GradientColorKey[] gck;
    private GradientAlphaKey[] gak;

    [SerializeField] private Gradient gradient;

    [SerializeField] private int gridX = 20;
    [SerializeField] private int gridZ = 20;
    private int[] trianglePoints;

    [SerializeField] private float gizmoRadius = 0.1f;
    //[SerializeField] private float minNoiseAmp = 2f;
    //[SerializeField] private float MaxNoiseAmp = 10f;
    [SerializeField] private float minNoise = 2f;
    [SerializeField] private float maxNoise = 10f;
    [SerializeField] private float minHeight;
    [SerializeField] private float maxheight;
    private float alphaKey = 1.0f;

    //cached References
    private Mesh triMesh;

    private void Awake()
    {
        CreateColor();
    }

    //Called when the object is enabled
    void OnEnable()
    { 
        StartFunctions();
        CreateFloor();
        UpdateMesh();
    }

    private void CreateColor()
    {
        gradient = new Gradient();

        gck = new GradientColorKey[4];
        gak = new GradientAlphaKey[4];

        color1 = Random.ColorHSV(0, 1, 0, 1, 0, 1);
        color2 = Random.ColorHSV(0, 1, 0, 1, 0, 1);
        color3 = Random.ColorHSV(0, 1, 0, 1, 0, 1);
        color4 = Random.ColorHSV(0, 1, 0, 1, 0, 1);

        gck[0].color = color1;
        gck[0].time = 0.0f;
        gak[0].alpha = alphaKey;
        gck[1].color = color2;
        gck[1].time = 0.33f;
        gak[1].alpha = alphaKey;
        gck[2].color = color3;
        gck[2].time = 0.66f;
        gak[2].alpha = alphaKey;
        gck[3].color = color4;
        gck[3].time = 1.0f;
        gak[3].alpha = alphaKey;

        gradient.SetKeys(gck, gak);

    }

    //function that calls components
    private void StartFunctions()
    {
        triMesh = new Mesh();
        GetComponent<MeshFilter>().mesh = triMesh;
    }

    //Creates the landscape
    private void CreateFloor()
    {
        vertex = new Vector3[(gridX + 1) * (gridZ + 1)];
        CreateVertices();
    }

    //Updates the mesh to show the landscape
    private void UpdateMesh()
    {
        triMesh.Clear();

        triMesh.vertices = vertex;
        triMesh.triangles = trianglePoints;
        triMesh.colors = colors;
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>() as MeshCollider;
        meshCollider.convex = true;

        triMesh.RecalculateNormals();
    }

    //creates the triangle for each quad
    private void CreateVertices()
    {
        for (int i = 0, z = 0; z <= gridZ; z++)
        {
            for (int x = 0; x <= gridX; x++)
            {
                float y = GetNoiseSample(x, z);

                vertex[i] = new Vector3(x, y, z);

                if (y > maxheight)
                {
                    maxheight = y;
                }

                if (y< minHeight)
                {
                    minHeight = y;
                }
                i++;
            }
        }


        trianglePoints = new int[gridX * gridZ * 6];

        int vert = 0;
        int tris = 0;
       
        for (int z = 0; z < gridZ; z++)
        {
            for (int x = 0; x < gridX; x++)
            {
                CreateTriangles(vert, tris);
                vert++;
                tris += 6;
            }
            vert++;
        }

        colors = new Color[vertex.Length];

        for (int i = 0, z = 0; z <= gridZ; z++)
        {
            for (int x = 0; x <= gridX; x++)
            {
                float height = Mathf.InverseLerp(minHeight, maxheight, vertex[i].y);
                colors[i] = gradient.Evaluate(height);
                i++;
            }
        }
    }

    private float GetNoiseSample(int x, int z)
    {
        float xValue = (float)x / gridX;
        float zValue = (float)z / gridZ;

        float randomPerlin = Random.Range(minNoise, maxNoise);
        float yValue = Mathf.PerlinNoise((xValue * randomPerlin) / 2, (zValue * randomPerlin) / 2);

        return yValue;
    }

    //create the triangle from the different vertices to make a quad
    private void CreateTriangles(int v, int t)
    {
        trianglePoints[t + 0] = v +0;
        trianglePoints[t + 1] = v +gridX + 1;
        trianglePoints[t + 2] = v + 1;
        trianglePoints[t + 3] = v + 1;
        trianglePoints[t + 4] = v + gridX + 1;
        trianglePoints[t + 5] = v + gridX + 2;
    }

    //draws the grid
    private void OnDrawGizmos()
    {
        if (vertex == null) return;

        for(int i = 0; i < vertex.Length; i++)
        {
            Gizmos.DrawSphere(vertex[i], gizmoRadius);
        }
    }
}
