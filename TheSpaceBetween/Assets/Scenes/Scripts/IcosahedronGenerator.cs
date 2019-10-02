using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcosahedronGenerator : MonoBehaviour
{
    public GameObject spherePrefab;
    public float scale;
    private float t;
    void Start()
    {
        // create 12 vertices of a icosahedron
    t = (1.0f + Mathf.Sqrt(5.0f)) / 2.0f;


    AddVertex(new Vector3(-1 * scale,  t * scale,  0 * scale));
    AddVertex(new Vector3( 1 * scale,  t * scale,  0 * scale));
    AddVertex(new Vector3(-1 * scale, -t * scale,  0 * scale));
    AddVertex(new Vector3( 1 * scale, -t * scale,  0 * scale));

    AddVertex(new Vector3( 0 * scale, -1 * scale,  t * scale));
    AddVertex(new Vector3( 0 * scale,  1 * scale,  t * scale));
    AddVertex(new Vector3( 0 * scale, -1 * scale, -t * scale));
    AddVertex(new Vector3( 0 * scale,  1 * scale, -t * scale));

    AddVertex(new Vector3( t * scale,  0 * scale, -1 * scale));
    AddVertex(new Vector3( t * scale,  0 * scale,  1 * scale));
    AddVertex(new Vector3(-t * scale,  0 * scale, -1 * scale));
    AddVertex(new Vector3(-t * scale,  0 * scale,  1 * scale));
    }
    void AddVertex (Vector3 point) {
        GameObject vertex = Instantiate(spherePrefab);
        vertex.transform.position = point;
        vertex.name = "vertex";
    }
    
}
