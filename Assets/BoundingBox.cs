using MyMathLibrary;
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

    private void LateUpdate()
    {
        MyTransform myTransform = GetComponent<MyGameObject>().myTransform;
        // Calculate the center and size of the bounding box
        worldMax = myTransform.GetLocalToWorldMatrix().TransformPoint(max);
        worldMin = myTransform.GetLocalToWorldMatrix().TransformPoint(min);
    // Draw the gizmo
    }

    private void OnDrawGizmos()
{
    MyTransform myTransform = GetComponent<MyGameObject>().myTransform;
    Gizmos.color = Color.red;
        // Calculate the center and size of the bounding box
    MyVector3 center = myTransform.position;
    MyVector3 size = new MyVector3(Mathf.Abs(max.x - min.x)*myTransform.scale.x, Mathf.Abs(max.y - min.y)*myTransform.scale.y, Mathf.Abs(max.z - min.z)*myTransform.scale.z);
    // Draw the gizmo
    Gizmos.matrix = Matrix4x4.TRS(center.ToUnityVector3(), myTransform.rotation.Normalize().ToUnityQuaternion(), Vector3.one);
    Gizmos.DrawWireCube(Vector3.zero, size.ToUnityVector3());
        /*Gizmos.DrawWireCube(new Vector3((worldMin.x + worldMax.x) / 2f, (worldMin.y + worldMax.y) / 2f, (worldMin.z + worldMax.z) / 2f),
            new Vector3(Mathf.Abs(worldMax.x - worldMin.x), Mathf.Abs(worldMax.y - worldMin.y), Mathf.Abs(worldMax.z - worldMin.z)) / 2f);*/
    }
}
