using MyMathLibrary;
using UnityEngine;

public class BoundingSphere : BoundingObject
{
    public MyVector3 center;
    public float radius;
    

    public override bool Intersects(BoundingObject other)
    {
        if (other is BoundingBox)
        {
            return other.Intersects(this);
        }
        else if (other is BoundingSphere)
        {
            BoundingSphere otherSphere = (BoundingSphere)other;
            MyVector3 distance = MyVector3.Subtract(center, otherSphere.center);
            return MyVector3.MagnitudeSquared(distance) <= (radius + otherSphere.radius) * (radius + otherSphere.radius);
        }
        return false;
    }

    private void Update()
    {
        center = GetComponent<MyGameObject>().myTransform.GetLocalToWorldMatrix().GetPosition();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(center.ToUnityVector3(), radius);
    }
}