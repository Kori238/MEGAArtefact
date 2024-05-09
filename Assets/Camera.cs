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

    public float rotationSpeed = 5.0f;
    public float minRotationX = -45.0f;
    public float maxRotationX = 45.0f;

    private float rotationX = 0.0f;
    void Start()
    {
        thisGameObject = GetComponent<MyGameObject>();
        playerGameObject = thisGameObject.myTransform.parent;
    }

    // Update is called once per frame
    private void Update()
    {
        MyMatrix4x4 localMatrix = playerGameObject.myTransform.GetLocalToWorldMatrix();
        transform.SetPositionAndRotation(localMatrix.GetPosition().ToUnityVector3(), localMatrix.GetRotation().ToUnityQuaternion());
        transform.position += new Vector3(0, cameraYOffset, 0);
        transform.position += transform.forward * cameraXZOffset;
        float mouseY = Input.GetAxis("Mouse Y");
        //RotateCamera(mouseY);
    }

    private void RotateCamera(float mouseY)
    {
        rotationX -= mouseY * rotationSpeed * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, minRotationX, maxRotationX);

        transform.localEulerAngles = new Vector3(rotationX, 0, 0);
    }
}
