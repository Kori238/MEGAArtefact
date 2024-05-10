using MyMathLibrary;
using System;
using System.Threading;
using UnityEngine;

public class AABB : BoundingBox
{
    public override bool Intersects(BoundingObject other)
    {
        if (other is OBB)
        {
            return other.Intersects(this);
        }
        else if (other is AABB)
        {
            AABB otherAABB = (AABB)other;
            return (worldMin.x <= otherAABB.worldMax.x && worldMax.x >= otherAABB.worldMin.x) &&
                   (worldMin.y <= otherAABB.worldMax.y && worldMax.y >= otherAABB.worldMin.y) &&
                   (worldMin.z <= otherAABB.worldMax.z && worldMax.z >= otherAABB.worldMin.z);
        }
        else if (other is BoundingSphere)
        {
            BoundingSphere otherSphere = (BoundingSphere)other;
            MyVector3 closestPoint = new MyVector3(
                Mathf.Clamp(otherSphere.worldCenter.x, worldMin.x, worldMax.x),
                Mathf.Clamp(otherSphere.worldCenter.y, worldMin.y, worldMax.y),
                Mathf.Clamp(otherSphere.worldCenter.z, worldMin.z, worldMax.z));
            MyVector3 distance = MyVector3.Subtract(closestPoint, otherSphere.worldCenter);
            return MyVector3.MagnitudeSquared(distance) <= otherSphere.worldRadius * otherSphere.worldRadius;
        }
        else if (other is OBB)
        {
            return other.Intersects(this);
        }
        return false;
    }        
    public override MyVector3[] GetAxes(MyQuaternion rotation)
    {
        MyVector3[] axes = new MyVector3[3];
        axes[0] = MyVector3.right;
        axes[1] = MyVector3.up;
        axes[2] = MyVector3.forward;
        return axes;
    }

    private void LateUpdate()
    {
        MyTransform myTransform = GetComponent<MyGameObject>().myTransform;
        MyMatrix4x4 localToWorldMatrix = myTransform.localToWorldMatrix;
        localToWorldMatrix = MyMatrix4x4.NormalizeRotationMatrix(MyMatrix4x4.InvertRotationMatrix(localToWorldMatrix.GetRotationMatrix())) * localToWorldMatrix; //Do not rotate AABB points!!!!!
        worldMin = localToWorldMatrix.TransformPoint(min);
        worldMax = localToWorldMatrix.TransformPoint(max);
    }

    private void OnDrawGizmos()
    {
        MyTransform myTransform = GetComponent<MyGameObject>().myTransform;
        LateUpdate();
        Gizmos.color = Color.red;
        MyVector3 size = new MyVector3(Mathf.Abs(max.x - min.x), Mathf.Abs(max.y - min.y), Mathf.Abs(max.z - min.z));
        Gizmos.DrawWireCube(MyVector3.Lerp(worldMin, worldMax, 0.5f).ToUnityVector3(), MyVector3.Multiply(myTransform.localToWorldMatrix.GetScale(), size).ToUnityVector3());
    }
}