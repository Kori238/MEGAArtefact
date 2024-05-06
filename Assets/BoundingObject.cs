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
