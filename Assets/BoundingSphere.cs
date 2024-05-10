using MyMathLibrary;
using System;
using System.Reflection;
using UnityEngine;

public class BoundingSphere : BoundingObject
{
    public MyVector3 center;
    public float radius;
    public MyVector3 worldCenter;
    public float worldRadius;
    

    public override bool Intersects(BoundingObject other)
    {
        if (other is OBB)
        {
            return other.Intersects(this);
        }
        else if (other is BoundingSphere)
        {
            BoundingSphere otherSphere = (BoundingSphere)other;
            MyVector3 distance = MyVector3.Subtract(worldCenter, otherSphere.worldCenter);
            return MyVector3.MagnitudeSquared(distance) <= (worldRadius + otherSphere.worldRadius) * (worldRadius + otherSphere.worldRadius);
        }
        else if (other is AABB)
        {
            return other.Intersects(this);
        }
        return false;
    }

    private void Start()
    {
        
    }

    private void LateUpdate()
    {
        MyTransform myTransform = GetComponent<MyGameObject>().myTransform;
        worldCenter = myTransform.localToWorldMatrix.GetPosition();
        worldRadius = radius * myTransform.scale.Magnitude()/1.732f; //Approximation of root 3
    }

    private void OnDrawGizmos()
    {
        LateUpdate();
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(worldCenter.ToUnityVector3(), worldRadius);
    }

    public override float GetRadius(MyVector3 axis)
    {
        return worldRadius;
    }
    public override MyVector3[] GetAxes(MyQuaternion rotation)
    {
        MyVector3[] axes = new MyVector3[3];
        axes[0] = MyVector3.right;
        axes[1] = MyVector3.up;
        axes[2] = MyVector3.forward;
        return axes;
    }
}