using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMathLibrary;

public class Camera : MonoBehaviour
{
    MyGameObject playerGameObject;
    MyGameObject thisGameObject;
    public float cameraYOffset;
    public float cameraXZOffset;
    void Start()
    {
        thisGameObject = GetComponent<MyGameObject>();
        playerGameObject = thisGameObject.myTransform.parent;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        MyMatrix4x4 localMatrix = playerGameObject.myTransform.GetLocalToWorldMatrix();
        transform.SetPositionAndRotation(localMatrix.GetPosition().ToUnityVector3(), localMatrix.GetRotation().ToUnityQuaternion());
        transform.position += new Vector3(0, cameraYOffset, 0);
        transform.position += transform.forward * cameraXZOffset;
    }


    void Update()
    {
        
    }
}
