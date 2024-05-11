using MyMathLibrary;
using UnityEngine;

public class OrbitingObject : MonoBehaviour
{
    public MyGameObject parent;
    public float orbitSpeed = 1f;

    private MyTransform myTransform;

    private void Start()
    {
        myTransform = GetComponent<MyGameObject>().myTransform;
        parent = myTransform.parent;
    }

    private void Update()
    {
        // Calculate the direction from the parent to the orbiting object
        MyVector3 direction = MyVector3.Subtract(myTransform.position, parent.myTransform.position);

        // Rotate the direction by the orbit speed
        MyQuaternion rotation = new MyQuaternion(orbitSpeed * Time.deltaTime, MyVector3.up);
        direction = rotation * direction;

        // Set the new position of the orbiting object
        myTransform.position = MyVector3.Add(parent.myTransform.position, direction);
    }
}