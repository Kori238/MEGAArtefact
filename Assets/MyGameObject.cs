using MyMathLibrary;
using System.Collections.Generic;
using UnityEngine;

public class MyGameObject : MonoBehaviour
{
    public MyTransform myTransform = new MyTransform();
    private MeshFilter meshFilter;
    Vector3[] initialVertices;

    private void Awake()
    {
        transform.position = Vector3.zero;
        meshFilter = GetComponent<MeshFilter>();
        initialVertices = meshFilter.mesh.vertices;
        List<MyGameObject> includes = new List<MyGameObject>();
        foreach (MyGameObject mgo in myTransform.children)
        {
            includes.Add(mgo.myTransform.SetParent(this));
        }
        myTransform.children.AddRange(includes);
    }

    void LateUpdate()
    {
        Vector3[] vertices = meshFilter.mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            MyVector3 vertex = new MyVector3(initialVertices[i].x, initialVertices[i].y, initialVertices[i].z);
            if (myTransform.GetLocalToWorldMatrix().IsSingular()) continue;
            vertex = myTransform.GetLocalToWorldMatrix().TransformPoint(vertex);
            vertices[i] = vertex.ToUnityVector3(); 
        }
        meshFilter.mesh.SetVertices(vertices);
        meshFilter.mesh.RecalculateBounds();
        meshFilter.mesh.RecalculateNormals();
        meshFilter.mesh.RecalculateTangents();
    }
}
