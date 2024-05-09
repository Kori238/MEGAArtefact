using MyMathLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    public MyVector3 max_scale = new MyVector3(3, 3, 3);
    public MyVector3 min_scale = new MyVector3(1, 1, 1);
    public float max_height = 5f;
    public float min_height = -5f;
    public MyVector3 scaling = new MyVector3(1f, 1f, 1f);
    public MyVector3 translating = new MyVector3(0, 1f, 0);

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MyTransform myTransform = GetComponent<MyGameObject>().myTransform;
        myTransform.position = MyVector3.Add(myTransform.position, MyVector3.Multiply(translating, Time.deltaTime));
        myTransform.rotation *= new MyQuaternion(Mathf.PI / 2 * Time.deltaTime, new MyVector3(0, 1, 0)).Normalize();
        myTransform.scale = MyVector3.Add(myTransform.scale,  MyVector3.Multiply(scaling, Time.deltaTime));
        if ((myTransform.scale.x > max_scale.x && scaling.x > 0) ||
            (myTransform.scale.x < min_scale.x && scaling.x < 0))
        {
            scaling = MyVector3.Subtract(new MyVector3(0, 0, 0), scaling);
        } 
        if ((myTransform.position.y > max_height && translating.y > 0) ||
            (myTransform.position.y < min_height && translating.y < 0))
        {
            translating = MyVector3.Subtract(new MyVector3(0, 0, 0), translating);
        }
        GetComponent<MyGameObject>().myTransform = myTransform;
    }
}
