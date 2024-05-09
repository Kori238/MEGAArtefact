using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMathLibrary;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    MyRigidBody body;
    BoundingObject thisCollider;
    public float moveSpeed = 5;
    public float jumpForce = 10;
    public float rotationSpeed = 5.0f;
    public bool canJump;

    void Start()
    {
        body = GetComponent<MyRigidBody>();
        thisCollider = GetComponent<BoundingObject>();
        thisCollider.collisionEvent += TouchedGround;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;
            body.myGameObject.myTransform.position = MyVector3.Add(body.myGameObject.myTransform.position, new MyVector3(0, 0.1f, 0));
            body.AddImpulse(new MyMathLibrary.MyVector3(0, jumpForce, 0));
        }
        float mouseX = Input.GetAxis("Mouse X");
        RotatePlayer(mouseX);
        body.AddForce(new MyVector3(Input.GetAxis("Horizontal") * moveSpeed, 0, Input.GetAxis("Vertical") * moveSpeed));
    }

    private void RotatePlayer(float mouseX)
    {
        float rotation = mouseX * rotationSpeed * Time.deltaTime;
        body.myGameObject.myTransform.rotation =  body.myGameObject.myTransform.rotation * new MyQuaternion(rotation, MyVector3.up);
    }

    void TouchedGround(BoundingObject other, MyVector3 collisionNormal, float penetrationDepth)
    {
        if (!canJump && MyVector3.Dot(collisionNormal, MyVector3.up) > 0)
        {
            canJump = true;
        }
    }
}
