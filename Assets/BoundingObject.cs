using MyMathLibrary;
using UnityEngine;

public abstract class BoundingObject : MonoBehaviour
{
    public abstract bool Intersects(BoundingObject other);

    private void FixedUpdate()
    {
        foreach (BoundingObject obj in GameObject.FindObjectsOfType<BoundingObject>())
        {
            if (obj == this) continue;
            Debug.Log(Intersects(obj));
        }
    }
}

public abstract class BoundingBox : BoundingObject
{
    public MyVector3 min;
    public MyVector3 max;
    public MyVector3 worldMin;
    public MyVector3 worldMax;
}