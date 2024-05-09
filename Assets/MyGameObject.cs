using MyMathLibrary;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MyGameObject : MonoBehaviour
{
    public MyTransform myTransform = new MyTransform();
    private MeshFilter meshFilter;
    public Mesh originalMesh;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        if (originalMesh == null)
        {
            originalMesh = meshFilter.sharedMesh;
        }
        meshFilter.sharedMesh = Instantiate(originalMesh);
    }

    /*private void OnValidate()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = Instantiate(originalMesh);
        if (Application.isEditor && !Application.isPlaying)
        {
            if (meshFilter != null && meshFilter.sharedMesh != null)
            {
                if (initialVertices == null)
                {
                    initialVertices = meshFilter.sharedMesh.vertices;
                } else
                {
                    TransformMesh();
                }
            }
        }
    }*/

    private void TransformMesh()
    {
        if (meshFilter != null && meshFilter.sharedMesh != null)
        {
            Vector3[] vertices = originalMesh.vertices;
            for (int i = 0; i < vertices.Length; i++)
            {
                MyVector3 vertex = new MyVector3(originalMesh.vertices[i].x, originalMesh.vertices[i].y, originalMesh.vertices[i].z);
                if (myTransform.GetLocalToWorldMatrix().IsSingular()) continue;
                vertex = myTransform.GetLocalToWorldMatrix().TransformPoint(vertex);
                vertices[i] = vertex.ToUnityVector3(); 
            }
            meshFilter.sharedMesh.SetVertices(vertices);
            meshFilter.sharedMesh.RecalculateBounds();
            meshFilter.sharedMesh.RecalculateNormals();
            meshFilter.sharedMesh.RecalculateTangents();
        }
    }

    private void LateUpdate()
    {
        TransformMesh();
        List<MyGameObject> includes = new List<MyGameObject>();
        foreach (MyGameObject mgo in myTransform.children)
        {
            if (mgo == null) continue;
            includes.Add(mgo.myTransform.SetParent(this));
        }
        foreach (MyGameObject mgo in includes) 
        {
            if (mgo == null) continue;
            myTransform.children.Add(mgo);
        }
    }
}
