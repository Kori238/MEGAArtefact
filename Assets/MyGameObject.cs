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
        myTransform.UpdateLocalToWorldMatrix();
        meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null) return;
        if (originalMesh == null)
        {
            originalMesh = meshFilter.sharedMesh;
        }
        meshFilter.sharedMesh = Instantiate(originalMesh);
    }
    private void TransformMesh()
    {
        if (meshFilter != null && meshFilter.sharedMesh != null)
        {
            Vector3[] vertices = originalMesh.vertices;
            for (int i = 0; i < vertices.Length; i++)
            {
                MyVector3 vertex = new MyVector3(originalMesh.vertices[i].x, originalMesh.vertices[i].y, originalMesh.vertices[i].z);
                if (myTransform.localToWorldMatrix.IsSingular()) continue;
                vertex = myTransform.localToWorldMatrix.TransformPoint(vertex);
                vertices[i] = vertex.ToUnityVector3(); 
            }
            meshFilter.sharedMesh.SetVertices(vertices);
            meshFilter.sharedMesh.RecalculateBounds();
            meshFilter.sharedMesh.RecalculateNormals();
            meshFilter.sharedMesh.RecalculateTangents();
        }
        myTransform.hasUpdated = false;
    }

    private void LateUpdate()
    {
        if (meshFilter != null && myTransform.hasUpdated) TransformMesh();
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

    private void OnValidate()
    {
        myTransform.UpdateLocalToWorldMatrix();
    }
}
