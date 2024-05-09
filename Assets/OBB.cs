using MyMathLibrary;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class OBB : BoundingBox
{
    public override bool Intersects(BoundingObject other)
    {
        MyTransform thisTransform = GetComponent<MyGameObject>().myTransform;
        MyTransform otherTransform = other.GetComponent<MyGameObject>().myTransform;
        if (other is OBB otherBox)
        {
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
                float radius = GetRadius(axes[i]);
                float distance = Math.Abs(MyVector3.Dot(axes[i], MyVector3.Subtract(otherSphere.worldCenter, thisTransform.position)));
                if (distance > radius + otherSphere.worldRadius)
                    return false;
            }
            return true;
        }
        else if (other is AABB otherAABB)
        {
           // Calculate the axes of this box
            MyVector3[] axes1 = GetAxes(thisTransform.GetLocalToWorldMatrix().GetRotation());

            // Calculate the axes of the AABB
            MyVector3[] axes2 = new MyVector3[] { MyVector3.right, MyVector3.up, MyVector3.forward };

            // Check for intersection on all axes
            return IsSeparated(axes1, axes2, this, otherAABB) == false;
        }

        return false;
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
        float r1 = box1.GetRadius(axis);
        float r2 = box2.GetRadius(axis);
        MyVector3 center1 = box1.GetComponent<MyGameObject>().myTransform.GetLocalToWorldMatrix().GetPosition();
        MyVector3 center2 = box2.GetComponent<MyGameObject>().myTransform.GetLocalToWorldMatrix().GetPosition();
        float distance = Math.Abs(MyVector3.Dot(axis, MyVector3.Subtract(center1, center2)));
        return distance > r1 + r2;
    }

    

    private void LateUpdate()
    {
        MyTransform myTransform = GetComponent<MyGameObject>().myTransform;
        MyMatrix4x4 localToWorldMatrix = myTransform.GetLocalToWorldMatrix();
        worldMin = localToWorldMatrix.TransformPoint(min);
        worldMax = localToWorldMatrix.TransformPoint(max);
    }

    private void OnDrawGizmos()
    {
        LateUpdate();
        MyTransform myTransform = GetComponent<MyGameObject>().myTransform;
        Gizmos.color = Color.red;
        MyMatrix4x4 localTransformation = myTransform.GetLocalToWorldMatrix();
        MyVector3 size = new MyVector3(Mathf.Abs(max.x - min.x), Mathf.Abs(max.y - min.y), Mathf.Abs(max.z - min.z));
        size = MyVector3.Multiply(size, myTransform.GetLocalToWorldMatrix().GetScale());
        Gizmos.matrix = Matrix4x4.TRS(localTransformation.GetPosition().ToUnityVector3(), localTransformation.GetRotation().Normalize().ToUnityQuaternion(), size.ToUnityVector3());
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}
