using MyMathLibrary;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MyGameObject : MonoBehaviour
{
    public MyTransform myTransform;
    private MeshFilter meshFilter;
    Vector3[] initialVertices;

    private void Start()
    {
        myTransform = new MyTransform();
        meshFilter = GetComponent<MeshFilter>();
        initialVertices = meshFilter.mesh.vertices;
    }

    // Update is called once per frame

    public void Update()
    {
        
    }

    void LateUpdate()
    {
        Vector3[] vertices = meshFilter.mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            MyVector3 vertex = new MyVector3(initialVertices[i].x, initialVertices[i].y, initialVertices[i].z);
            vertex = myTransform.GetLocalToWorldMatrix().TransformPoint(vertex);
            //Debug.Log(vertex);
            vertices[i] = vertex.ToUnityVector3(); 
        }
        meshFilter.mesh.SetVertices(vertices);
        meshFilter.mesh.RecalculateBounds();
        meshFilter.mesh.RecalculateNormals();
        meshFilter.mesh.RecalculateTangents();
        Debug.Log(myTransform.GetLocalToWorldMatrix());
    }
}
