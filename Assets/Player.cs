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
         float mouseX = Input.GetAxis("Mouse X") * Screen.width / (Screen.height + Screen.width);
        RotatePlayer(mouseX);
    
        // Get the forward and right directions in world space
        MyVector3 forward = body.myGameObject.myTransform.forward;
        MyVector3 right = body.myGameObject.myTransform.right;
    
        // Calculate the movement direction based on the input
        MyVector3 movementDirection = new MyVector3(0, 0, 0);
        movementDirection = MyVector3.Add(movementDirection, MyVector3.Multiply(forward, Input.GetAxis("Vertical")));
        movementDirection = MyVector3.Add(movementDirection, MyVector3.Multiply(right, Input.GetAxis("Horizontal")));
    
        // Normalize the movement direction to prevent faster movement when moving diagonally
        movementDirection = MyVector3.Normalize(movementDirection);
    
        // Apply the movement
        body.AddForce(MyVector3.Multiply(movementDirection, moveSpeed));
    }

    private void RotatePlayer(float mouseX)
    {
        float rotation = mouseX * rotationSpeed * Time.deltaTime;
        body.myGameObject.myTransform.rotation *= new MyQuaternion(rotation, MyVector3.up);
    }

    void TouchedGround(BoundingObject other, MyVector3 collisionNormal, float penetrationDepth)
    {
        if (!canJump && MyVector3.Dot(collisionNormal, MyVector3.up) > 0)
        {
            canJump = true;
        }
    }
}
