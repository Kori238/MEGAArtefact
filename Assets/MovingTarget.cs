using MyMathLibrary;
using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    public MyGameObject myGameObject;
    public float scalingSpeed = 1f;
    public float minScale = 0.5f;
    public float maxScale = 2f;
    public float translationSpeed = 1f;
    public MyVector3 minPos = new MyVector3(-5, -5, -5);
    public MyVector3 maxPos = new MyVector3(5, 5, 5);
    public bool translateY = true;
    public bool translateX = true;
    public bool translateZ = true;
    public float rotationSpeed = 1f;
    public float minRotation = -180f;
    public float maxRotation = 180f;
    public MyVector3 rotationAxis = new MyVector3(0, 1, 0);

    private float currentScale = 1f;
    private MyVector3 currentPosition;
    private MyQuaternion currentRotation;
    private int scaleDirection = 1;
    private int translateXDirection = 1;
    private int translateYDirection = 1;
    private int translateZDirection = 1;

    private void Start()
    {
        myGameObject = GetComponent<MyGameObject>();
        minPos = MyVector3.Add(minPos, myGameObject.myTransform.position);
        maxPos = MyVector3.Add(maxPos, myGameObject.myTransform.position);
    }

    private void Update()
    {
        currentPosition = myGameObject.myTransform.position;
        currentRotation = myGameObject.myTransform.rotation;
        // Scale
        currentScale += scalingSpeed * Time.deltaTime * scaleDirection;
        if (currentScale >= maxScale)
        {
            scaleDirection = -1;
        }
        else if (currentScale <= minScale)
        {
            scaleDirection = 1;
        }
        myGameObject.myTransform.scale = new MyVector3(currentScale, currentScale, currentScale);

        // Translate
        if (translateX) {
            currentPosition.x += translationSpeed * Time.deltaTime * translateXDirection;
            if (currentPosition.x <= minPos.x)
            {
                translateXDirection = 1;
            }
            else if (currentPosition.x >= maxPos.x)
            {
                translateXDirection = -1;
            }
        }

        if (translateY) {
            currentPosition.y += translationSpeed * Time.deltaTime * translateYDirection;
            Debug.Log(currentPosition.y);
            if (currentPosition.y <= minPos.y)
            {
                translateYDirection = 1;
            }
            else if (currentPosition.y >= maxPos.y)
            {
                translateYDirection = -1;
            }
        }

        if (translateZ) {
            currentPosition.z += translationSpeed * Time.deltaTime * translateZDirection;
            if (currentPosition.z <= minPos.z)
            {
                translateZDirection = 1;
            }
            else if (currentPosition.z >= maxPos.z)
            {
                translateZDirection = -1;
            }
        }

        myGameObject.myTransform.position = currentPosition;

        // Rotate
        currentRotation = currentRotation * new MyQuaternion(rotationSpeed * Time.deltaTime, rotationAxis).Normalize();
        var (angle, axis) = currentRotation.ToAngleAxis();
        angle = Mathf.Clamp(angle, minRotation, maxRotation);
        currentRotation = new MyQuaternion(angle, axis).Normalize();
        myGameObject.myTransform.rotation = currentRotation.Normalize();
    }
}