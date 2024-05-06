using MyMathLibrary;
using UnityEngine;

public abstract class BoundingObject : MonoBehaviour
{
    public abstract bool Intersects(BoundingObject other);

    public MyTransform myTransform;

    public void Start()
    {
        myTransform = GetComponent<MyGameObject>().myTransform;
    }
}

// AABB (Axis-Aligned Bounding Box)