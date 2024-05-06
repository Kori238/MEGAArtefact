using MyMathLibrary;
using UnityEngine;

public class BoundingBox : BoundingObject
{
    public MyVector3 min;
    public MyVector3 max;

    public override bool Intersects(BoundingObject other)
    {
        if (other is BoundingBox)
        {
            BoundingBox otherBox = (BoundingBox)other;
            return (min.x <= otherBox.max.x && max.x >= otherBox.min.x) &&
                   (min.y <= otherBox.max.y && max.y >= otherBox.min.y) &&
                   (min.z <= otherBox.max.z && max.z >= otherBox.min.z);
        }
        else if (other is BoundingSphere)
        {
            BoundingSphere otherSphere = (BoundingSphere)other;
            MyVector3 closestPoint = MyVector3.Clamp(otherSphere.center, min, max);
            MyVector3 distance = MyVector3.Subtract(otherSphere.center, closestPoint);
            return MyVector3.MagnitudeSquared(distance) <= otherSphere.radius * otherSphere.radius;
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3((min.x + max.x) / 2, (min.y + max.y) / 2, (min.z + max.z) / 2), new Vector3(max.x - min.x, max.y - min.y, max.z - min.z));
    }
}