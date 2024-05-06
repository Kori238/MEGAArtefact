using MyMathLibrary;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class BoundingBox : BoundingObject
{
    public MyVector3 min;
    public MyVector3 max;
    public MyVector3 worldMax;
    public MyVector3 worldMin;

    public override bool Intersects(BoundingObject other)
    {
        MyTransform thisTransform = GetComponent<MyGameObject>().myTransform;
        MyTransform otherTransform = other.GetComponent<MyGameObject>().myTransform;
        if (other is BoundingBox)
        {
            BoundingBox otherBox = (BoundingBox)other;

            // Calculate the axes of both boxes
            MyVector3[] axes1 = GetAxes(thisTransform.rotation);
            MyVector3[] axes2 = GetAxes(otherTransform.rotation);

            // Check for intersection on all axes
            return IsSeparated(axes1, axes2, this, otherBox) == false;
        }
        else if (other is BoundingSphere)
        {
            BoundingSphere otherSphere = (BoundingSphere)other;
            MyVector3[] axes = GetAxes(thisTransform.rotation);

    // Check for intersection on all axes
            for (int i = 0; i < 3; i++)
            {
                float radius = GetRadius(this, axes[i]);
                float distance = Math.Abs(MyVector3.Dot(axes[i], MyVector3.Subtract(otherSphere.worldCenter, thisTransform.position)));
                if (distance > radius + otherSphere.worldRadius)
                    return false;
            }
            return true;
        }
        return false;
    }

    private MyVector3[] GetAxes(MyQuaternion rotation)
    {
        MyVector3[] axes = new MyVector3[3];
        axes[0] = rotation * MyVector3.right;
        axes[1] = rotation * MyVector3.up;
        axes[2] = rotation * MyVector3.forward;
        return axes;
    }

    private bool IsSeparated(MyVector3[] axes1, MyVector3[] axes2, BoundingBox box1, BoundingBox box2)
    {
        // Check for intersection on all axes
        for (int i = 0; i < 3; i++)
        {
            if (IsSeparatedOnAxis(axes1[i], box1, box2))
                return true;
            if (IsSeparatedOnAxis(axes2[i], box1, box2))
                return true;
        }

        // Check for intersection on cross products of axes
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                MyVector3 axis = MyVector3.Cross(axes1[i], axes2[j]);
                if (MyVector3.MagnitudeSquared(axis) > 0 && IsSeparatedOnAxis(axis, box1, box2))
                    return true;
            }
        }

        return false;
    }

    private bool IsSeparatedOnAxis(MyVector3 axis, BoundingBox box1, BoundingBox box2)
    {
        MyTransform box1Transform = box1.GetComponent<MyGameObject>().myTransform;
        MyTransform box2Transform = box2.GetComponent<MyGameObject>().myTransform;
        float r1 = GetRadius(box1, axis);
        float r2 = GetRadius(box2, axis);
        float distance = Math.Abs(MyVector3.Dot(axis, MyVector3.Subtract(box1Transform.position, box2Transform.position)));
        return distance > r1 + r2;
    }

    private float GetRadius(BoundingBox box, MyVector3 axis)
    {
        float r = 0;
        r += Math.Abs(MyVector3.Dot(axis, MyVector3.Subtract(box.max, box.min))) / 2;
        return r;
    }

    private bool IsPointInside(MyVector3 point, MyVector3 min, MyVector3 max)
    {
        return point.x >= min.x && point.x <= max.x &&
               point.y >= min.y && point.y <= max.y &&
               point.z >= min.z && point.z <= max.z;
    }

    private void LateUpdate()
    {
        MyTransform myTransform = GetComponent<MyGameObject>().myTransform;
        worldMax = myTransform.GetLocalToWorldMatrix().TransformPoint(max);
        worldMin = myTransform.GetLocalToWorldMatrix().TransformPoint(min);
    }

    private void OnDrawGizmos()
{
    MyTransform myTransform = GetComponent<MyGameObject>().myTransform;
    Gizmos.color = Color.red;
    MyVector3 center = myTransform.position;
    MyVector3 size = new MyVector3(Mathf.Abs(max.x - min.x)*myTransform.scale.x, Mathf.Abs(max.y - min.y)*myTransform.scale.y, Mathf.Abs(max.z - min.z)*myTransform.scale.z);
    Gizmos.matrix = Matrix4x4.TRS(center.ToUnityVector3(), myTransform.rotation.Normalize().ToUnityQuaternion(), Vector3.one);
    Gizmos.DrawWireCube(Vector3.zero, size.ToUnityVector3());
    }
}
