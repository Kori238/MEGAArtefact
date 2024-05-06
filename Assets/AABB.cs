using MyMathLibrary;
using UnityEngine;

public abstract class BoundingObject : MonoBehaviour
{
    public abstract bool Intersects(BoundingObject other);
}

// AABB (Axis-Aligned Bounding Box)