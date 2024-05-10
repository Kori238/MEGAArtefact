using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMathLibrary;

[ExecuteInEditMode]
public class CameraController : MonoBehaviour
{
    MyGameObject playerGameObject;
    MyGameObject thisGameObject;
    public float cameraYOffset;
    public float cameraXZOffset;
    public MyQuaternion localZRotation = new MyQuaternion(MyVector3.zero);

    public float rotationSpeedY = 5.0f;
    public float rotationSpeedZ = 5.0f;
    void Start()
    {
        thisGameObject = GetComponent<MyGameObject>();
        playerGameObject = thisGameObject.myTransform.parent;
    }

    // Update is called once per frame
    private void Update()
    {
        MyMatrix4x4 localMatrix = playerGameObject.myTransform.GetLocalToWorldMatrix();
        
        float mouseY = Input.GetAxis("Mouse Y") * Screen.height / (Screen.height + Screen.width);
        MyQuaternion newRotation = localZRotation * new MyQuaternion(mouseY * rotationSpeedZ, MyVector3.right);
        newRotation = newRotation.Normalize();
    
        // Limit the rotation to be between -90 degrees and 90 degrees
        MyVector3 eulerAngles = newRotation.ToEulerAngles();
        eulerAngles.x = Mathf.Clamp(eulerAngles.x, -Mathf.PI / 2, Mathf.PI / 2);
        newRotation = MyQuaternion.FromEulerAngles(eulerAngles);
    
        localZRotation = newRotation;

        transform.SetPositionAndRotation(localMatrix.GetPosition().ToUnityVector3(), 
            localMatrix.GetRotation().ToUnityQuaternion() * 
            (MyMatrix4x4.CreateTranslation(localMatrix.GetPosition()) * MyMatrix4x4.CreateRotation(localZRotation)).ToQuaternion().ToUnityQuaternion());

        transform.position += new Vector3(0, cameraYOffset, 0);
        transform.position += transform.forward * cameraXZOffset;
    }
}
