using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShapeWithVector : MonoBehaviour
{
    public Material black;
    public Material red;

    public GameObject textUI;

    private void Start()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> triangles = new List<int>();

        // Adding Vertices
        //-------------------------------//
        vertices.Add(new Vector3(-1, 1, -1)); // 0
        vertices.Add(new Vector3(1, 1, -1)); // 1
        vertices.Add(new Vector3(-1, -1, -1)); // 2
        vertices.Add(new Vector3(1, -1, -1)); // 3

        vertices.Add(new Vector3(-1, 1, 1)); // 4
        vertices.Add(new Vector3(1, 1, 1)); // 5
        vertices.Add(new Vector3(-1, -1, 1)); // 6
        vertices.Add(new Vector3(1, -1, 1)); // 7
        //-------------------------------//

        // Adding UVs
        //-------------------------------//
        uvs.Add(new Vector2(-1, 1));
        uvs.Add(new Vector2(1, 1));
        uvs.Add(new Vector2(-1, -1));
        uvs.Add(new Vector2(1, -1));
        
        uvs.Add(new Vector2(-1, 1));
        uvs.Add(new Vector2(1, 1));
        uvs.Add(new Vector2(-1, -1));
        uvs.Add(new Vector2(1, -1));
        //-------------------------------//


        // Adding Triangles
        //--------------------------------------------------------------------//
        //---------------//
        triangles.Add(0);
        triangles.Add(1);
        triangles.Add(3);

        triangles.Add(3);
        triangles.Add(2);
        triangles.Add(0);
        //---------------//

        //---------------//
        triangles.Add(1);
        triangles.Add(5);
        triangles.Add(7);

        triangles.Add(7);
        triangles.Add(3);
        triangles.Add(1);
        //---------------//

        //---------------//
        triangles.Add(5);
        triangles.Add(4);
        triangles.Add(6);

        triangles.Add(6);
        triangles.Add(7);
        triangles.Add(5);
        //---------------//

        //---------------//
        triangles.Add(4);
        triangles.Add(0);
        triangles.Add(2);

        triangles.Add(2);
        triangles.Add(6);
        triangles.Add(4);
        //---------------//

        //---------------//
        triangles.Add(2);
        triangles.Add(3);
        triangles.Add(7);

        triangles.Add(7);
        triangles.Add(6);
        triangles.Add(2);
        //---------------//

        //---------------//
        triangles.Add(0);
        triangles.Add(4);
        triangles.Add(5);

        triangles.Add(5);
        triangles.Add(1);
        triangles.Add(0);
        //---------------//
        //--------------------------------------------------------------------//

        // Creating the mesh
        //--------------------------------------------------------------------//
        Mesh mesh = new Mesh();

        mesh.vertices = vertices.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.triangles = triangles.ToArray();

        GameObject gameObject = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));

        gameObject.GetComponent<MeshFilter>().mesh = mesh;

        gameObject.GetComponent<MeshRenderer>().material = black;
        //--------------------------------------------------------------------//

        // Creating vertices dots
        //--------------------------------------------------------------------//
        for (int i = 0; i < vertices.Count; i++)
        {
            Vector3 vector3 = vertices[i];

            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.transform.localScale = Vector3.one * 0.2f;
            go.transform.position = vector3;
            go.GetComponent<Renderer>().material = red;
            go.transform.SetParent(gameObject.transform, true);

            GameObject go2 = Instantiate(textUI);
            go.transform.position = vector3;
            go2.GetComponent<TMP_Text>().text = i + "(" + vector3.x + "," + vector3.y + "," + vector3.z + ")";

            go2.transform.SetParent(go.transform, true);
            RectTransform rect = go2.GetComponent<RectTransform>();
            rect.anchoredPosition3D = vector3;
        }
        //--------------------------------------------------------------------//
    }
}