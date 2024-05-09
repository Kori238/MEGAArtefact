using MyMathLibrary;
using System.Collections.Generic;
using UnityEngine;

public class MyRigidBody : MonoBehaviour
{
    public MyGameObject myGameObject;
    public BoundingObject thisCollider;
    public float mass = 1.0f;
    public float drag = 0.0f;
    public float angularDrag = 0.0f;
    public bool useGravity = true;
    public bool isKinematic = false;

    private MyVector3 velocity = MyVector3.zero;
    private MyVector3 angularVelocity = MyVector3.zero;
    private MyVector3 force = MyVector3.zero;
    private MyVector3 torque = MyVector3.zero;

    private void Start()
    {
        thisCollider = GetComponent<BoundingObject>();
        myGameObject = GetComponent<MyGameObject>();
        thisCollider.collisionEvent += OnBoundingEnter;
    }

    private void FixedUpdate()
    {
        if (!isKinematic)
        {
            // Apply gravity
            if (useGravity)
            {
                force = MyVector3.Add(force, MyVector3.Multiply(MyVector3.Multiply(MyVector3.down, mass), 9.8f));
            }

            // Calculate velocity
            velocity = MyVector3.Add(velocity, MyVector3.Multiply(MyVector3.Divide(force, mass), Time.deltaTime));
            velocity = MyVector3.Multiply(velocity, (1 - drag * Time.deltaTime));

            // Calculate angular velocity
            angularVelocity = MyVector3.Add(angularVelocity, MyVector3.Multiply(MyVector3.Divide(torque, mass), Time.deltaTime));
            angularVelocity = MyVector3.Multiply(angularVelocity, (1 - angularDrag * Time.deltaTime));

            // Update position and rotation
            myGameObject.myTransform.position = MyVector3.Add(myGameObject.myTransform.position, MyVector3.Multiply(velocity, Time.deltaTime));
            myGameObject.myTransform.rotation *= new MyQuaternion(MyVector3.Multiply(angularVelocity, Time.deltaTime));

            // Reset forces
            force = MyVector3.zero;
            torque = MyVector3.zero;
        }
    }

    public void AddForce(MyVector3 force)
    {
        this.force = MyVector3.Add(this.force, force);
    }

    public void AddTorque(MyVector3 torque)
    {
        this.torque = MyVector3.Add(this.torque, torque);
    }

    public void AddImpulse(MyVector3 impulse)
    {
        velocity = MyVector3.Add(velocity, MyVector3.Divide(impulse, mass));
    }

    public void OnBoundingEnter(BoundingObject other, MyVector3 collisionNormal, float penetrationDepth)
    {
        Debug.Log($"CollisionNormal: {collisionNormal} PenetrationDepth: {penetrationDepth}");
        myGameObject.myTransform.position = MyVector3.Add(myGameObject.myTransform.position, MyVector3.Multiply(collisionNormal, penetrationDepth / 50));
        force = MyVector3.zero;
        velocity = MyVector3.zero;
    }
}