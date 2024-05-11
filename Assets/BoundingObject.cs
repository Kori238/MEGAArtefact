using MyMathLibrary;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class BoundingObject : MonoBehaviour
{
    public bool isTrigger = false;
    public delegate void CollisionEvent(BoundingObject other, MyVector3 collisionNormal, float penetrationDepth);
    public event CollisionEvent collisionEvent;
    public abstract bool Intersects(BoundingObject other);

    public abstract MyVector3[] GetAxes(MyQuaternion rotation);

    public abstract float GetRadius(MyVector3 axis);

    private void Update()
    {
        foreach (BoundingObject obj in GameObject.FindObjectsOfType<BoundingObject>())
        {
            if (obj == this) continue;
            if (!Intersects(obj)) continue;

            MyRigidBody myRigidBody = GetComponent<MyRigidBody>();

            MyVector3 collisionNormal = MyVector3.zero;
            float penetrationDepth = 0f;

            if (myRigidBody != null)
            {
                collisionNormal = CalculateCollisionNormal(obj);
                penetrationDepth = CalculatePenetrationDepth(obj, collisionNormal);
            }
            collisionEvent?.Invoke(obj, collisionNormal, penetrationDepth);
        }
    }

    private MyVector3 CalculateCollisionNormal(BoundingObject other)
    {
        MyTransform thisTransform = GetComponent<MyGameObject>().myTransform;
        MyTransform otherTransform = other.GetComponent<MyGameObject>().myTransform;

        MyVector3 distance = MyVector3.Subtract(thisTransform.localToWorldMatrix.GetPosition(), otherTransform.localToWorldMatrix.GetPosition());
        MyVector3 direction = MyVector3.Normalize(distance);

        MyVector3[] axes1 = GetAxes(thisTransform.localToWorldMatrix.GetRotation());
        MyVector3[] axes2 = other.GetAxes(otherTransform.localToWorldMatrix.GetRotation());

        // Transform the local axes into world space
        for (int i = 0; i < 3; i++)
        {
            axes1[i] = thisTransform.localToWorldMatrix.GetRotation() * axes1[i];
            axes2[i] = otherTransform.localToWorldMatrix.GetRotation() * axes2[i];
        }

        float minOverlap = float.MaxValue;
        MyVector3 collisionNormal = MyVector3.zero;

        foreach (MyVector3 axis in axes1)
        {
            float overlap = IsSeparated(axis, this, other);
            if (Mathf.Abs(overlap) < minOverlap)
            {
                minOverlap = Mathf.Abs(overlap);
                collisionNormal = axis;
            }
        }

        foreach (MyVector3 axis in axes2)
        {
            float overlap = IsSeparated(axis, this, other);
            if (Mathf.Abs(overlap) < minOverlap)
            {
                minOverlap = Mathf.Abs(overlap);
                collisionNormal = axis;
            }
        }

        //Make sure the collision normal is pointing in the correct direction
        if (MyVector3.Dot(collisionNormal, direction) < 0)
        {
            collisionNormal = MyVector3.Multiply(collisionNormal, -1);
        }

        return collisionNormal;
    }

    private float CalculatePenetrationDepth(BoundingObject other, MyVector3 collisionNormal)
    {
        float penetrationDepth = IsSeparated(collisionNormal, this, other);
        return Mathf.Abs(penetrationDepth);
    }

    private float IsSeparated(MyVector3 axis, BoundingObject box1, BoundingObject box2)
    {
        float r1 = box1.GetRadius(axis);
        float r2 = box2.GetRadius(axis);
        MyVector3 center1 = box1.GetComponent<MyGameObject>().myTransform.localToWorldMatrix.GetPosition();
        MyVector3 center2 = box2.GetComponent<MyGameObject>().myTransform.localToWorldMatrix.GetPosition();
        float distance = Math.Abs(MyVector3.Dot(axis, MyVector3.Subtract(center1, center2)));
        return distance - (r1 + r2);
    }
}

public abstract class BoundingBox : BoundingObject
{
    public MyVector3 min = new MyVector3(-0.5f, -0.5f, -0.5f);
    public MyVector3 max = new MyVector3(0.5f, 0.5f, 0.5f);
    public MyVector3 worldMin;
    public MyVector3 worldMax;

    public override float GetRadius(MyVector3 axis)
    {
        MyVector3[] axes = GetAxes(this.GetComponent<MyGameObject>().myTransform.localToWorldMatrix.GetRotation());
        MyVector3 boxSize = new MyVector3 (
            this.worldMax.x - this.worldMin.x,
            this.worldMax.y - this.worldMin.y,
            this.worldMax.z - this.worldMin.z
            );
        float r = 0;
        r += Math.Abs(MyVector3.Dot(axis, MyVector3.Multiply(axes[0], boxSize.x))) / 2;
        r += Math.Abs(MyVector3.Dot(axis, MyVector3.Multiply(axes[1], boxSize.y))) / 2;
        r += Math.Abs(MyVector3.Dot(axis, MyVector3.Multiply(axes[2], boxSize.z))) / 2;
        return r;
    }
    public override MyVector3[] GetAxes(MyQuaternion rotation)
    {
        MyVector3[] axes = new MyVector3[3];
        axes[0] = rotation * MyVector3.right;
        axes[1] = rotation * MyVector3.up;
        axes[2] = rotation * MyVector3.forward;
        return axes;
    }
}