using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    MyRigidBody body;
    void Start()
    {
        body = GetComponent<MyRigidBody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            body.AddImpulse(new MyMathLibrary.MyVector3(0, 100, 0));
        }
    }
}
